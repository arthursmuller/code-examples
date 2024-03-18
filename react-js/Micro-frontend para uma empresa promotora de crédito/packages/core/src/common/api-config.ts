import { AxiosInstance, AxiosRequestConfig } from 'axios';

import { Deferred } from 'utils/deferred';

import { RequestUtilsSingleton } from './request-utils-singleton';

const tokenFreeRequests = ['login', 'logout'];

const createPendingRequest = (
  requestConfig: AxiosRequestConfig,
): Promise<any> => {
  //eslint-disable-line
  const { promise, resolve } = Deferred();

  RequestUtilsSingleton.add((newToken: string) => {
    requestConfig.headers.common.Authorization = `Bearer ${newToken}`; // eslint-disable-line no-param-reassign
    return resolve(requestConfig);
  });

  return promise.then((pendingRequest) => pendingRequest);
};

const istokenRequiredRequest = (requestConfig: AxiosRequestConfig): boolean =>
  !tokenFreeRequests.some((url) => requestConfig?.url?.endsWith(url));

interface setupInterceptorsConfig {
  onExpiredToken?: () => void;
  onInvalidToken?: () => void;
  holdRequestOnInvalid?: boolean;
}

export const setupInterceptors = (
  axiosInstance: AxiosInstance,
  { onExpiredToken }: setupInterceptorsConfig,
): (() => void) => {
  let responseInterceptor: number;
  let requestInterceptor: number;

  if (!RequestUtilsSingleton.hasInterceptors) {
    responseInterceptor = axiosInstance.interceptors.response.use(
      (response) => {
        if (response && response.headers && response.headers.authorization) {
          RequestUtilsSingleton.setJwt(response.headers.authorization);
        }
        return response;
      },
      (error) => Promise.reject(error),
    );

    requestInterceptor = axiosInstance.interceptors.request.use(
      (requestConfig) => {
        if (istokenRequiredRequest(requestConfig)) {
          if (RequestUtilsSingleton.checkJwtExpired()) {
            if (!RequestUtilsSingleton.hasPendingRequest()) {
              onExpiredToken && onExpiredToken();
            }

            return createPendingRequest(requestConfig);
          }
        }

        return requestConfig;
      },
      (error) => Promise.reject(error),
    );

    RequestUtilsSingleton.hasInterceptors = true;
  }

  return () => {
    axiosInstance.interceptors.response.eject(responseInterceptor);
    axiosInstance.interceptors.request.eject(requestInterceptor);
    RequestUtilsSingleton.hasInterceptors = false;
  };
};
