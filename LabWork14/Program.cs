using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary12;
using ClassLibrary13;
using ClassLibraryLab10;

namespace LabWork14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание коллекции Bank
            List<SortedDictionary<string, Card>> Bank = new List<SortedDictionary<string, Card>>();

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
            Bank.Add(branch1);
            Bank.Add(branch2);

            // Вывод содержимого для проверки
            Console.WriteLine("Содержимое банка:");
            foreach (var branch in Bank)
            {
                foreach (var card in branch)
                {
                    Console.WriteLine($"ID: {card.Key}, Имя: {card.Value.Name}, Срок: {card.Value.Time}");
                }
            }

            // Выполнение запросов
            ExecuteQueries(Bank);

            Console.ReadLine();
        }

        static void ExecuteQueries(List<SortedDictionary<string, Card>> bank)
        {
            // a) LINQ запрос на выборку данных
            var queryWhere = from branch in bank
                             from card in branch
                             where card.Value.Name.StartsWith("I")
                             select card;

            Console.WriteLine("\nРезультат запроса (Where):");
            foreach (var card in queryWhere)
            {
                Console.WriteLine($"ID: {card.Key}, Имя: {card.Value.Name}, Срок: {card.Value.Time}");
            }

            // b) Методы расширения на операции над множествами
            var branch1Cards = bank[0].Values;
            var branch2Cards = bank[1].Values;

            var queryUnion = branch1Cards.Union(branch2Cards);
            var queryExcept = branch1Cards.Except(branch2Cards);
            var queryIntersect = branch1Cards.Intersect(branch2Cards);

            Console.WriteLine("\nРезультаты запросов (Union, Except, Intersect):");
            Console.WriteLine("Union:");
            foreach (var card in queryUnion)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
            Console.WriteLine("\nExcept:");
            foreach (var card in queryExcept)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }
            Console.WriteLine("\nIntersect:");
            foreach (var card in queryIntersect)
            {
                Console.WriteLine($"ID: {card.Id}, Имя: {card.Name}, Срок: {card.Time}");
            }

            // c) Агрегирование данных
            var querySum = bank.SelectMany(branch => branch.Values).Sum(card => card.num.number);
            var queryMax = bank.SelectMany(branch => branch.Values).Max(card => int.Parse(card.Time.Substring(0, 2)));
            var queryMin = bank.SelectMany(branch => branch.Values).Min(card => int.Parse(card.Time.Substring(0, 2)));
            var queryAverage = bank.SelectMany(branch => branch.Values).Average(card => card.num.number);

            Console.WriteLine($"\nРезультаты запросов (Sum, Max, Min, Average):");
            Console.WriteLine($"Сумма номеров на картах: {querySum}");
            Console.WriteLine($"Максимальный месяц срока карты: {queryMax}");
            Console.WriteLine($"Минимальный месяц срока карты: {queryMin}");
            Console.WriteLine($"Среднее значение номеров на картах: {queryAverage}");

            // d) Группировка данных
            var queryGroupBy = from branch in bank
                               from card in branch
                               group card by card.Value.Time.Substring(3, 2) into g
                               select new { Month = g.Key, Count = g.Count() };

            Console.WriteLine("\nРезультат запроса (Group by):");
            foreach (var group in queryGroupBy)
            {
                Console.WriteLine($"Месяц: {group.Month}, Количество карт: {group.Count}");
            }

            // e) Получение нового типа (Let)
            var queryLet = from branch in bank
                           let totalCards = branch.Count
                           select new { BranchName = branch.First().Value.Name, TotalCards = totalCards };

            Console.WriteLine("\nРезультат запроса (Let):");
            foreach (var result in queryLet)
            {
                Console.WriteLine($"Отделение: {result.BranchName}, Всего карт: {result.TotalCards}");
            }

            // f) Соединение (Join)
            var queryJoin = from branch1 in bank[0]
                            join branch2 in bank[1] on branch1.Key equals branch2.Key
                            select new { Name1 = branch1.Value.Name, Name2 = branch2.Value.Name };

            Console.WriteLine("\nРезультат запроса (Join):");
            foreach (var result in queryJoin)
            {
                Console.WriteLine($"Имя из отделения 1: {result.Name1}, Имя из отделения 2: {result.Name2}");
            }
        }
    }
}
