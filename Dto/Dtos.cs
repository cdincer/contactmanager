using System.ComponentModel.DataAnnotations;

namespace matelso.contactmanager.Dto
{
    public record CreateUserDto(
     [Required, MinLength(2, ErrorMessage = "Salutation field must be longer than 2 characters long")] string Salutation,
     [Required, MinLength(2, ErrorMessage = "Firstname field must be longer than 2 characters long")] string Firstname,
     [Required, MinLength(2, ErrorMessage = "Lastname field must be longer than 2 characters long")] string Lastname,
     [EmailAddress, Required] string Email,
     string? Displayname,
     DateTime? Birthddate,
     string? PhoneNumber
 );
    /*
     [Required]
            [MinLength(2, ErrorMessage = "Salutation field must be longer than 2 characters long")]
    */
    public record RetrieveUserDto(
    Guid Id,
    string salutation,
    string firstname,
    string lastname,
    string displayname,
    DateTime birthdate,
    DateTime creationtimestamp,
    bool notifyhasbirthdaysoon,
    string email,
    string phonenumber
);


    public record ListUserDto(
    Guid Id,
    string salutation,
    string firstname,
    string lastname,
    string displayname,
    DateTime birthdate,
    DateTime creationtimestamp,
    string email,
    string phonenumber
)
    {
        public bool notifyhasbirthdaysoon { get; set; }
    };

}