using System.ComponentModel.DataAnnotations;

namespace LibreriaMilo.Models;

public class User
{
    public int Id { get; set; }
    
    [Required, MaxLength(125)]
    public string Name { get; set; }
    
    [Required, MaxLength(125)]
    public string LastName { get; set; }
    
    [Required, MaxLength(20)]
    public string Telephone { get; set; }
    
    [Required, MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    //Navigation
    public ICollection<Loan>? Loans { get; set; }
}