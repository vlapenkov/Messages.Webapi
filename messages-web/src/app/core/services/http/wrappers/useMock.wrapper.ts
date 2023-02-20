import { createWrapper } from '@/app/core/handlers/base/handler-wrapper';
import { Ok } from '@/app/core/handlers/http/results/ok.result';
import { AnyRequest } from '../@types/requetst-handler';

export const useMock = <TResponse, TRrequest = void>(mockFn: (request: TRrequest) => TResponse) =>
  createWrapper<AnyRequest, AnyRequest>(
    () => (request: TRrequest) =>
      new Promise((resolve) => {
        setTimeout(() => {
          resolve(new Ok(mockFn(request)));
        }, 500);
      }),
  );
