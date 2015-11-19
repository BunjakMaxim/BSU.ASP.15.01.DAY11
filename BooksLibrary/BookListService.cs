using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibrary.Repository;

namespace BooksLibrary
{
    public class BookListService  : IEnumerable<Book>
    {
        private Book[] arrayBooks;
        public IBookRepository repository;//--------------------

        public int Count { get; set; }
        
        public BookListService(IBookRepository r)
        {
            repository = r;
            Count = 0;
            ReadeRepository();
        }
        
        public void AddBook(Book book)
        {
            if (IndexBookOfArray(book) != -1)
                throw new Exception("Повторное добавление книги!");

            if (arrayBooks.Length + 1 == Count)
            {
                Book[] array = new Book[Count * 2];
                Array.Copy(arrayBooks, array, Count);
                arrayBooks = array;
            }

            arrayBooks[Count++] = book;
        }
        
        public void RemoveBook(Book book)
        {
            int index = IndexBookOfArray(book);

            if (index < 0)
                throw new Exception("Книга для удаления не  найдена");

            for (int i = index; i < Count - 1; i++)
                arrayBooks[i] = arrayBooks[i + 1];
            Count--;
        }

        public void SortBooksByTag(IComparer<Book> c)
        {
            if (c == null)
                throw new ArgumentNullException();

            int end = Count - 1;
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < end; j++)
                    if (c.Compare(arrayBooks[j], arrayBooks[j + 1]) > 0)
                    {
                        Book b = arrayBooks[j];
                        arrayBooks[j] = arrayBooks[j + 1];
                        arrayBooks[j + 1] = b;
                    }
                end--;
            }
        }

        public IEnumerator<Book> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return arrayBooks[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int IndexBookOfArray(Book item)
        {
            for (int i = 0; i < Count; i++)
                if (arrayBooks[i].Equals(item))
                    return i;

            return -1;
        }

        private void ReadeRepository()
        {
            arrayBooks = new Book[10];
            Count = 0;
            foreach (var book in repository.Read())
                AddBook(book);
        }

        public void SaveRepository() //-----------------------------------------
        {
            repository.Seve(this);
        }
    }
}
