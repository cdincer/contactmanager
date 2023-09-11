using System.ComponentModel.DataAnnotations;
using System.Xml;
using contactmanager.Entity;

namespace contactmanager
{
    public class Contact : IEntity
    {

        private readonly DateTime NullCheck = DateTime.Parse("0001-01-01T00:00:00");
        private readonly int UserBirthDateCheck = 14;

        #region Field Area
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

        #region Property Area
        public string Salutation
        {
            get { return _salutation; }
            set { _salutation = value; }
        }
        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }
        public string? DisplayName
        {
            get { return _displayname; }
            set { _displayname = value; }
        }
        public DateTime? BirthDate
        {
            get { return _birthddate; }
            set { _birthddate = value; }
        }
        public DateTime CreationTimeStamp
        {
            get { return _creationTimeStamp; }
        }
        public DateTime LastChangeTimestamp
        {
            get { return _lastChangeTimeStamp; }
        }
        public bool NotifyHasBirthDaySoon
        {
            get
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
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
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