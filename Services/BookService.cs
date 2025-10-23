using quest5.Models;
using quest5.Resources;

namespace quest5.Services
{
    public class BookService : IBookService
    {
        private readonly InMemoryData _data;
        private readonly IAuthorService _authors;

        public BookService(InMemoryData data, IAuthorService authors)
        {
            _data = data;
            _authors = authors;
        }

        public IEnumerable<Book> GetAll() => _data.Books;

        public Book? GetById(int id) => _data.Books.FirstOrDefault(b => b.Id == id);

        public Book Create(Book book)
        {
            if (!_authors.Exists(book.AuthorId))
                throw new ArgumentException(ResourceHelper.Get("AuthorNotFound", book.AuthorId));

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException(ResourceHelper.Get("InvalidBookTitle"));

            book.Id = _data.GetNextBookId();
            _data.Books.Add(book);
            return book;
        }

        public bool Update(int id, Book book)
        {
            var existing = _data.Books.FirstOrDefault(b => b.Id == id);
            if (existing == null) return false;
            if (!_authors.Exists(book.AuthorId))
                throw new ArgumentException(ResourceHelper.Get("AuthorNotFound", book.AuthorId));

            existing.Title = book.Title;
            existing.PublishedYear = book.PublishedYear;
            existing.AuthorId = book.AuthorId;
            return true;
        }

        public bool Delete(int id)
        {
            var book = _data.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return false;

            _data.Books.Remove(book);
            return true;
        }
    }
}
