using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BooksLibrary.Repository
{
    public class XmlRepository : IBookRepository
    {
        private readonly string path;

        public XmlRepository(string path)
        {
            this.path = path;
        }

        public IEnumerable<Book> Read()
        {
            string author = "", title = "", genre = "";

            XmlReader reader = XmlReader.Create(path);

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "autor":
                            author = reader.ReadString();
                            break;
                        case "title":
                            title = reader.ReadString();
                            break;
                        case "genre":
                            genre = reader.ReadString();
                            break;
                        case  "year":
                            yield return new Book() { Author = author, Title = title, Genre = genre, Year = int.Parse(reader.ReadString())};
                            break;
                    }
                }
            }
        }

        public void Seve(IEnumerable<Book> array)
        {
            using (var xml = new XmlTextWriter(path, Encoding.UTF8))
            {
                xml.Formatting = Formatting.Indented;
                xml.Indentation = 2;

                xml.WriteStartDocument();
                xml.WriteStartElement("library");

                foreach (var b in array)
                {
                    xml.WriteStartElement("book");
                    xml.WriteElementString("autor", b.Author);
                    xml.WriteElementString("title", b.Title);
                    xml.WriteElementString("genre", b.Genre);
                    xml.WriteElementString("year", b.Year.ToString());
                    xml.WriteEndElement();

                }

                xml.WriteEndElement();
                xml.WriteEndDocument();
                xml.Flush();
                xml.Close();
            }
        }
    }
}
