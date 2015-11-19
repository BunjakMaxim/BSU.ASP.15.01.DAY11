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
            BookListService booksBinaryReader = new BookListService(new BinaryReaderRepository("BooksList.dat"));

            Console.WriteLine("BinaryReaderRepository:");
            foreach (Book b in booksBinaryReader)
                Console.WriteLine(b);
            Console.WriteLine();

            Console.WriteLine("BinaryReaderRepository sort:");
            booksBinaryReader.SortBooksByTag(new ComparableBook());
            foreach (Book b in booksBinaryReader)
                Console.WriteLine(b);
            Console.WriteLine();

            Console.WriteLine("XmlRepository:");
            BookListService booksXml = new BookListService(new XmlRepository("BooksList.xml"));
            foreach (Book b in booksXml)
                Console.WriteLine(b);
            Console.WriteLine();

            Console.WriteLine("Linq2XmlRepository:");
            BookListService booksLinq2Xml = new BookListService(new Linq2XmlRepository("BooksList.xml"));
            foreach (Book b in booksLinq2Xml)
                Console.WriteLine(b);
            
            Console.ReadKey();
        }
    }
}
