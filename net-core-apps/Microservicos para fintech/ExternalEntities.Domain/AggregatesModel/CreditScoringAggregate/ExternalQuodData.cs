using ExternalEntities.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class ExternalQuodData : ValueObject
    {
        public ExternalQuodScore Score { get; set; }
        public ExternalQuodNegative Negative { get; set; }
        public List<ExternalQuodProtest> Protests { get; set; }
        public ExternalQuodAddress CurrentAddress { get; set; }
        public List<ExternalQuodAddress> Addresses { get; set; }
        public List<ExternalQuodEmail> Emails { get; set; }
        public List<ExternalQuodPhoneNumber> PhoneNumbers { get; set; }
        public List<ExternalQuodPhoneNumber> MobilePhoneNumbers { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool? PublicExposed { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
            yield return Name;
            yield return Gender;
            yield return Score;
            yield return Negative;
            yield return Emails;
            yield return PhoneNumber;
            yield return MobilePhoneNumber;
            yield return Protests;
            yield return Addresses;
            yield return PhoneNumbers;
            yield return BirthDate;
            yield return MobilePhoneNumbers;
            yield return CurrentAddress;
        }
    }

    public class ExternalQuodNegative : ValueObject
    {
        public ExternalQuodNegative(decimal pendenciesControlCred)
        {
            PendenciesControlCred = pendenciesControlCred;
        }

        public decimal PendenciesControlCred { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PendenciesControlCred;
        }
    }
     
    public class ExternalQuodEmail : ValueObject
    {
        public ExternalQuodEmail(string email, DateTime lastSeen)
        {
            Email = email;
            LastSeen = lastSeen;
        }

        public string Email { get; set; }
        public DateTime LastSeen { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
            yield return LastSeen;
        }
    }

    public class ExternalQuodPhoneNumber : ValueObject
    {
        public ExternalQuodPhoneNumber(string email, DateTime lastSeen)
        {
            Email = email;
            LastSeen = lastSeen;
        }

        public string Email { get; set; }
        public DateTime LastSeen { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
            yield return LastSeen;
        }
    }

    public class ExternalQuodAddress : ValueObject
    {
        public ExternalQuodAddress(string street, string number, string complement, string neighborhood, string city, string state, string postalCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        private ExternalQuodAddress() { }

        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return Complement;
            yield return Complement;
            yield return Neighborhood;
            yield return City;
            yield return State;
            yield return PostalCode;
        }
    }
}
