using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabWork14
{
    internal class Program
    {
        static List<SortedDictionary<string, Card>> Bank;

        static void Main(string[] args)
        {
            Bank = InitializeBank();

            // Главное меню
            while (true)
            {
                DisplayMenu();

                Console.Write("\nВыберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowBankContents();
                        break;
                    case "2":
                        ExecuteWhereQuery();
                        break;
                    case "3":
                        ExecuteSetOperations();
                        break;
                    case "4":
                        ExecuteAggregation();
                        break;
                    case "5":
                        ExecuteGroupBy();
                        break;
                    case "6":
                        ExecuteLetQuery();
                        break;
                    case "7":
                        ExecuteJoinQuery();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static List<SortedDictionary<string, Card>> InitializeBank()
        {
            // Создание коллекции Bank
            List<SortedDictionary<string, Card>> bank = new List<SortedDictionary<string, Card>>();

            // Создание отделений банка и заполнение картами
            SortedDictionary<string, Card> branch1 = new SortedDictionary<string, Card>();
            SortedDictionary<string, Card> branch2 = new SortedDictionary<string, Card>();

            // Добавление карт в отделение 1
            branch1.Add("1234 5678 9012 3456", new Card("1234 5678 9012 3456", "Ivan Petrov", "02/25", 1));
            branch1.Add("9876 5432 1098 7654", new Card("9876 5432 1098 7654", "Anna Ivanova", "05/24", 2));
            branch1.Add("4567 8901 2345 6789", new Card("4567 8901 2345 6789", "John Smith", "09/23", 3));

            // Добавление карт в отделение 2
            branch2.Add("1111 2222 3333 4444", new Card("1111 2222 3333 4444", "Olga Kuznetsova", "11/26", 4));
            branch2.Add("5555 6666 7777 8888", new Card("5555 6666 7777 8888", "Alex Johnson", "03/25", 5));

            // Добавление отделений в коллекцию Bank
            bank.Add(branch1);
            bank.Add(branch2);

            return bank;
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Показать содержимое банка");
            Console.WriteLine("2. LINQ запрос на выборку данных (Where)");
            Console.WriteLine("3. Операции над множествами (Union, Except, Intersect)");
            Console.WriteLine("4. Агрегирование данных (Sum, Max, Min, Average)");
            Console.WriteLine("5. Группировка данных (Group by)");
            Console.WriteLine("6. Получение нового типа (Let)");
            Console.WriteLine("7. Соединение (Join)");
            Console.WriteLine("8. Выход");
        }

        static void ShowBankContents()
        {
            Console.WriteLine("\nСодержимое банка:");
            foreach (var branch in Bank)
            {
                foreach (var card in branch)
                {
                    Console.WriteLine($"ID: {card.Key}, Имя: {card.Value.Name}, Срок: {card.Value.Time}");
                }
            }
        }

        static void ExecuteWhereQuery()
        {
            // a) LINQ запрос на выборку данных
            var queryWhereLinq = from branch in Bank
                                 from card in branch
                                 where card.Value.Name.StartsWith("I")
                                 select card;

            Console.WriteLine("\nРезультат запроса (Where - LINQ):");
            foreach (var card in queryWhereLinq)
            {
                Console.WriteLine($"ID: {card.Key}, Имя: {card.Value.Name}, Срок: {card.Value.Time}");
            }

            // b) Методы расширения на операции над множествами
            var queryWhereExtensions = Bank.SelectMany(branch => branch.Values)
                                           .Where(card => card.Name.StartsWith("I"))
                                           .Select(card => new { card.Id, card.Name, card.Time });

            Console.WriteLine("\nРезультат запроса (Where - Методы расширения):");
            foreach (var card in queryWhereExtensions)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
        }

        static void ExecuteSetOperations()
        {
            // a) LINQ запрос на операции над множествами
            var queryUnionLinq = Bank.SelectMany(branch => branch.Values);
            var queryExceptLinq = Bank.SelectMany(branch => branch.Values);
            var queryIntersectLinq = Bank.SelectMany(branch => branch.Values);

            Console.WriteLine("\nРезультаты запросов (Union, Except, Intersect - LINQ):");
            Console.WriteLine("Union:");
            foreach (var card in queryUnionLinq)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
            Console.WriteLine("\nExcept:");
            foreach (var card in queryExceptLinq)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
            Console.WriteLine("\nIntersect:");
            foreach (var card in queryIntersectLinq)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }

            // b) Методы расширения на операции над множествами
            var allCards = Bank.SelectMany(branch => branch.Values);
            var queryUnionExtensions = allCards.Union(allCards);
            var queryExceptExtensions = allCards.Except(allCards);
            var queryIntersectExtensions = allCards.Intersect(allCards);

            Console.WriteLine("\nРезультаты запросов (Union, Except, Intersect - Методы расширения):");
            Console.WriteLine("Union:");
            foreach (var card in queryUnionExtensions)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
            Console.WriteLine("\nExcept:");
            foreach (var card in queryExceptExtensions)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
            Console.WriteLine("\nIntersect:");
            foreach (var card in queryIntersectExtensions)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
        }

        static void ExecuteAggregation()
        {
            // a) LINQ запрос на агрегирование данных
            var queryAggregationLinq = Bank.SelectMany(branch => branch.Values);
            var sumLinq = queryAggregationLinq.Sum(card => card.num.number);
            var maxLinq = queryAggregationLinq.Max(card => int.Parse(card.Time.Substring(0, 2)));
            var minLinq = queryAggregationLinq.Min(card => int.Parse(card.Time.Substring(0, 2)));
            var averageLinq = queryAggregationLinq.Average(card => card.num.number);

            Console.WriteLine($"\nРезультаты запроса (Sum, Max, Min, Average - LINQ):");
            Console.WriteLine($"Сумма номеров на картах: {sumLinq}");
            Console.WriteLine($"Максимальный месяц срока карты: {maxLinq}");
            Console.WriteLine($"Минимальный месяц срока карты: {minLinq}");
            Console.WriteLine($"Среднее значение номеров на картах: {averageLinq}");

            // b) Методы расширения на агрегирование данных
            var queryAggregationExtensions = Bank.SelectMany(branch => branch.Values);
            var sumExtensions = queryAggregationExtensions.Sum(card => card.num.number);
            var maxExtensions = queryAggregationExtensions.Max(card => int.Parse(card.Time.Substring(0, 2)));
            var minExtensions = queryAggregationExtensions.Min(card => int.Parse(card.Time.Substring(0, 2)));
            var averageExtensions = queryAggregationExtensions.Average(card => card.num.number);

            Console.WriteLine($"\nРезультаты запроса (Sum, Max, Min, Average - Методы расширения):");
            Console.WriteLine($"Сумма номеров на картах: {sumExtensions}");
            Console.WriteLine($"Максимальный месяц срока карты: {maxExtensions}");
            Console.WriteLine($"Минимальный месяц срока карты: {minExtensions}");
            Console.WriteLine($"Среднее значение номеров на картах: {averageExtensions}");
        }

        static void ExecuteGroupBy()
        {
            // a) LINQ запрос на группировку данных
            var queryGroupByLinq = Bank.SelectMany(branch => branch.Values)
                                      .GroupBy(card => card.Time.Substring(3, 2))
                                      .Select(g => new { Month = g.Key, Count = g.Count() });

            Console.WriteLine("\nРезультат запроса (Group by - LINQ):");
            foreach (var group in queryGroupByLinq)
            {
                Console.WriteLine($"Месяц: {group.Month}, Количество карт: {group.Count}");
            }

            // b) Методы расширения на группировку данных
            var queryGroupByExtensions = Bank.SelectMany(branch => branch.Values)
                                            .GroupBy(card => card.Time.Substring(3, 2))
                                            .Select(g => new { Month = g.Key, Count = g.Count() });

            Console.WriteLine("\nРезультат запроса (Group by - Методы расширения):");
            foreach (var group in queryGroupByExtensions)
            {
                Console.WriteLine($"Месяц: {group.Month}, Количество карт: {group.Count}");
            }
        }

        static void ExecuteLetQuery()
        {
            // a) LINQ запрос на получение нового типа
            var queryLetLinq = Bank.SelectMany(branch => branch.Values)
                                   .Select(card => new { BranchName = card.Name, TotalCards = 1 })
                                   .GroupBy(x => x.BranchName)
                                   .Select(g => new { BranchName = g.Key, TotalCards = g.Sum(x => x.TotalCards) });

            Console.WriteLine("\nРезультат запроса (Let - LINQ):");
            foreach (var result in queryLetLinq)
            {
                Console.WriteLine($"Отделение: {result.BranchName}, Всего карт: {result.TotalCards}");
            }

            // b) Методы расширения на получение нового типа
            var queryLetExtensions = Bank.SelectMany(branch => branch.Values)
                                         .Select(card => new { BranchName = card.Name, TotalCards = 1 })
                                         .GroupBy(x => x.BranchName)
                                         .Select(g => new { BranchName = g.Key, TotalCards = g.Sum(x => x.TotalCards) });

            Console.WriteLine("\nРезультат запроса (Let - Методы расширения):");
            foreach (var result in queryLetExtensions)
            {
                Console.WriteLine($"Отделение: {result.BranchName}, Всего карт: {result.TotalCards}");
            }
        }

        static void ExecuteJoinQuery()
        {
            // a) LINQ запрос на соединение
            var queryJoinLinq = Bank[0].Join(Bank[1],
                                             b1 => b1.Key,
                                             b2 => b2.Key,
                                             (b1, b2) => new { Name1 = b1.Value.Name, Name2 = b2.Value.Name });

            Console.WriteLine("\nРезультат запроса (Join - LINQ):");
            foreach (var result in queryJoinLinq)
            {
                Console.WriteLine($"Имя из отделения 1: {result.Name1}, Имя из отделения 2: {result.Name2}");
            }

            // b) Методы расширения на соединение
            var queryJoinExtensions = Bank[0].Join(Bank[1],
                                                   b1 => b1.Key,
                                                   b2 => b2.Key,
                                                   (b1, b2) => new { Name1 = b1.Value.Name, Name2 = b2.Value.Name });

            Console.WriteLine("\nРезультат запроса (Join - Методы расширения):");
            foreach (var result in queryJoinExtensions)
            {
                Console.WriteLine($"Имя из отделения 1: {result.Name1}, Имя из отделения 2: {result.Name2}");
            }
        }
    }
}
