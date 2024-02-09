using intershipJenkins.Api.Models;
using intershipJenkins.Api.Services.BookService;
using intershipJenkins.Tests.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace intershipJenkins.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IMongoCollection<Book>> _mockCollection;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockCollection = new Mock<IMongoCollection<Book>>();
            var mockDatabaseConfig = new Mock<IOptions<DatabaseConfiguration>>();
            mockDatabaseConfig.Setup(x => x.Value).Returns(new DatabaseConfiguration
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase"
            });

            var mockMongoDatabase = new Mock<IMongoDatabase>();
            mockMongoDatabase.Setup(x => x.GetCollection<Book>(typeof(Book).Name, null)).Returns(_mockCollection.Object);

            var mockMongoClient = new Mock<IMongoClient>();
            mockMongoClient.Setup(x => x.GetDatabase("TestDatabase", null)).Returns(mockMongoDatabase.Object);

            _bookService = new BookService(mockDatabaseConfig.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidBook_ReturnsInsertedBook()
        {
            // Arrange
            var book = new Book { Id = ObjectId.GenerateNewId().ToString(), Title = "Test Book" };
            _mockCollection.Setup(x => x.InsertOneAsync(book, null, default)).Returns(Task.CompletedTask);

            // Act
            var result = await _bookService.createAsync(book);

            // Assert
            Assert.Equal(book, result);
            Assert.Equal(book.Id, result.Id);
        }
       /* [Fact]
        public async Task GetById_ExistingId_ReturnsBook()
        {
            // Arrange
            var book = new Book { Id = "1", Title = "Test Book" };
            _mockCollection.Setup(x => x.Find(It.IsAny < Book >> (), null, default)).Returns(new FakeMongoCursor<Book>(new List<Book> { book }));

            // Act
            var result = await _bookService.getById("1");

            // Assert
            Assert.Equal(book, result);
        }*/

       


    }
}
