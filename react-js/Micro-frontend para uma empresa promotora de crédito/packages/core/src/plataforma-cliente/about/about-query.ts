import { createQueryFor } from 'utils';

export interface AboutStatus {
  chave: string;
  status: string;
}

export interface AboutModel {
  branch: string;
  versao: string;
  servidor: string;
  aspnetcoreEnvironment: string;
  status: AboutStatus[];
}

export const {
  getQueryConfig: getAboutQueryConfig,
  useQueryOf: useAboutQuery,
} = createQueryFor<AboutModel>('about');
