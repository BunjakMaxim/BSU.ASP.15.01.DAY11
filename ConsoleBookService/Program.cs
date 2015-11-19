using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibrary.Repository;
using BooksLibrary;

namespace ConsoleBookService
{
    class Program
    {
        static void Main(string[] args)
        {
            BookListService books = new BookListService(new BinaryReaderRepository("BooksList.dat"));

            books.AddBook(new Book() { Author = "Николай Гоголь", Title = "Вечера на хуторе близ Диканьки", Year = 1832, Genre = "Проза" });
            foreach (Book b in books)
                Console.WriteLine(b);

            Console.WriteLine(); Console.WriteLine();

            books.RemoveBook(new Book() { Author = "Николай Гоголь", Title = "Вечера на хуторе близ Диканьки", Year = 1832, Genre = "Проза" });
            foreach (Book b in books)
                Console.WriteLine(b);

            books.SortBooksByTag(new ComparableBook());
            foreach (Book b in books)
                Console.WriteLine(b);

            books = new BookListService(new XmlRepository("BooksList.xml"));

            Console.ReadKey();
        }
    }
}
