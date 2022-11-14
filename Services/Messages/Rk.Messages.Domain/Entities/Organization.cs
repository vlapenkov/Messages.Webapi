using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization :BaseEntity
    {
        protected Organization() { }
        

        public Organization(long id,  string name, string fullName, string ogrn, string inn, string kpp, string region, string city, string address, string site, OrganizationStatus status)
        {
            Id = id;
            Name = name;
            FullName = fullName;
            Ogrn = ogrn;
            Inn = inn;
            Kpp = kpp;
            Region = region;
            City = city;
            Address = address;
            Site = site;
            Status = status;
        }

        #region Private Members

        [StringLength(512)]
        public string Name { get; private set; }

        [StringLength(1024)]
        public string FullName { get; private set; }

        [StringLength(20)]
        public string Ogrn { get; private set; }

        [StringLength(12)]
        public string Inn { get; private set; }

        [StringLength(9)]
        public string Kpp { get; private set; }

        [StringLength(512)]
        public string Region { get; private set; }

        [StringLength(512)]
        public string City { get; private set; }

        [StringLength(1024)]
        public string Address { get; private set; }

        [StringLength(512)]
        public string Site { get; private set; }

        public OrganizationStatus Status { get; private set; }

        #endregion
    }
}
