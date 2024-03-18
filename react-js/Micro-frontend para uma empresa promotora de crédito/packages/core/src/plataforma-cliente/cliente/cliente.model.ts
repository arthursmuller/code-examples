import { GeneroModel } from './generos-query';
import { EstadoCivilModel } from './estados-civil-query';
import { GrauInstrucaoModel } from './graus-instrucao-query';

import { MunicipioModel } from '../localizacao/municipios-query';
import { LojaModel } from '../loja';

export interface ClienteModel {
  idGenero: number;
  idEstadoCivil: number;
  idGrauInstrucao: number;
  idCidadeNatal: number;
  nome: string;
  dataNascimento: string;
  filiacao1: string;
  filiacao2: string;
  deficienteVisual: boolean;
  email?: string;
  idLoja?: number;
}

export interface ClienteExibicaoModel {
  id: number;
  nome: string;
  cpf: string;
  dataNascimento: string;
  filiacao1: string;
  filiacao2: string;
  deficienteVisual: boolean;
  email: string;
  genero?: GeneroModel;
  estadoCivil?: EstadoCivilModel;
  grauInstrucao?: GrauInstrucaoModel;
  cidadeNatal?: MunicipioModel;
  loja?: LojaModel;
}
