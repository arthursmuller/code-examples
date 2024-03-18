import {
  TelefoneClienteExibicaoModel,
  TelefoneClienteModel,
  LojaModel,
} from '@pcf/core';
import { getFormattedPhone } from '@pcf/design-system';

export const getDDDandFone = (
  fullPhoneNumber: string,
): { ddd: string; fone: string } => {
  return {
    ddd: fullPhoneNumber.slice(0, 2),
    fone: fullPhoneNumber.slice(2),
  };
};

export const transformFormDataToModel = (
  phone: string,
): TelefoneClienteModel => {
  return {
    ...getDDDandFone(phone),
  };
};

export const getFormattedMainPhone = (
  phones: TelefoneClienteExibicaoModel[] | undefined,
): string | undefined => {
  const foundPhone = phones?.find((phone) => phone.principal);
  return foundPhone && getFormattedPhone(foundPhone);
};

export const getReadableLoja = (loja: LojaModel): string => {
  return `${loja.nome}
          ${
            loja.tipoLogradouro?.descricao
              ? `${loja.tipoLogradouro?.descricao} `
              : ''
          }${loja.logradouro ?? ''}${loja.numero ? ` ,${loja.numero}` : ''}${
    loja.complemento ? ` ,${loja.complemento}` : ''
  }
          ${loja.telefones ? getFormattedPhone(loja.telefones[0].telefone) : ''}
  `;
};
