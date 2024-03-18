// TODO: Copiei pois não é exportado

import { isValidMobilePhone } from '@brazilian-utils/brazilian-utils';

export interface TelefoneClienteExibicaoModel {
  id: number;
  idCliente: number;
  ddd?: string;
  fone?: string;
  principal: boolean;

  telefone?: string;
}

// https://github.com/brazilian-utils/brazilian-utils/blob/main/src/utilities/phone/index.ts
const LANDLINE_VALID_FIRST_NUMBERS = [2, 3, 4, 5];

export function isValidLandlinePhoneFirstNumber(phone: string): boolean {
  return LANDLINE_VALID_FIRST_NUMBERS.includes(Number(phone.charAt(2)));
}

export const MOBILE_PHONE_MASK = '(##) #####-####';
export const LANDLINE_PHONE_MASK = '(##) ####-####';

export const getInitialMask = (defaultValue: string): string => {
  return defaultValue && isValidLandlinePhoneFirstNumber(defaultValue)
    ? LANDLINE_PHONE_MASK
    : MOBILE_PHONE_MASK;
};

export const getCelular = (
  phones?: TelefoneClienteExibicaoModel[],
): TelefoneClienteExibicaoModel | undefined =>
  phones?.find(({ ddd, fone }) => {
    return isValidMobilePhone(`${ddd}${fone}`);
  });

export interface Phone {
  ddd?: string;
  fone?: string;
}

export const getFormattedPhone = (phone: Phone | string): string =>
  typeof phone === 'string'
    ? phone.replace(/(\d{2})(\d{4,5})(\d{4})/g, '($1) $2 $3')
    : `(${phone.ddd}) ${phone.fone?.replace(/(\d{4,5})(\d{4})/g, '$1-$2')}`;
