using Domain.Model;
using JeBalance.SQLLite.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeBalance.Common
{
    public static class Extensions
    {
        public static Person ToDomain(this PersonSQLS sqls)
        {
            return new Person
            {
                Id = sqls.Id,
                FirstName = sqls.FirstName,
                LastName = sqls.LastName,
                StreetNumber = sqls.StreetNumber,
                StreetName = sqls.StreetName,
                PostalCode = sqls.PostalCode,
                CityName = sqls.CityName,
                IsVIP = sqls.IsVIP,
                UserName = sqls.UserName,
                Rejection = sqls.Rejection,
                IsFisc = sqls.IsFisc,
                IsAdmin = sqls.IsAdmin,
                Email = sqls.Email
            };
        }

        public static PersonSQLS ToSQLS(this Person domain)
        {
            return new PersonSQLS
            {
                Id = domain.Id,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                UserName = domain?.UserName,
                StreetNumber = domain.StreetNumber,
                StreetName = domain.StreetName,
                PostalCode = domain.PostalCode,
                CityName = domain.CityName,
                IsVIP = domain.IsVIP,
                Email = domain.Email,
                IsAdmin = domain.IsAdmin,
                IsFisc = domain.IsFisc,
                Rejection = domain.Rejection,
            };
        }

        public static Denonciation ToDomain(this DenonciationSQLS sqls)
        {
            return new Denonciation
            {
                Id = sqls.Id,
                Timestamp = sqls.Timestamp,
                Informant = sqls.Informant.ToDomain(), 
                Suspect = sqls.Suspect.ToDomain(),
                Offense = sqls.Offense,
                EvasionCountry = sqls.EvasionCountry,
            };
        }

        public static DenonciationSQLS ToSQLS(this Denonciation domain)
        {
            return new DenonciationSQLS
            {
                Timestamp = domain.Timestamp,
                Informant = domain.Informant.ToSQLS(), 
                Suspect = domain.Suspect.ToSQLS(),
                Offense = domain.Offense,
                EvasionCountry = domain.EvasionCountry,
                                
            };
        }

        public static Response ToDomain(this ResponseSQLS sqls)
        {
            if (sqls == null) return null;

            return new Response
            {
                Id = sqls.Id,
                Timestamp = sqls.Timestamp,
                ResponseType = sqls.ResponseType,
                Amount = sqls.Amount,
                DenonciationId = sqls.DenonciationId,
                Denonciation = sqls.Denonciation?.ToDomain() 
            };
        }

        public static ResponseSQLS ToSQLS(this Response Person)
        {
            if (Person == null) return null;

            return new ResponseSQLS
            {
                Id = Person.Id,
                Timestamp = Person.Timestamp,
                ResponseType = Person.ResponseType,
                Amount = Person.Amount,
                DenonciationId = Person.DenonciationId,
                Denonciation = Person.Denonciation?.ToSQLS() 
            };
        }

        public static Calomniateur ToDomain(this CalomniateurSQLS sqls)
        {
            if (sqls == null) return null;

            return new Calomniateur
            {
                Id = sqls.Id,
                Person = sqls.Person?.ToDomain() 
            };
        }

        public static CalomniateurSQLS ToSQLS(this Calomniateur Person)
        {
            if (Person == null) return null;

            return new CalomniateurSQLS
            {
                Id = Person.Id,
                Person = Person.Person?.ToSQLS() 
            };
        }

        public static Admin ToDomain(this AdminSQLS sqls)
        {
            if (sqls == null) return null;

            return new Admin
            {
                Id = sqls.Id,
                Person = sqls.Person?.ToDomain() 
            };
        }

        public static AdminSQLS ToSQLS(this Admin Person)
        {
            if (Person == null) return null;

            return new AdminSQLS
            {
                Id = Person.Id,
                Person = Person.Person?.ToSQLS()
            };
        }

        public static UserSQLS ToSQLS(this User domain)
        {
            if (domain == null) return null;

            return new UserSQLS
            {
                Person = domain.Person.ToSQLS(),
                PasswordHash = domain.PasswordHash,
                IsAdmin = domain.IsAdmin,
                IsVip = domain.IsVip,
                IsFisc = domain.IsFisc
            };
        }

    }
}
