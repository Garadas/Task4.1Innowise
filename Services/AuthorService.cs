using Microsoft.EntityFrameworkCore;
using quest5.Models;
using quest5.Resources;
using quest5.DataFolder;

namespace quest5.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly InMemoryData _data;

        public AuthorService(InMemoryData data)
        {
            _data = data;
        }

        public IEnumerable<Author> GetAll()
        {
            return _data.Authors.Include(a => a.Books).AsNoTracking().ToList();
        }

        public Author GetById(int id)
        {
            var author = _data.Authors.Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
                throw new KeyNotFoundException(ResourceHelper.Get("AuthorNotFound", id));

            return author;
        }

        public Author Create(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException(ResourceHelper.Get("AuthorRequired"));

            _data.Authors.Add(author);
            _data.SaveChanges();

            return author;
        }

        public bool Update(int id, Author author)
        {
            var existing = _data.Authors.Find(id);
            if (existing == null)
                throw new KeyNotFoundException(ResourceHelper.Get("AuthorNotFound", id));

            existing.Name = author.Name;
            existing.DateOfBirth = author.DateOfBirth;
            _data.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var author = _data.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            if (author == null)
                throw new KeyNotFoundException(ResourceHelper.Get("AuthorNotFound", id));

            _data.Authors.Remove(author);
            _data.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _data.Authors.Any(a => a.Id == id);
    }
}
