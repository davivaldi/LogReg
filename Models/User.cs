using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogReg.Models
{

  
    public class User
    {
    [Key]
    public int UserId{get;set;}

    [Required]
    [EmailAddress]
    [MinLength(3, ErrorMessage = "Must be at least 2 characters long")]
    public string Email {get; set;}

    [Required]
    [MinLength(8, ErrorMessage = "Must be at least 8 characters long")]
    [DataType(DataType.Password)]
    public string Password {get;set;}
    
    // [NotMapped]
    // [Compare("Password")]
    // // [DataType(DataType.Password)]
    // public string Confirm {get;set;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    }
}