using ClassLibrary12;
using ClassLibraryLab10;
using LabWork14;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabWork13Tests
{
    [TestClass]
    public class ProgramTests
    {
        private MyObservableCollection<Card> collection1;
        private MyObservableCollection<Card> collection2;


        [TestMethod]
        public void AddElement_AddsElementToCollection()
        {
            // Arrange
            Card cardToAdd = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);

            // Act
            collection1.Add(cardToAdd);

            // Assert
            Assert.AreEqual(1, collection1.Count);
            Assert.IsTrue(collection1.Contains(cardToAdd));
        }
        [TestMethod]
        public void RemoveElement_RemovesElementFromCollection()
        {
            // Arrange
            Card cardToRemove = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
            collection1.Add(cardToRemove);

            // Act
            collection1.Remove(cardToRemove);

            // Assert
            Assert.AreEqual(0, collection1.Count);
            Assert.IsFalse(collection1.Contains(cardToRemove));
        }

        [TestMethod]
        public void ModifyElement_ModifiesExistingElement()
        {
            // Arrange
            Card initialCard = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
            collection1.Add(initialCard);

            Card modifiedCard = new Card("5678 9012 3456 1234", "Jane Smith", "01/27", 456);

            // Act
            collection1[0] = modifiedCard;

            // Assert
            Assert.AreEqual(1, collection1.Count);
            Assert.IsFalse(collection1.Contains(initialCard));
            Assert.IsTrue(collection1.Contains(modifiedCard));
        }
    }

    [TestClass]
    public class UnitTest1
    {
        private static List<SortedDictionary<string, Card>> Bank;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Bank = Program.InitializeBank();
        }

        [TestMethod]
        public void TestExecuteWhereQuery()
        {
            // Arrange
            string expectedName = "Ivan Petrov";

            // Act
            var queryWhereLinq = from branch in Bank
                                 from card in branch
                                 where card.Value.Name.StartsWith("I")
                                 select card;

            // Assert
            Assert.IsNotNull(queryWhereLinq);
            Assert.IsTrue(queryWhereLinq.Any());

            var firstCard = queryWhereLinq.First();
            Assert.AreEqual(expectedName, firstCard.Value.Name);
        }
    }
}
