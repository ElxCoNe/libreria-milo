namespace LibreriaMilo.Models;

public enum LoanStatus
{
    Active,
    Returned,
    Overdue
}

public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }

    public DateTime DateLoan { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public LoanStatus Status { get; set; } = LoanStatus.Active;

    // Navigation
    public User? User { get; set; }
    public Book? Book { get; set; }
}