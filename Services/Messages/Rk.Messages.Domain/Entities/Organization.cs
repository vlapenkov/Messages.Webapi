using System;
using System.Collections.Generic;
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
        public string Name { get; private set; } = null!;

        [StringLength(1024)]
        public string FullName { get; private set; } = null!;

        [StringLength(20)]
        public string Ogrn { get; private set; } = null!;

        [StringLength(12)]
        public string Inn { get; private set; } = null!;

        [StringLength(9)]
        public string Kpp { get; private set; } = null!;

        [StringLength(512)]
        public string? Region { get; private set; }

        [StringLength(512)]
        public string? City { get; private set; }

        [StringLength(1024)]
        public string? Address { get; private set; }

        [StringLength(512)]
        public string? Site { get; private set; }

        [StringLength(1024)]
        public string? Okved { get; private set; } = null!;

        [StringLength(1024)]
        public string? Okved2 { get; private set; }

        [StringLength(512)]
        public string? Phone { get; private set; }

        [StringLength(512)]
        public string? Email { get; private set; }

        public OrganizationStatus Status { get; private set; }

        public double? Latitude { get; private set; }

        public double? Longitude { get; private set; }

        /// <summary>Признак организации- производителя</summary>
        public bool IsProducer { get; private set; }

        /// <summary>Признак организации- покупателя</summary>
        public bool IsBuyer { get; private set; }

        [StringLength(1024)]
        public string? FactAddress { get; private set; }

        [StringLength(512)]
        public string? BankName { get; private set; }

        [StringLength(512)]
        public string? Account { get; private set; }

        [StringLength(512)]
        public string? CorrAccount { get; private set; }

        [StringLength(512)]
        public string?  Bik { get; private set; }

        /// <summary>
        /// Изображение логотипа организации
        /// </summary>
        public Guid? DocumentId { get; private set; }

        /// <summary>
        /// Документы организации
        /// </summary>
        private readonly List<OrganizationDocument> _organizationDocuments = new() ;
        public virtual IReadOnlyList<OrganizationDocument> OrganizationDocuments => _organizationDocuments;

        #endregion

        public void SetProducer(bool isProducer) {
            IsProducer = isProducer;
        }

        public void SetBuyer(bool isBuyer)
        {
            IsBuyer = isBuyer;
        }

        public void SetFactAddress(string factAddress)
        {
            FactAddress = factAddress;
        }

        /// <summary>Установить банковские реквизиты</summary>
        public void SetAccountParameters(string bankName, string account, string corrAccount, string bik) { 
        
            BankName = bankName;
            Account = account;
            CorrAccount = corrAccount;
            Bik = bik;
        }

        /// <summary>
        /// Добавить id логотипа организации
        /// </summary>
        /// <param name="documentId"></param>
        public void  SetDocumentId(Guid? documentId) { 
            DocumentId = documentId;
        }

        public void SetStatus(OrganizationStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// Добавить вложения
        /// </summary>
        /// <param name="files"></param>
        public void AddOrganizationDocuments(IReadOnlyCollection<OrganizationDocument> files)
        {
            _organizationDocuments.AddRange(files);
        }
    }
}
