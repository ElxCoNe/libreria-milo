using LibreriaMilo.Data;
using LibreriaMilo.Models;
using LibreriaMilo.Response;
using Microsoft.EntityFrameworkCore;

namespace LibreriaMilo.Services;

public class LoanService
{
    private readonly MysqlDbContext _context;
    public LoanService(MysqlDbContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<Loan>> GetAllLoans()
    {
        var loans = _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .ToList();

        return new ServiceResponse<IEnumerable<Loan>>()
        {
            IsSuccess = true,
            Data = loans
        };
    }

    public ServiceResponse<Loan> GetLoanById(int id)
    {
        var loan = _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefault(l => l.Id == id);

        if (loan != null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = true,
                Data = loan
            };
        }

        return new ServiceResponse<Loan>()
        {
            IsSuccess = false,
            Message = "Loan no encontrado",
            Data = null
        };
    }

    public ServiceResponse<Loan> CreateLoan(Loan loan)
    {
        var user = _context.Users.Find(loan.UserId);

        if (user == null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "User no encontrado",
                Data = null
            };
        }

        var book = _context.Books.Find(loan.BookId);

        if (book == null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "Book no encontrado",
                Data = null
            };
        }

        if (book.Stock == 0)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "No hay stock del libro",
                Data = null
            };
        }

        book.Stock -= 1;

        if (book.Stock == 0)
        {
            book.Status = BookStatus.Loaned;
        }

        _context.Loans.Add(loan);
        _context.SaveChanges();

        return new ServiceResponse<Loan>()
        {
            IsSuccess = true,
            Data = loan,
            Message = "Prestamo creado correctamente"
        };
    }

    public ServiceResponse<Loan> UpdateLoan(int id, Loan loan)
    {
        var loanUpdate = _context.Loans.Find(id);

        if (loanUpdate == null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "Loan no encontrado",
                Data = null
            };
        }

        var user = _context.Users.Find(loan.UserId);

        if (user == null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "User no encontrado",
                Data = null
            };
        }

        var oldBook = _context.Books.Find(loanUpdate.BookId);
        var newBook = _context.Books.Find(loan.BookId);

        if (newBook == null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "Book no encontrado",
                Data = null
            };
        }

        if (loanUpdate.BookId != loan.BookId)
        {
            if (newBook.Stock == 0)
            {
                return new ServiceResponse<Loan>()
                {
                    IsSuccess = false,
                    Message = "No hay stock del nuevo libro seleccionado",
                    Data = null
                };
            }

            if (oldBook != null)
            {
                oldBook.Stock += 1;

                if (oldBook.Stock > 0)
                {
                    oldBook.Status = BookStatus.Available;
                }
            }

            newBook.Stock -= 1;

            if (newBook.Stock == 0)
            {
                newBook.Status = BookStatus.Loaned;
            }
        }

        loanUpdate.UserId = loan.UserId;
        loanUpdate.BookId = loan.BookId;
        loanUpdate.ReturnDate = loan.ReturnDate;

        _context.SaveChanges();

        return new ServiceResponse<Loan>()
        {
            IsSuccess = true,
            Data = loanUpdate,
            Message = "Prestamo actualizado correctamente"
        };
    }
    
    
    public ServiceResponse<Loan> DeleteLoan(int id)
    {
        var loan = _context.Loans.Find(id);

        if (loan == null)
        {
            return new ServiceResponse<Loan>()
            {
                IsSuccess = false,
                Message = "Loan no encontrado",
                Data = null
            };
        }

        var book = _context.Books.Find(loan.BookId);

        if (book != null)
        {
            book.Stock += 1;

            if (book.Stock > 0)
            {
                book.Status = BookStatus.Available;
            }
        }

        _context.Loans.Remove(loan);
        _context.SaveChanges();

        return new ServiceResponse<Loan>()
        {
            IsSuccess = true,
            Data = loan,
            Message = "Prestamo eliminado correctamente"
        };
    }
}