using quest5.DataFolder;
using quest5.Models;
using quest5.Resources;
using Microsoft.EntityFrameworkCore;


namespace quest5.Services
{
    public class BookService : IBookService
    {
        private readonly InMemoryData _context;

        public BookService(InMemoryData context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.AsNoTracking().ToList();
        }

        public Book GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                throw new KeyNotFoundException(ResourceHelper.Get("BookNotFound", id));

            return book;
        }

        public Book Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException(ResourceHelper.Get("InvalidBookTitle"));

            if (!_context.Authors.Any(a => a.Id == book.AuthorId))
                throw new ArgumentException(ResourceHelper.Get("AuthorNotFound", book.AuthorId));

            _context.Books.Add(book);
            _context.SaveChanges();

            return book;
        }

        public void Update(int id, Book book)
        {
            var existing = _context.Books.Find(id);
            if (existing == null)
                throw new KeyNotFoundException(ResourceHelper.Get("BookNotFound", id));

            existing.Title = book.Title;
            existing.PublishedYear = book.PublishedYear;
            existing.AuthorId = book.AuthorId;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                throw new KeyNotFoundException(ResourceHelper.Get("BookNotFound", id));

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public IEnumerable<object> GetAuthorsWithBookCount()
        {
            return _context.Authors
                .Include(a => a.Books)
                .Select(a => new
                {
                    a.Name,
                    BooksCount = a.Books.Count
                })
                .ToList();
        }

        public IEnumerable<Book> GetBooksAfterYear(int year)
        {
            return _context.Books
                .Where(b => b.PublishedYear != null && b.PublishedYear > year)
                .ToList();
        }

        public IEnumerable<Author> FindAuthorsByName(string name)
        {
            return _context.Authors
                .Include(a => a.Books)
                .Where(a => a.Name.Contains(name))
                .ToList();
        }
    }
}