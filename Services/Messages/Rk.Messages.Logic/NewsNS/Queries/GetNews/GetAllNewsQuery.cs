using MediatR;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.NewsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.NewsNS.Queries.GetNews
{
    public class GetAllNewsQuery : IRequest<PagedResponse<NewsResponse>>
    {
        public NewsRequest Request { get; set; }
    }
}
