import { createWrapper } from '@/app/core/handlers/base/handler-wrapper';
import { Ok } from '@/app/core/handlers/http/results/ok.result';
import { AnyRequest } from '../@types/requetst-handler';

export const useMock = <TResponse>(mockFn: () => TResponse) =>
  createWrapper<AnyRequest, AnyRequest>(
    () => () =>
      new Promise((resolve) => {
        setTimeout(() => {
          resolve(new Ok(mockFn()));
        }, 500);
      }),
  );
