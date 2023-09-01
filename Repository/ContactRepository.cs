using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using Dapper;
using matelso.contactmanager.Dto;
using matelso.contactmanager.Entity;
using Npgsql;

namespace matelso.contactmanager.Repository
{


    public class ContactRepository : IRepository
    {
        private readonly IConfiguration _configuration;

        public ContactRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateAsync(Contact entity)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Contact (Id,Salutation, Firstname , Lastname , Displayname,"
                                        + "Birthdate,CreationTimestamp,NotifyHasBirthdaySoon,Email,Phonenumber)"
                    + "VALUES (@Id,@Salutation, @Firstname , @Lastname ,@Displayname,@Birthdate,"
                    + "@CreationTimestamp, @NotifyHasBirthdaySoon,@Email,@Phonenumber)",
                            new
                            {
                                id = entity.Id,
                                Salutation = entity.GetSalutation(),
                                FirstName = entity.GetFirstName(),
                                Lastname = entity.GetLastName(),
                                Displayname = entity.GetDisplayName(),
                                Birthdate = entity.GetBirthDate(),
                                CreationTimestamp = entity.GetCreationTimeStamp(),
                                NotifyHasBirthdaySoon = entity.GetNotifyHasBirthDaySoon(),
                                Email = entity.GetEmail(),
                                Phonenumber = entity.GetPhoneNumber()
                            });
        }

        public async Task<IEnumerable<ListUserDto>> GetAllAsync()
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var Contacts = await connection.QueryAsync<RetrieveUserDto>
                    ("SELECT * FROM Contact");

            List<ListUserDto> ReturnedContracts = new();

            foreach (RetrieveUserDto user in Contacts)
            {
                ListUserDto Contact = new(user.Id,
                                user.salutation,
                                user.firstname,
                                user.lastname,
                                user.displayname,
                                user.birthdate,
                                user.creationtimestamp,
                                user.email,
                                user.phonenumber);
                if (user.birthdate != null)
                {
                    DateTime UserBirthDate = user.birthdate;
                    bool birthDayCalc = false;
                    DateTime checkBirthDayEndDate = DateTime.Now.AddDays(14);
                    if (UserBirthDate >= DateTime.Now && UserBirthDate <= checkBirthDayEndDate)
                    {
                        birthDayCalc = true;
                    }
                    Contact.notifyhasbirthdaysoon = birthDayCalc;
                }
                ReturnedContracts.Add(Contact);

            }

            return ReturnedContracts;
        }
        public async Task<ListUserDto> GetAsync(Guid id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var Contact = await connection.QueryFirstAsync<RetrieveUserDto>
                    ("SELECT * FROM Contact where Id = @Id", new { Id = id });

            ListUserDto ReturnedContact = new(Contact.Id,
                                Contact.salutation,
                                Contact.firstname,
                                Contact.lastname,
                                Contact.displayname,
                                Contact.birthdate,
                                Contact.creationtimestamp,
                                Contact.email,
                                Contact.phonenumber);

            DateTime UserBirthDate = Contact.birthdate;
            bool birthDayCalc = false;
            DateTime checkBirthDayEndDate = DateTime.Now.AddDays(14);
            if (UserBirthDate >= DateTime.Now && UserBirthDate <= checkBirthDayEndDate)
            {
                birthDayCalc = true;
            }
            ReturnedContact.notifyhasbirthdaysoon = birthDayCalc;

            return ReturnedContact;
        }

        public Task UpdateAsync(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}