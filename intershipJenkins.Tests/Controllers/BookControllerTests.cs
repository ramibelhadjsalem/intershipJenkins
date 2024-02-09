using intershipJenkins.Api.Controllers;
using intershipJenkins.Api.DTOS;
using intershipJenkins.Api.Models;
using intershipJenkins.Api.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace intershipJenkins.Tests.Controllers
{
    public class BookControllerTests
    {

        [Fact]
        public async Task Get_ReturnsListOfBooks()
        {
            // Arrange
            var mockBooksService = new Mock<IBookServices>();
            var expectedBooks = new List<Book>
            {
                new Book { Id = Guid.NewGuid().ToString(), Title = "Book 1", Author = "Author 1" },
                new Book { Id = Guid.NewGuid().ToString(), Title = "Book 2", Author = "Author 2" },
                new Book { Id = Guid.NewGuid().ToString(), Title = "Book 3", Author = "Author 3" }
            };

            mockBooksService.Setup(service => service.listAsync())
                            .ReturnsAsync(expectedBooks);

            var controller = new BookController(mockBooksService.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<List<Book>>(okResult.Value);
            Assert.Equal(expectedBooks.Count, books.Count);



        }

        [Fact]
        public async Task Get_WithNotFoundId_ReturnsNotFound()
        {
            // Arrange
            var mockBookService = new Mock<IBookServices>();
            string notFoundId = Guid.NewGuid().ToString();

            mockBookService.Setup(services => services.getById(notFoundId))
                .ReturnsAsync((Book)null);

            var controller = new BookController(mockBookService.Object);

            //Act
            var actionResult = await controller.Get(notFoundId);

            //Assert

            Assert.IsType<NotFoundResult>(actionResult.Result);

        }

        [Fact]
        public async Task Get_WithValid_ReturnOkResult()
        {
            //Arrange 
            var mockServices = new Mock<IBookServices>();
            string validId = Guid.NewGuid().ToString();
            var expectingBook = new Book { Id = validId, Title = "test 123" };


            mockServices.Setup(services => services.getById(validId))
                    .ReturnsAsync(expectingBook);

            var controller = new BookController(mockServices.Object);

            //Act

            var actionResult = await controller.Get(validId);

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var book = Assert.IsType<Book>(okResult.Value);

            Assert.Equal(expectingBook.Id, book.Id);
        }

        [Fact]
        public async Task Post_ValidBook_ReturnsCreatedAtAction()
        {
            // Arrange
            var bookDto = new BookDto
            {
                Title = "Test Book",
                Price = 10.99m,
                Author = "Test Author",
                Category = "Test Category"
            };

            var createdBook = new Book
            {
                Id = Guid.NewGuid().ToString(),
                Title = bookDto.Title,
                Price = bookDto.Price,
                Author = bookDto.Author,
                Category = bookDto.Category
            };

            var booksServiceMock = new Mock<IBookServices>();
            booksServiceMock.Setup(service => service.createAsync(It.IsAny<Book>()))
                .ReturnsAsync(createdBook);

            var booksController = new BookController(booksServiceMock.Object);

            // Act
            var actionResult = await booksController.Post(bookDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal("Get", createdAtActionResult.ActionName);
        
            Assert.Equal(createdBook.Author, ((Book)createdAtActionResult.Value).Author);
            Assert.Equal(createdBook.Category, ((Book)createdAtActionResult.Value).Category);
            Assert.Equal(createdBook.Price, ((Book)createdAtActionResult.Value).Price);

        }

       /* [Fact]
        public async Task Post_EmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IBookServices>();
            var controller = new BookController(mockService.Object);
            var invalidBook = new BookDto { Price = 10, Author = "John Doe", Category = "Fiction" };

            // Act
            var actionResult = await controller.Post(invalidBook);

            // Assert
            var result = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
          *//*  Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            var errors = await result.Content.ReadAsAsync<ValidationProblemDetails>();
            Assert.True(errors.Errors.ContainsKey("Title"));
            Assert.Contains(errors.Errors["Title"], "Title is required.");*//*
        }*/
    }

}

