import { createAxiosWrapper } from '../handlers/axios/axios.handler';
import { IRequestOptions } from '../handlers/options/get-options.handler';

export const useGet = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url }: IRequestOptions = { ...defaultOptions, ...optionsProvided };
    return http.get(url, {
      params: {
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
    const { url }: IRequestOptions = { ...defaultOptions, ...optionsProvided };
    return http.post(url, {
      ...input,
    });
  });
};

export const usePut = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url }: IRequestOptions = { ...defaultOptions, ...optionsProvided };

    return http.put(url, {
      ...input,
    });
  });
};

export const usePatch = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url }: IRequestOptions = { ...defaultOptions, ...optionsProvided };

    return http.patch(url, {
      ...input,
    });
  });
};

export const useDelete = <TOut, Tin = undefined>() => {
  const defaultOptions: IRequestOptions = {
    url: '',
  };
  return createAxiosWrapper<TOut, Tin>((optionsProvided) => (http, input) => {
    const { url }: IRequestOptions = { ...defaultOptions, ...optionsProvided };

    return http.delete(url, {
      params: {
        ...input,
      },
    });
  });
};
