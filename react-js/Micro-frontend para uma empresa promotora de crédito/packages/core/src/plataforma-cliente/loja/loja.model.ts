import { MunicipioModel } from '../localizacao/municipios-query';
import { TipoLogradouroModel } from '../localizacao/tipos-logradouro-query';

interface LojaTelefoneModel {
  id: number;
  mensagemApresentacao: string;
  possuiContaWhatsApp: boolean;
  telefone: string;
}

export interface LojaModel {
  bairro: string;
  cep: string;
  logradouro: string;
  numero: string;
  complemento: string;
  idMunicipio: number;
  idTipoLogradouro: number;

  id: number;
  latitude: number;
  longitude: number;
  mensagemApresentacao: string;
  nome: string;

  telefones?: LojaTelefoneModel[];
  municipio: MunicipioModel;
  tipoLogradouro: TipoLogradouroModel;
}
