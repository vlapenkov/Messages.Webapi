import { createAxiosWrapper } from '../axios/axios.handler';
import { IRequestOptions } from '../options/get-options.handler';

export const useGet = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url, bodyOrParams }: IRequestOptions = { ...defaultOptions, ...optionsProvided };
    return http.get(url, {
      params: bodyOrParams ?? {
        ...input,
      },
    });
  });
};

export const usePost = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url, bodyOrParams }: IRequestOptions = { ...defaultOptions, ...optionsProvided };
    return http.post(
      url,
      bodyOrParams ?? {
        ...input,
      },
    );
  });
};

export const usePut = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url, bodyOrParams }: IRequestOptions = { ...defaultOptions, ...optionsProvided };
    return http.put(
      url,
      bodyOrParams ?? {
        ...input,
      },
    );
  });
};

export const usePatch = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url, bodyOrParams }: IRequestOptions = { ...defaultOptions, ...optionsProvided };

    return http.patch(
      url,
      bodyOrParams ?? {
        ...input,
      },
    );
  });
};

export const useDelete = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url, bodyOrParams }: IRequestOptions = { ...defaultOptions, ...optionsProvided };
    return http.delete(url, {
      params: bodyOrParams ?? {
        ...input,
      },
    });
  });
};
