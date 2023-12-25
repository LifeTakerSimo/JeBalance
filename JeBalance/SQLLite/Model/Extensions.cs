using Domain.Model;
using JeBalance.SQLLite.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeBalance.Common
{
    public static class Extensions
    {
        public static Personne ToDomain(this PersonSQLS sqls)
        {
            return new Personne
            {
                Id = sqls.Id,
                FirstName = sqls.FirstName,
                LastName = sqls.LastName,
                StreetNumber = sqls.StreetNumber,
                StreetName = sqls.StreetName,
                PostalCode = sqls.PostalCode,
                CityName = sqls.CityName,
                IsVIP = sqls.IsVIP
            };
        }

        public static PersonSQLS ToSQLS(this Personne domain)
        {
            return new PersonSQLS
            {
                Id = domain.Id,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                StreetNumber = domain.StreetNumber,
                StreetName = domain.StreetName,
                PostalCode = domain.PostalCode,
                CityName = domain.CityName,
                IsVIP = domain.IsVIP
            };
        }

        // Denonciation conversion methods
        public static Denonciation ToDomain(this DenonciationSQLS sqls)
        {
            return new Denonciation
            {
                Id = sqls.Id,
                Timestamp = sqls.Timestamp,
                Informant = sqls.Informant.ToDomain(), 
                Suspect = sqls.Suspect.ToDomain(),
                Offense = sqls.Offense,
                EvasionCountry = sqls.EvasionCountry
            };
        }

        public static DenonciationSQLS ToSQLS(this Denonciation domain)
        {
            return new DenonciationSQLS
            {
                Id = domain.Id,
                Timestamp = domain.Timestamp,
                Informant = domain.Informant.ToSQLS(), 
                Suspect = domain.Suspect.ToSQLS(),
                Offense = domain.Offense,
                EvasionCountry = domain.EvasionCountry
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

        public static ResponseSQLS ToSQLS(this Response domain)
        {
            if (domain == null) return null;

            return new ResponseSQLS
            {
                Id = domain.Id,
                Timestamp = domain.Timestamp,
                ResponseType = domain.ResponseType,
                Amount = domain.Amount,
                DenonciationId = domain.DenonciationId,
                Denonciation = domain.Denonciation?.ToSQLS() 
            };
        }

        public static Calomniateur ToDomain(this CalomniateurSQLS sqls)
        {
            if (sqls == null) return null;

            return new Calomniateur
            {
                Id = sqls.Id,
                Personne = sqls.Person?.ToDomain() 
            };
        }

        public static CalomniateurSQLS ToSQLS(this Calomniateur domain)
        {
            if (domain == null) return null;

            return new CalomniateurSQLS
            {
                Id = domain.Id,
                Person = domain.Personne?.ToSQLS() 
            };
        }

        public static Admin ToDomain(this AdminSQLS sqls)
        {
            if (sqls == null) return null;

            return new Admin
            {
                Id = sqls.Id,
                Personne = sqls.Person?.ToDomain() 
            };
        }

        public static AdminSQLS ToSQLS(this Admin domain)
        {
            if (domain == null) return null;

            return new AdminSQLS
            {
                Id = domain.Id,
                Person = domain.Personne?.ToSQLS()
            };
        }
    }
}
