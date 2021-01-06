using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class Contact
    {    
        [Key]
    public int ID { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required(ErrorMessage = "Enter Your First Name")]
    [StringLength(50, ErrorMessage = "First Name should be less than or equal to Fifty characters.")]
    public string FirstName { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    [Required(ErrorMessage = "Enter Your Last Name")]
    [StringLength(50, ErrorMessage = "Last Name should be less than or equal to Fifty characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Enter Your EMail ID")]
    [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "You must provide a Mobile/Phone Number")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile/Phone number")]
    public string PhoneNumber { get; set; }

    public bool Status { get; set; }

        //    public int ID { get; set; }

        //    public string FirstName { get; set; }

        //    public string LastName { get; set; }

        //    public string Email { get; set; }

        //    public string PhoneNumber { get; set; }

        //    public bool Status { get; set; }


    }
}
