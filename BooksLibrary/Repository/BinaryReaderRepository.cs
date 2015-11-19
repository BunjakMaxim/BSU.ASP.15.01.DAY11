using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Repository
{
    public class BinaryReaderRepository : IBookRepository
    {
        private readonly string path;

        public BinaryReaderRepository(string path)
        {
            this.path = path;
        }

        public IEnumerable<Book> Read()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string autor = reader.ReadString();
                    string title = reader.ReadString();
                    int year = reader.ReadInt32();
                    string genre = reader.ReadString();

                    yield return new Book() { Author = autor, Title = title, Year = year, Genre = genre };
                }
                reader.Close();
            }
        }

        public void Seve(IEnumerable<Book> iter)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                writer.Write(iter.Count());

                foreach (Book b in iter)
                {
                    writer.Write(b.Author);
                    writer.Write(b.Title);
                    writer.Write(b.Year);
                    writer.Write(b.Genre);
                }
            }
        }
    }
}
