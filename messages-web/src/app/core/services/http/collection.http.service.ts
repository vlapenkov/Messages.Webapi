import { ModelBase } from '../../models/model-base';
import { defineHttpService } from './define-http.service';
import { useDefaultQueries } from './handlers/use-default-queries';

export function defineCollectionService<TModel extends ModelBase>() {
  const x = defineHttpService<TModel>({});
  return useDefaultQueries(x);
  throw new Error('Not Implemented!');
}
