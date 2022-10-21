import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { AxiosCommand } from '../axios/axios.command';
import { GetUrlHandler } from './UrlGetter';

export class PutCommand<TOut, Tin extends ICommand<TOut>> extends AxiosCommand<TOut, Tin> {
  constructor(getUrl: GetUrlHandler<Tin>) {
    super((http, input) =>
      http.put(getUrl(input), {
        ...input,
      }),
    );
  }
}
