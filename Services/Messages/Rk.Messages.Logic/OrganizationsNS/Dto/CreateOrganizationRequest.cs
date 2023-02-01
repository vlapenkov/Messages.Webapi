using System.Collections.Generic;

namespace Rk.Messages.Logic.OrganizationsNS.Dto
{
    /// <summary>
    /// Создание организации
    /// </summary>
    public record CreateOrganizationRequest
    {

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Ogrn { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string FactAddress { get; set; }       

        public string Site { get; set; }

        public string Okved { get; set; }

        public string Okved2 { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        /// <summary>Признак организации- производителя</summary>
        public bool IsProducer { get; set; }

        /// <summary>Признак организации- покупателя</summary>
        public bool IsBuyer { get; set; }

        /// <summary>банк</summary>
        public string BankName { get; set; }

        /// <summary>рс</summary>
        public string Account { get; set; }

        /// <summary>кс</summary>
        public string CorrAccount { get; set; }

        /// <summary>бик</summary>
        public string Bik { get; set; }

        /// <summary>документ для картинки</summary>
        public FileDataDto Document { get; set; }

        /// <summary>Вложения</summary>
        public List<FileDataDto> Documents { get; set; }
    }
}
