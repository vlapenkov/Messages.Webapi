using System.ComponentModel.DataAnnotations;
using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization : AuditableEntity
    {
        protected Organization() { }
        

        public Organization(
            long id,  
            string name, 
            string fullName, 
            string ogrn, 
            string inn, 
            string kpp, 
            string region, 
            string city, 
            string address, 
            string site, 
            string okved, 
            string okved2, 
            string phone,
            string email,            
            OrganizationStatus status,
            double? latitude=null,
            double? longitude = null
            ) :
            this( 
                name,  
                fullName,  
                ogrn,  
                inn,  
                kpp,  
                region,  
                city,  
                address,  
                site, 
                okved, 
                okved2, 
                phone,
                email,                
                status,
                latitude,
                longitude
                )    
        {
            Id = id;           
        }

        public Organization(
            string name, 
            string fullName, 
            string ogrn, 
            string inn, 
            string kpp, 
            string region, 
            string city, 
            string address, 
            string site, 
            string okved, 
            string okved2,
            string phone,
            string email,
            OrganizationStatus status,
            double? latitude = null,
            double? longitude = null            
            )
        {
            
            Name = name;
            FullName = fullName;
            Ogrn = ogrn;
            Inn = inn;
            Kpp = kpp;
            Region = region;
            City = city;
            Address = address;
            Site = site;

            Okved = okved;
            Okved2 = okved2;

            Phone = phone;
            Email = email;

            Status = status;

            Latitude = latitude;
            Longitude = longitude;
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

        [StringLength(1024)]
        public string Okved { get; private set; }

        [StringLength(1024)]
        public string Okved2 { get; private set; }

        [StringLength(512)]
        public string Phone { get; private set; }

        [StringLength(512)]
        public string Email { get; private set; }

        public OrganizationStatus Status { get; private set; }

        public double? Latitude { get; private set; }

        public double? Longitude { get; private set; }

        /// <summary>Признак организации- производителя</summary>
        public bool IsProducer { get; private set; }

        /// <summary>Признак организации- покупателя</summary>
        public bool IsBuyer { get; private set; }

        #endregion

        public void SetProducer(bool isProducer) {
            IsProducer = isProducer;
        }

        public void SetBuyer(bool isBuyer)
        {
            IsBuyer = isBuyer;
        }
    }
}
