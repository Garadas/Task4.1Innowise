using quest5.Models;

namespace quest5.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        Book Create(Book book);
        bool Update(int id, Book book);
        bool Delete(int id);
    }
}
