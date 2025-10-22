using quest5.Models;

namespace quest5.Services
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        Author Create(Author author);
        bool Update(int id, Author author);
        bool Delete(int id);
        bool Exists(int id);
    }
}
