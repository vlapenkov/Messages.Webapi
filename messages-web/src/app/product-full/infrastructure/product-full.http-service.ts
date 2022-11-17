import { defineSingleHttpService } from '@/app/core/services/http/custom/single.http-service';
import { IProductFullModel } from '../@types/IProductFullModel';

const [productFullService, { defineGet, definePost }] = defineSingleHttpService<IProductFullModel>({
  url: 'api/v1/Products',
});

productFullService.get = defineGet((arg: number) => ({
  url: `/${arg}`,
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
