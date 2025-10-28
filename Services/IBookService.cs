using quest5.Models;

namespace quest5.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        Book Create(Book book);
        void Update(int id, Book book);
        void Delete(int id);

        IEnumerable<object> GetAuthorsWithBookCount();
        IEnumerable<Book> GetBooksAfterYear(int year);
        IEnumerable<Author> FindAuthorsByName(string name);
    }
}
