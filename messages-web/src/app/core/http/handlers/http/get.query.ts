import { IQuery } from '@/app/core/cqrs/base/@types/IQuery';
import { AxiosQuery } from '../axios/axios.query';
import { UrlGetter } from './@types/UrlGetter';

export class GetQuery<TOut, Tin extends IQuery<TOut> | undefined> extends AxiosQuery<TOut, Tin> {
  constructor(getUrl: UrlGetter<Tin>) {
    super((http, input) =>
      http.get<TOut>(getUrl(input), {
        params: {
          ...input,
        },
      }),
    );
  }
}
