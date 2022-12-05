using Rk.Messages.Spa.Infrastructure.Dto.CommonNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Spa.Infrastructure.Dto.NewsNS
{
    public record CreateNewsRequest
    {      

        [StringLength(1024)]
        public string Name { get; set; }

        [StringLength(4096)]
        public string Description { get; set; }

        public FileDataDto? Document { get; set; }


    }
}
