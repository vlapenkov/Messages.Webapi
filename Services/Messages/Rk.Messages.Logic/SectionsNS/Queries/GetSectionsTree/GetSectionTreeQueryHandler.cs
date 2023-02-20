using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.SectionsNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Queries.GetSectionsTree
{
    public class GetSectionTreeQueryHandler :IRequestHandler<GetSectionTreeQuery, SectionTreeNode>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;       

        public GetSectionTreeQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить потомков для данного раздела
        /// </summary>
        /// <param name="catalogSection">текущий раздел</param>
        /// <param name="sectionTreeNode">текущий узел</param>
        private void AddChildren(CatalogSection catalogSection, SectionTreeNode sectionTreeNode)
        {
           var sectionDto =  _mapper.Map<SectionDto>(catalogSection); 
                                                      
           var addedNode = new  SectionTreeNode(sectionDto);

          sectionTreeNode.AddChild(addedNode);

            foreach (var item in catalogSection.Children)
            {
                AddChildren(item, addedNode);
            }
            
        }

        public async Task<SectionTreeNode> Handle(GetSectionTreeQuery query, CancellationToken cancellationToken)
        {
            CatalogSection rootSectionFound=null;

            SectionTreeNode rootNode = null;


            if (query.ParentSectionId != null)
            {
                rootSectionFound = await _appDbContext.CatalogSections
                    .Include(product => product.SectionDocuments)
                     .ThenInclude(pd => pd.Document)
                     .AsNoTracking()
                    .FirstOrDefaultAsync(self => self.Id == query.ParentSectionId)
                   ?? throw new EntityNotFoundException($"Категория с Id = {query.ParentSectionId} не найдена");

               var sectionDto = _mapper.Map<SectionDto>(rootSectionFound);
                
                rootNode = new SectionTreeNode(sectionDto);

                
                foreach (var item in rootSectionFound.Children)
                {
                    AddChildren(item, rootNode);
                }

            }
            else
            {
                //  rootNode = new SectionTreeNode(new SectionDto(null, 0, "Рутовый узел"));

                rootNode = new SectionTreeNode(new SectionDto { ParentSectionId = null, Id = 0, Name = "Рутовый узел" });

                var rootSections = await _appDbContext.CatalogSections.Where(self => self.ParentCatalogSectionId == null).ToListAsync();

                foreach (var item in rootSections)
                {
                    AddChildren(item, rootNode);
                }

            }


            return rootNode;
        }

       
    }
}
