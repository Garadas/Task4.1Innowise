using quest5.Models;
using quest5.Repositories;
using quest5.Services;

namespace quest5.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _books;

        public AuthorService(IAuthorRepository books)
        {
            _books = books;
        }

        public async Task<IEnumerable<Author>> GetAllAsync() =>
            await _books.GetAllAsync();

        public async Task<Author> GetByIdAsync(int id)
        {
            var author = await _books.GetByIdAsync(id);
            if (author == null)
                throw new KeyNotFoundException($"Author with ID {id} not found.");

            return author;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("Author name cannot be empty.");

            await _books.AddAsync(author);
            return author;
        }

        public async Task UpdateAsync(int id, Author author)
        {
            var existing = await _books.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Author with ID {id} not found.");

            existing.Name = author.Name;
            existing.DateOfBirth = author.DateOfBirth;
            await _books.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _books.GetByIdAsync(id);
            if (author == null)
                throw new KeyNotFoundException($"Author with ID {id} not found.");

            await _books.DeleteAsync(author);
        }
    }
}