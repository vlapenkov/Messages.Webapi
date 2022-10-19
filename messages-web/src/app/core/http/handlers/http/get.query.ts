import { IQuery } from '@/app/core/cqrs/base/@types/IQuery';
import { AxiosQuery } from '../axios/axios.query';
import { UrlGetter } from './@types/UrlGetter';

export class GetQuery<TOut, Tin extends IQuery<TOut>> extends AxiosQuery<TOut, Tin> {
  constructor(getUrl: UrlGetter<Tin>) {
    super((input, http) =>
      http.get<TOut>(getUrl(input), {
        params: {
          ...input,
        },
      }),
    );
  }
}
