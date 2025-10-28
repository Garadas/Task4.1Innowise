using quest5.Models;
using quest5.Services;
using quest5.Repositories;

namespace quest5.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _books;

        public BookService(IBookRepository books)
        {
            _books = books;
        }

        public async Task<IEnumerable<Book>> GetAllAsync() =>
            await _books.GetAllAsync();

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await _books.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found.");

            return book;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Book title cannot be empty.");

            await _books.AddAsync(book);
            return book;
        }

        public async Task UpdateAsync(int id, Book book)
        {
            var existing = await _books.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Book with ID {id} not found.");

            existing.Title = book.Title;
            existing.PublishedYear = book.PublishedYear;
            existing.AuthorId = book.AuthorId;

            await _books.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _books.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Book with ID {id} not found.");

            await _books.DeleteAsync(existing);
        }
    }
}