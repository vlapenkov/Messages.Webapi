import { createAxiosWrapper } from '../handlers/axios/axios.handler';

export const useGet = <TOut, Tin = undefined>() =>
  createAxiosWrapper<TOut, Tin>(
    (getUrl) => (http, input) =>
      http.get(getUrl(input), {
        params: {
          ...input,
        },
      }),
  );

export const usePost = <TOut, Tin = undefined>() =>
  createAxiosWrapper<TOut, Tin>(
    (getUrl) => (http, input) =>
      http.post(getUrl(input), {
        ...input,
      }),
  );

export const usePut = <TOut, Tin = undefined>() =>
  createAxiosWrapper<TOut, Tin>(
    (getUrl) => (http, input) =>
      http.put(getUrl(input), {
        ...input,
      }),
  );

export const usePatch = <TOut, Tin = undefined>() =>
  createAxiosWrapper<TOut, Tin>(
    (getUrl) => (http, input) =>
      http.patch(getUrl(input), {
        ...input,
      }),
  );

export const useDelete = <TOut, Tin = undefined>() =>
  createAxiosWrapper<TOut, Tin>(
    (getUrl) => (http, input) =>
      http.delete(getUrl(input), {
        params: {
          ...input,
        },
      }),
  );
