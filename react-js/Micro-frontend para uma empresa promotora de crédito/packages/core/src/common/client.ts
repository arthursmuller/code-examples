import axios from 'axios';
import qs from 'qs';

import { BemApiRetorno } from './bem-api-retorno.model';

export interface PutVariables<Request> {
  data: Request;
  id: number;
}

export const api = axios.create();

export const setApiBaseUrl = (url: string): void => {
  api.defaults.baseURL = url;
};

export const setAuthorization = (jwtToken: string | null): void => {
  if (jwtToken) {
    api.defaults.headers.common.Authorization = `Bearer ${jwtToken}`;
  } else {
    const { Authorization, ...config } = api.defaults.headers.common; // eslint-disable-line
    api.defaults.headers.common = config;
  }
};

export const getQueryFor =
  <Result, QueryModel = null>(
    route: string,
    query?: QueryModel,
    skipAlerts?: boolean,
  ): (() => Promise<Result>) =>
  async () => {
    const queryString =
      (query && qs.stringify(query, { addQueryPrefix: true })) || '';

    const response = await api.get<BemApiRetorno<Result>>(
      `${route}${queryString}`,
    );

    if (response.data?.alertas?.length && !skipAlerts) {
      throw response.data.alertas[0].mensagem;
    }

    return response?.data.retorno;
  };

export const postQueryFor =
  <Request, Result>(route: string): ((data: Request) => Promise<Result>) =>
  async (data: Request) => {
    const response = await api.post<BemApiRetorno<Result>>(route, data);

    return response?.data.retorno;
  };

export const deleteQueryFor =
  <Result>(route: string): (() => Promise<Result>) =>
  async () => {
    const response = await api.delete<BemApiRetorno<Result>>(route);

    return response?.data.retorno;
  };

export const patchQueryFor =
  <Request, Result>(route: string): ((data: Request) => Promise<Result>) =>
  async (data: Request) => {
    const response = await api.patch<BemApiRetorno<Result>>(route, data);

    return response?.data.retorno;
  };

export const putQueryFor =
  <Request, Result>(
    route: string,
  ): ((data: PutVariables<Partial<Request>>) => Promise<Result>) =>
  async ({ data, id }: PutVariables<Partial<Request>>) => {
    const response = await api.put<BemApiRetorno<Result>>(
      `${route}/${id}`,
      data,
    );

    return response?.data.retorno;
  };

export const simplePutQueryFor =
  <Request, Result>(route: string): ((data: Request) => Promise<Result>) =>
  async (data: Request) => {
    const response = await api.put<BemApiRetorno<Result>>(`${route}`, data);

    return response?.data.retorno;
  };
