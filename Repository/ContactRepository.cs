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
        private readonly int UserBirthDateCheck = 14;
        private readonly DateTime NullCheck = DateTime.Parse("0001-01-01T00:00:00");

        public ContactRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> CreateAsync(Contact entity)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    (@"INSERT INTO Contact (Id, Salutation, Firstname , Lastname , 
                    Displayname, Birthdate, CreationTimestamp, LastChangeTimestamp, 
                    NotifyHasBirthdaySoon, Email, Phonenumber)
                    VALUES (@Id, @Salutation, @Firstname , @Lastname , 
                    @Displayname, @Birthdate, @CreationTimestamp, @LastChangeTimestamp, 
                    @NotifyHasBirthdaySoon, @Email, @Phonenumber)",
                            new
                            {
                                id = entity.Id,
                                Salutation = entity.GetSalutation(),
                                FirstName = entity.GetFirstName(),
                                Lastname = entity.GetLastName(),
                                Displayname = entity.GetDisplayName(),
                                Birthdate = entity.GetBirthDate(),
                                CreationTimestamp = entity.GetCreationTimeStamp(),
                                LastChangeTimestamp = entity.GetLastChangeTimestamp(),
                                NotifyHasBirthdaySoon = entity.GetNotifyHasBirthDaySoon(),
                                Email = entity.GetEmail(),
                                Phonenumber = entity.GetPhoneNumber()
                            });

            if (affected == 0)
                return false;

            return true;
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
                                user.Salutation,
                                user.Firstname,
                                user.Lastname,
                                user.Displayname,
                                user.Birthdate,
                                user.CreationTimestamp,
                                user.LastChangeTimestamp,
                                user.Email,
                                user.Phonenumber);

                Contact.Notifyhasbirthdaysoon = NotifyBirthDateCheck(user.Birthdate); ;
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
                                Contact.Salutation,
                                Contact.Firstname,
                                Contact.Lastname,
                                Contact.Displayname,
                                Contact.Birthdate,
                                Contact.CreationTimestamp,
                                Contact.LastChangeTimestamp,
                                Contact.Email,
                                Contact.Phonenumber);

            DateTime UserBirthDate = Contact.Birthdate;

            ReturnedContact.Notifyhasbirthdaysoon = NotifyBirthDateCheck(UserBirthDate);

            return ReturnedContact;
        }

        public async Task<bool> UpdateAsync(Contact entity)
        {
            using var connection = new NpgsqlConnection
          (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    (@"UPDATE Contact SET Salutation = @Salutation, Firstname =  @Firstname, 
                    Lastname = @Lastname , Displayname = @Displayname, Birthdate = @Birthdate, 
                    NotifyHasBirthdaySoon= @NotifyHasBirthdaySoon, Email = @Email,
                    Phonenumber = @Phonenumber, LastChangeTimestamp = @LastChangeTimestamp
                    WHERE Id = @Id",
                            new
                            {
                                Salutation = entity.GetSalutation(),
                                FirstName = entity.GetFirstName(),
                                Lastname = entity.GetLastName(),
                                Displayname = entity.GetDisplayName(),
                                Birthdate = entity.GetBirthDate(),
                                NotifyHasBirthdaySoon = entity.GetNotifyHasBirthDaySoon(),
                                Email = entity.GetEmail(),
                                Phonenumber = entity.GetPhoneNumber(),
                                LastChangeTimeStamp = DateTime.Now,
                                id = entity.Id,
                            });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Contact WHERE Id = @Id",
                new { Id = id });

            if (affected == 0)
                return false;

            return true;
        }

        private bool NotifyBirthDateCheck(DateTime UserBirthDate)
        {
            bool birthDayCalc = false;

            if (UserBirthDate != NullCheck)
            {
                int YearAdjustment = DateTime.Now.Year - UserBirthDate.Year;
                DateTime CurrBirthDate = UserBirthDate.AddYears(YearAdjustment);
                DateTime checkBirthDayEndDate = DateTime.Now.AddDays(UserBirthDateCheck);
                if (CurrBirthDate >= DateTime.Now && CurrBirthDate <= checkBirthDayEndDate)
                {
                    birthDayCalc = true;
                }

            }
            return birthDayCalc;
        }

    }
}