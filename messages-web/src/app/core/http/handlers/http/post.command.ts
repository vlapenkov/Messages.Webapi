import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { AxiosCommand } from '../axios/axios.command';
import { GetUrlHandler } from './UrlGetter';

export class PostCommand<TOut, Tin extends ICommand<TOut>> extends AxiosCommand<TOut, Tin> {
  constructor(getUrl: GetUrlHandler<Tin>) {
    super((http, input) =>
      http.post(getUrl(input), {
        ...input,
      }),
    );
  }
}
