import { SimulacaoNovoRetornoModel } from '@pcf/core';

import { SimulationMatriculaData } from '../../../components';

export enum PortabilidadeType {
  Portar = 1,
  Simular = 2,
}

export interface PortabilidadeTypeFormData {
  type: PortabilidadeType;
}

export interface PortabilidadeInfoFormData {
  cpf: number;
  origem: string;
  contrato: string;
  saldoDevedor: number;
  parcelas: number;
  parcelasPagas: number;
  prestacao: number;
}

export interface PortabilidadeResultFormData {
  simulacao: SimulacaoNovoRetornoModel;
}

export interface PortabilidadeFormData
  extends PortabilidadeTypeFormData,
    PortabilidadeInfoFormData,
    SimulationMatriculaData,
    PortabilidadeResultFormData {
  prazos: number | number[];
  prestacaoIntencao: number;
}
