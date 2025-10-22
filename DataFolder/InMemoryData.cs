using quest5.Models;

namespace quest5.Services
{  
    public class InMemoryData
    {
    public List<Author> Authors { get; } = new();
    public List<Book> Books { get; } = new();

    private int _nextAuthorId = 1;
    private int _nextBookId = 1;

        public InMemoryData()
        {
            Authors.Add(new Author { Name = "Agata Kristy", DateOfBirth = new DateTime(1890, 9, 15) });
            Authors.Add(new Author { Name = "Fyodor Dostoevsky", DateOfBirth = new DateTime(1821, 11, 11) });

            Books.Add(new Book { Title = "Secret Adversary", PublishedYear = 1922, AuthorId = 1 });
            Books.Add(new Book { Title = "Postern of Fate", PublishedYear = 1973, AuthorId = 1 });
            Books.Add(new Book { Title = "Crime and Punishment", PublishedYear = 1866, AuthorId = 2 });
        }

        public int GetNextAuthorId() => _nextAuthorId++;
        public int GetNextBookId() => _nextBookId++;
    }
}
