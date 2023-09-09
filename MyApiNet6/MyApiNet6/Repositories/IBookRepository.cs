using MyApiNet6.Models;

namespace MyApiNet6.Repositories
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> getAllBooksAsync();
        public Task<BookModel> getBookAsync(int id);
        public Task<int> AddBookAsync(BookModel model);
        public Task UpdateBookAsync(int id, BookModel model);
        public Task DeleteBookAsync(int id);
    }
}
