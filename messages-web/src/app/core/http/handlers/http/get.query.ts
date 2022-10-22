import { IQuery } from '@/app/core/cqrs/base/@types/IQuery';
import { handler } from '@/app/core/handlers/handler';
import { extend } from '@/app/core/handlers/handler-lab';
import { createAxiosHandler } from '../axios/axios.handler';
import { UrlExtractor } from './get-url-handler';

// export class GetQuery<TOut, Tin extends IQuery<TOut> | undefined> extends AxiosQuery<TOut, Tin> {
//   constructor(getUrl: GetUrlHandler<Tin>) {
//     super((http, input) =>
//       http.get<TOut>(getUrl(input), {
//         params: {
//           ...input,
//         },
//       }),
//     );
//   }
// }
