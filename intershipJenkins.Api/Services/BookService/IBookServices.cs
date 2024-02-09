using intershipJenkins.Api.Models;

namespace intershipJenkins.Api.Services.BookService
{
    public interface IBookServices
    {
        public Task<List<Book>> listAsync();
        public Task<Book> getById(string id);
        public Task<Book> createAsync(Book book);

        public Task updateAsync(string id, Book updatedBook);
        public Task removeById(string id);
    }
}
