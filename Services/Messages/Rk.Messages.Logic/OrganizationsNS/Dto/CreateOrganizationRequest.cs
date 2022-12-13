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

        public string PostAddress { get; set; }

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

        public string BankName { get; set; }

        public string Account { get; set; }

        public string CorrAccount { get; set; }

        public string Bik { get; set; }
    }
}
