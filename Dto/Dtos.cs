using System.ComponentModel.DataAnnotations;

namespace contactmanager.Dto
{
    public record CreateUserDto(
     [Required, MinLength(2, ErrorMessage = "Salutation field must be longer than 2 characters long")] string Salutation,
     [Required, MinLength(2, ErrorMessage = "Firstname field must be longer than 2 characters long")] string Firstname,
     [Required, MinLength(2, ErrorMessage = "Lastname field must be longer than 2 characters long")] string Lastname,
     [EmailAddress, Required] string Email,
     string? Displayname,
     DateTime? Birthdate,
     string? PhoneNumber
 );

    public record RetrieveUserDto(
    Guid Id,
    string Salutation,
    string Firstname,
    string Lastname,
    string Displayname,
    DateTime Birthdate,
    DateTime CreationTimestamp,
    DateTime LastChangeTimestamp,
    bool Notifyhasbirthdaysoon,
    string Email,
    string Phonenumber
);

    public record ListUserDto(
    Guid Id,
    string Salutation,
    string Firstname,
    string Lastname,
    string Displayname,
    DateTime Birthdate,
    DateTime CreationTimestamp,
    DateTime LastChangeTimestamp,
    string Email,
    string Phonenumber
)
    {
        public bool Notifyhasbirthdaysoon { get; set; }
    };

    public record UpdateUserDto(
    Guid Id,
    [Required, MinLength(2, ErrorMessage = "Salutation field must be longer than 2 characters long")] string Salutation,
    [Required, MinLength(2, ErrorMessage = "Firstname field must be longer than 2 characters long")] string Firstname,
    [Required, MinLength(2, ErrorMessage = "Lastname field must be longer than 2 characters long")] string Lastname,
    [EmailAddress, Required] string Email,
    string Displayname,
    DateTime Birthdate,
    string Phonenumber
    );

}