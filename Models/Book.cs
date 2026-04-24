using System.ComponentModel.DataAnnotations;

namespace LibreriaMilo.Models;


public enum BookStatus
{
    Available,
    Loaned,
    Maintenance
}

public class Book
{
    public int Id { get; set; }
    
    [Required, MaxLength(150)]
    public string Title { get; set; }
    
    [Required, MaxLength(150)]
    public string Author { get; set; }
    
    public int Quantity { get; set; }
    
    public int Stock { get; set; }
    
    [Required, MaxLength(150)]
    public string Category { get; set; }
    
    public BookStatus Status { get; set; } =  BookStatus.Available;
    
    //Navigation
    public ICollection<Loan>? Loans { get; set; }
}
