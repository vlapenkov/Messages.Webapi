import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { AxiosCommand } from '../axios/axios.command';
import { UrlExtractor } from './get-url-handler';

export class PatchCommand<TOut, Tin extends ICommand<TOut>> extends AxiosCommand<TOut, Tin> {
  constructor(getUrl: UrlExtractor<Tin>) {
    super((http, input) =>
      http.patch(getUrl(input), {
        ...input,
      }),
    );
  }
}
