using MediatR;
using Rk.Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.SectionsNS.Commands.AddDocument
{
    public class UpsertDocumentCommand : IRequest
    {
        public long SectionId { get; set; }
        
        public FileDataDto  Document { get; set; }
    }
}
