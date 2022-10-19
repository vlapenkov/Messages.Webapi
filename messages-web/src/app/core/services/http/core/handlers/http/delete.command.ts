import { ICommand } from '../../../../cqrs/base/@types/ICommand';
import { AxiosCommand } from '../axios/axios.command';
import { UrlGetter } from './@types/UrlGetter';

export class PutCommand<TOut, Tin extends ICommand<TOut>> extends AxiosCommand<TOut, Tin> {
  constructor(getUrl: UrlGetter<Tin>) {
    super((input, http) =>
      http.delete(getUrl(input), {
        params: {
          ...input,
        },
      }),
    );
  }
}
