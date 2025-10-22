using quest5.Models;

namespace quest5.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly InMemoryData _data;

        public AuthorService(InMemoryData data)
        {
            _data = data;
        }

        public IEnumerable<Author> GetAll() => _data.Authors;

        public Author? GetById(int id) => _data.Authors.FirstOrDefault(a => a.Id == id);

        public Author Create(Author author)
        {
            author.Id = _data.GetNextAuthorId();
            _data.Authors.Add(author);
            return author;
        }

        public bool Update(int id, Author author)
        {
            var existing = _data.Authors.FirstOrDefault(a => a.Id == id);
            if (existing == null) return false;

            existing.Name = author.Name;
            existing.DateOfBirth = author.DateOfBirth;
            return true;
        }

        public bool Delete(int id)
        {
            var author = _data.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return false;

            _data.Books.RemoveAll(b => b.AuthorId == id);
            _data.Authors.Remove(author);
            return true;
        }

        public bool Exists(int id) => _data.Authors.Any(a => a.Id == id);
    }
}
