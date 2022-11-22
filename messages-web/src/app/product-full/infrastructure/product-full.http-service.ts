import { defineSingleHttpService } from '@/app/core/services/http/custom/single.http-service';
import { IProductFullModel } from '../@types/IProductFullModel';

const [productFullService, { defineGet, definePost }] = defineSingleHttpService<IProductFullModel>({
  url: 'api/Products',
});

productFullService.get = defineGet((arg: { id: number }) => ({
  url: `/${arg.id}`,
}));

productFullService.post = definePost((md: IProductFullModel) => ({
  bodyOrParams: {
    catalogSectionId: md.catalogSectionId,
    name: md.name,
    fullName: md.fullName,
    description: md.description,
    attributeValues: md.attributeValues,
    codeTnVed: md.codeTnVed,
    price: md.price,
    documents: md.documents,
  },
}));

export { productFullService };
