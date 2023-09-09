using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNet6.data;
using MyApiNet6.Models;

namespace MyApiNet6.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            var book = _mapper.Map<Book>(model);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task DeleteBookAsync(int id)
        {
            var deleteBook = _context.Books.SingleOrDefault(x => x.Id == id);
            if(deleteBook != null)
            {
                _context.Books.Remove(deleteBook);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookModel>> getAllBooksAsync()
        {
            var books = await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> getBookAsync(int id)
        {
            var book = await _context.Books!.FindAsync(id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task UpdateBookAsync(int id, BookModel model)
        {
            if(id == model.Id)
            {
                var updatebook = _mapper.Map<Book>(model);
                _context.Books.Update(updatebook);
                await _context.SaveChangesAsync();
            }
        }
    }
}
