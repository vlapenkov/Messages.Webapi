import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { AxiosCommand } from '../axios/axios.command';
import { UrlExtractor } from './get-url-handler';

export class PutCommand<TOut, Tin extends ICommand<TOut>> extends AxiosCommand<TOut, Tin> {
  constructor(getUrl: UrlExtractor<Tin>) {
    super((http, input) =>
      http.put(getUrl(input), {
        ...input,
      }),
    );
  }
}
