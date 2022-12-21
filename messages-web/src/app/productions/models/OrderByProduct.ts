import { OrderByProduct } from '@/store/catalog-filters.store';

export interface IOrderByProductWithName {
  value: OrderByProduct;
  name: string;
}

export const ordersByProductWithName: IOrderByProductWithName[] = [
  {
    value: OrderByProduct.NameByAsc,
    name: 'по названию (возрастание)',
  },
  {
    value: OrderByProduct.NameByDesc,
    name: 'по названию (убывание)',
  },
  {
    value: OrderByProduct.RegionByAsc,
    name: 'по региону (возрастание)',
  },
  {
    value: OrderByProduct.RegionByDesc,
    name: 'по региону (убывание)',
  },
  {
    value: OrderByProduct.ProducerByAsc,
    name: 'по названию производителя (возрастание)',
  },
  {
    value: OrderByProduct.ProducerByDesc,
    name: 'по названию производителя (убывание)',
  },
  {
    value: OrderByProduct.RatingByAsc,
    name: 'по рейтингу (возрастание)',
  },
  {
    value: OrderByProduct.RatingByDesc,
    name: 'по рейтингу (убывание)',
  },
];
