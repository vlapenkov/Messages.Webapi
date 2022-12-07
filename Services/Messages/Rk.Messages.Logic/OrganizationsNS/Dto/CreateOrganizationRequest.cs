using Rk.Messages.Domain.Entities;
using Rk.Messages.Logic.CommonNS.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
        public string Site { get; set; }

        public string Okved { get; set; }

        public string Okved2 { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
