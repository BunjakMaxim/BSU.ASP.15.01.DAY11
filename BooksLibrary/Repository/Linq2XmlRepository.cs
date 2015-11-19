using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BooksLibrary.Repository
{
    public class Linq2XmlRepository : IBookRepository
    {
        private readonly string path;

        public Linq2XmlRepository(string path)
        {
            this.path = path;
        }

        public IEnumerable<Book> Read()
        {
            XDocument doc = XDocument.Load(path);
            string author = "", title = "", genre = "";

            foreach (XElement el in doc.Root.Elements())
                foreach (XElement element in el.Elements())
                    switch (element.Name.ToString())
                    {
                        case "author":
                            author = element.Value;
                            break;
                        case "title":
                            title = element.Value;
                            break;
                        case "genre":
                            genre = element.Value;
                            break;
                        case "year":
                            yield return new Book() { Author = author, Title = title, Genre = genre, Year = int.Parse(element.Value) };
                            break;
                    }                
        }

        public void Seve(IEnumerable<Book> iter)
        {
            XDocument doc = new XDocument();
            XElement library = new XElement("library");
            doc.Add(library);

            foreach (Book b in iter)
            {
                XElement book = new XElement("book");

                XElement author = new XElement("author");
                author.Value = b.Author;
                book.Add(author);
                
                XElement title = new XElement("title");
                title.Value = b.Title;
                book.Add(title);

                XElement genre = new XElement("genre");
                genre.Value = b.Genre;
                book.Add(genre);

                XElement year = new XElement("year");
                year.Value = b.Year.ToString();
                book.Add(year);

                doc.Root.Add(book);
            }

            doc.Save(path);
        }
    }
}
