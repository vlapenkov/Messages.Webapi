import { ModelBase } from '../../models/model-base';
import { HttpServiceOptions, defineHttpService } from './define-http.service';
import { useDefaultQueries } from './handlers/use-default-queries';

export function defineCollectionService<TModel extends ModelBase>(
  opts: HttpServiceOptions<TModel>,
) {
  const x = defineHttpService<TModel>(opts);
  return {
    ...useDefaultQueries(x),
    get: x.defineGet<TModel>(),
  };
}
