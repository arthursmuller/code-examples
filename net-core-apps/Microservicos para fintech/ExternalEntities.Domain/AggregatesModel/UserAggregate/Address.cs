using System;
using System.Collections.Generic;
using ExternalEntities.Domain.Abstractions;

namespace ExternalEntities.Domain.AggregatesModel.UserAggregate
{
    public class Address : ValueObject
    {
        public String City { get; set; }
        public String Cep { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public String Street { get; set; }
        public Int32? Number { get; set; }
        public String Complement { get; set; }
        public String Neighborhood { get; set; }
        public String ResidenceType { get; set; }

        public Address(String city, String cep, String state, String country, String street, Int32? number, String complement, String neighborhood, String residenceType) 
            => (City, Cep, State, Country, Street, Number, Complement, Neighborhood, ResidenceType) = (city, cep, state, country, street, number, complement, neighborhood, residenceType);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return City;
            yield return State;
            yield return Country;
            yield return Number;
            yield return Complement;
            yield return Neighborhood;
        }

        public bool IsCompleted()
            => 
            /*!string.IsNullOrEmpty(Cep) &&*/
            !string.IsNullOrEmpty(City) &&
            !string.IsNullOrEmpty(Neighborhood) &&
            !string.IsNullOrEmpty(Street) &&
            Number != 0;
    }
}

