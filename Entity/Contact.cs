using System.ComponentModel.DataAnnotations;
using System.Xml;
using contactmanager.Entity;

namespace contactmanager
{
    public class Contact : IEntity
    {

        private readonly DateTime NullCheck = DateTime.Parse("0001-01-01T00:00:00");
        private readonly int UserBirthDateCheck = 14;

        #region Property Definitions
        public Guid Id { get; set; }
        private string _salutation;
        private string _firstname;
        private string _lastname;
        private string? _displayname;
        private DateTime? _birthddate;
        private readonly DateTime _creationTimeStamp;
        private readonly DateTime _lastChangeTimeStamp;
        private bool _notifyHasBirthdaySoon;//14 days limit.
        private string _email;//Must be unique
        private string? _phoneNumber;
        #endregion

        #region Getter/Setter Area
        public string GetSalutation()
        {
            return _salutation;
        }
        public void SetSalutation(string Salutation)
        {
            _salutation = Salutation;
        }
        public string GetFirstName()
        {
            return _firstname;
        }
        public void SetFirstName(string FirstName)
        {
            _firstname = FirstName;
        }
        public string GetLastName()
        {
            return _lastname;
        }
        public void SetLastName(string LastName)
        {
            _lastname = LastName;
        }
        public string? GetDisplayName()
        {
            return _displayname;
        }
        public void SetDisplayName(string DisplayName)
        {
            _displayname = DisplayName;
        }
        public DateTime? GetBirthDate()
        {
            return _birthddate;
        }
        public void SetBirthDate(DateTime? BirthDate)
        {
            _birthddate = BirthDate;
        }
        public DateTime GetCreationTimeStamp()
        {
            return _creationTimeStamp;
        }
        public DateTime GetLastChangeTimestamp()
        {
            return _lastChangeTimeStamp;
        }
        public bool GetNotifyHasBirthDaySoon()
        {
            bool birthDayCalc = false;
            if (_birthddate != null)
            {
                int YearAdjustment = DateTime.Now.Year - _birthddate.Value.Year;
                DateTime CurrBirthDate = _birthddate.Value.AddYears(YearAdjustment);
                DateTime checkBirthDayEndDate = DateTime.Now.AddDays(UserBirthDateCheck);
                if (CurrBirthDate >= DateTime.Now && CurrBirthDate <= checkBirthDayEndDate)
                {
                    birthDayCalc = true;
                }
            }
            return birthDayCalc;
        }
        public void SetEmail(string Email)
        {
            _email = Email;
        }
        public string GetEmail()
        {
            return _email;
        }
        public void SetPhoneNumber(string PhoneNumber)
        {
            _phoneNumber = PhoneNumber;
        }
        public string? GetPhoneNumber()
        {
            return _phoneNumber;
        }

        #endregion

        public Contact(string salutation, string firstname, string lastname, string email,
        string? displayname = null, DateTime? birthdate = null
        , string? phoneNumber = null)
        {
            Id = Guid.NewGuid();
            _salutation = salutation;
            _firstname = firstname;
            _lastname = lastname;
            _displayname = string.IsNullOrWhiteSpace(displayname) ? firstname + " " + lastname : displayname;
            _birthddate = birthdate;
            _creationTimeStamp = DateTime.Now;
            _lastChangeTimeStamp = DateTime.Now;

            if (birthdate != NullCheck)
            {
                bool birthDayCalc = false;
                DateTime checkBirthDayEndDate = DateTime.Now.AddDays(UserBirthDateCheck); ;
                if (birthdate >= DateTime.Now && birthdate <= checkBirthDayEndDate)
                {
                    birthDayCalc = true;
                }
                _notifyHasBirthdaySoon = birthDayCalc;
            }
            _email = email;
            _phoneNumber = phoneNumber;
        }
    }

}