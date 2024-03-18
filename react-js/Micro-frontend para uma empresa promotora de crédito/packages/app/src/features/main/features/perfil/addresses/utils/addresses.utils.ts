import { formatCEP } from '@brazilian-utils/brazilian-utils';

import { EnderecoClienteExibicaoModel, EnderecoClienteModel } from '@pcf/core';

import { AddressFormData } from '../components/address-form';

export const transformModelEnderecoToFormData = (
  model: EnderecoClienteExibicaoModel,
): AddressFormData => {
  const {
    titulo,
    cep,
    municipio,
    complemento = '',
    principal,
    logradouro = '',
    tipoLogradouro,
    bairro,
    numero = '',
  } = model;

  return {
    titulo: titulo || '',
    cep,
    idNaturalidade: municipio?.uf.id ? String(municipio?.uf.id) : '',
    idCidadeNatal: municipio?.id ? String(municipio?.id) : '',
    cidadeNatal: municipio?.descricao,
    complemento,
    enderecoPrincipal: principal,
    idTipoLogradouro: tipoLogradouro?.id ? String(tipoLogradouro?.id) : '',
    logradouro,
    bairro,
    numero: String(numero),
  };
};

export const transformFormDataEnderecoToModel = (
  formData: AddressFormData,
): EnderecoClienteModel => {
  return {
    titulo: formData.titulo,
    idMunicipio: Number(formData.idCidadeNatal),
    bairro: formData.bairro,
    idTipoLogradouro: Number(formData.idTipoLogradouro),
    logradouro: formData.logradouro,
    numero: Number(formData.numero),
    complemento: formData.complemento,
    cep: formData.cep ? formData.cep.replace('-', '') : '',
    principal: !!formData.enderecoPrincipal,
  };
};

export const getReadableEndereco = (
  endereco: EnderecoClienteExibicaoModel,
): string => {
  const {
    complemento,
    tipoLogradouro,
    logradouro,
    numero,
    bairro,
    municipio,
    cep,
  } = endereco;

  const complementoDisplay = complemento ? `${complemento},` : '';
  const cepDisplay = !!cep && `CEP: ${formatCEP(cep)}`;

  return `${tipoLogradouro?.descricao} ${logradouro}, ${numero}, ${complementoDisplay}
          ${bairro} - ${municipio?.descricao}/${municipio?.uf.sigla}
          ${cepDisplay}
  `;
};
