using LibreriaMilo.Data;
using LibreriaMilo.Response;
using LibreriaMilo.Models;

namespace LibreriaMilo.Services;

public class BookService
{
    private readonly MysqlDbContext _context;
    
    public BookService(MysqlDbContext context)
    {
       _context = context;
    }

    public ServiceResponse<IEnumerable<Book>> GetAllBooks()
    {
        var books = _context.Books.ToList();

        return new ServiceResponse<IEnumerable<Book>>()
        {
            IsSuccess = true,
            Data = books
        };
    }

    public ServiceResponse<Book> SaveBook(Book book)
    {
        _context.Books.Add(book);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            return new ServiceResponse<Book>()
            {
                IsSuccess = true,
                Data = book,
                Message = "Libro creado correctamente"
            };
        }

        return new ServiceResponse<Book>()
        {
            IsSuccess = false,
            Message = "Libro no encontrado",
            Data = book
        };
    }

    public ServiceResponse<Book> GetBookById(int id)
    {
        var book = _context.Books.Find(id);

        if (book != null)
        {
            return new ServiceResponse<Book>()
            {
                IsSuccess = true,
                Data = book,
                Message = "Libro encontrado"
            };
        }

        return new ServiceResponse<Book>()
        {
            IsSuccess = false,
            Message = "Libro no encontrado",
            Data = null
        };
    }

    public ServiceResponse<Book> DeleteBook(int id)
    {
        var bookDeleted = _context.Books.Find(id);
        if (bookDeleted != null)
        {
            _context.Books.Remove(bookDeleted);
            _context.SaveChanges();
            
            return new ServiceResponse<Book>()
            {
                IsSuccess = true,
                Data = bookDeleted,
                Message = "Libro eliminado correctamente"
            };
        }

        return new ServiceResponse<Book>()
        {
            IsSuccess = false,
            Message = "Libro no encontrado",
            Data = null
        };

    }

    public ServiceResponse<Book> UpdateBook(int id, Book book)
    {
        var updateBook = _context.Books.Find(id);
        if (updateBook != null)
        {
            updateBook.Title = book.Title;
            updateBook.Author = book.Author;
            updateBook.Quantity = book.Quantity;
            updateBook.Category = book.Category;
            
            _context.SaveChanges();

            return new ServiceResponse<Book>()
            {
                IsSuccess = true,
                Message = "Libro correctamente actualizado",
                Data = updateBook
            };
        }

        return new ServiceResponse<Book>()
        {
            IsSuccess = false,
            Message = "Libro no encontrado",
            Data = null
        };

    }
    
}