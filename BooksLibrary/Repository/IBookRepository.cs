using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> Read();
        void Seve(IEnumerable<Book> iter);
    }
}
