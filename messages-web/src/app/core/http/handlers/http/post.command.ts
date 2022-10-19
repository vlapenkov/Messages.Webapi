import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { AxiosCommand } from '../axios/axios.command';
import { UrlGetter } from './@types/UrlGetter';

export class PostCommand<TOut, Tin extends ICommand<TOut>> extends AxiosCommand<TOut, Tin> {
  constructor(getUrl: UrlGetter<Tin>) {
    super((input, http) =>
      http.post(getUrl(input), {
        ...input,
      }),
    );
  }
}
