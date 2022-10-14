using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Logic.SectionsNS.Commands.CreateSectionCommand
{
    /// <summary>
    /// Команда для создания раздела
    /// </summary>
    public class CreateSectionCommand : IRequest<long>
    {
        /// <summary>Id Родительского раздела</summary>
        public long? ParentSectionId { get; set; }

        /// <summary>Наименование раздела</summary>
        public string Name { get; set; }
    }
}
