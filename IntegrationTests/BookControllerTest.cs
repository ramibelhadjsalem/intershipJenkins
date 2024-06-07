using intershipJenkins.Api.DTOS;
using intershipJenkins.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace intershipJenkins.IntegrationTests
{
    public class BookControllerTest : BaseControllerIntergerationtest
    {
        public BookControllerTest() :base(){}
        //[Fact]
        //public async Task Get_ReturnsListOfBooks()
        //{


        //    var response = await _httpClient.GetAsync("/api/book");


        //    response.EnsureSuccessStatusCode();

        //    var books = await response.Content.ReadFromJsonAsync<List<Book>>(); // Assuming Book is your model class
        //    Assert.NotNull(books); // Ensure books list is not null



        //}

        [Theory]
        [InlineData("test1", "author1", 10.5, "cate")]
        [InlineData("test2", "author2", 20.2, "cate")]
        [InlineData("test3", "author3", 30.3, "cate")]
        public async Task AddBook_ReturnsCreatedStatusCode(string title, string author, decimal price, string category)
        {
            // Arrange
            var newBook = new BookDto { Title = title, Author = author, Price = price, Category = category };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/book", newBook);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData(null, "author", 10.5, "category")] // Null title
        [InlineData("title", null, 10.5, "category")]   // Null author
        [InlineData("title", "author", 10.5, null)]     // Null category

        public async Task AddBook_WithInvalidData_ReturnsBadRequest(string title, string author, decimal price, string category)
        {
            // Arrange
            var newBook = new BookDto { Title = title, Author = author, Price = price, Category = category };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/book", newBook);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_ExistingBook_ReturnsNoContent()
        {
            // Arrange: Add a book to update
            var newBook = new Book { Title = "To Be Updated", Author = "Author Name", Price = 10.99m, Category = "Category" };
            var addResponse = await _httpClient.PostAsJsonAsync("/api/book", newBook);
            var addedBook = await addResponse.Content.ReadFromJsonAsync<Book>();

            // Update the book's details
            addedBook.Title = "Updated Title";
            addedBook.Author = "Updated Author";
            addedBook.Price = 20.99m;
            addedBook.Category = "Updated Category";

            // Act: Send a PUT request to update the book
            var updateResponse = await _httpClient.PutAsJsonAsync($"/api/book/{addedBook.Id}", addedBook);

            // Assert: Check if the response status code is NoContent
            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

            // Optionally, you can retrieve the updated book and assert its details
            var getResponse = await _httpClient.GetAsync($"/api/book/{addedBook.Id}");
            getResponse.EnsureSuccessStatusCode(); // Ensure the response is successful
            var updatedBook = await getResponse.Content.ReadFromJsonAsync<Book>();

            Assert.NotNull(updatedBook);
            Assert.Equal("Updated Title", updatedBook.Title);
            Assert.Equal("Updated Author", updatedBook.Author);
            Assert.Equal(20.99m, updatedBook.Price);
            Assert.Equal("Updated Category", updatedBook.Category);
        }
        [Fact]
        public async Task Update_NonExistingBook_ReturnsNotFound()
        {
            // Arrange: Create a new book object with updated details
            var updatedBook = new Book { Id = "nonexistingid", Title = "Updated Title", Author = "Updated Author", Price = 20.99m, Category = "Updated Category" };

            // Act: Send a PUT request to update the non-existing book
            var updateResponse = await _httpClient.PutAsJsonAsync($"/api/book/{updatedBook.Id}", updatedBook);

            // Assert: Check if the response status code is NotFound
            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        }

    }
}

