namespace matelso.contactmanager.Dto
{
    public record CreateUserDto(
     string Salutation,
     string Firstname,
     string Lastname,
     string Email,
     string? Displayname,
     DateTime? Birthddate,
     string? PhoneNumber
 );

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