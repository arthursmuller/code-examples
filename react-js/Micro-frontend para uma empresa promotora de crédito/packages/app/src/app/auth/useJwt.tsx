import { RequestUtilsSingleton, DecodedJwtData } from '@pcf/core';

interface UseJwtData {
  jwt: string | null;
  decodedJwt: DecodedJwtData | null;
  userId: number;
}

export const useJwt = (): UseJwtData => {
  const decodedJwt = RequestUtilsSingleton.getDecodedJwt();
  const userId = parseInt(decodedJwt?.nameid || '', 10) || -1;

  return {
    jwt: RequestUtilsSingleton.getJwt(),
    decodedJwt,
    userId,
  };
};
