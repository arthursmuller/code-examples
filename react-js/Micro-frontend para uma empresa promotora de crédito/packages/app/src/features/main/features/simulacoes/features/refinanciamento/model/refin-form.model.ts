import { ContratoModel } from '@pcf/core';

import { SimulationMatriculaData } from '../../../components';
import { Prazo } from '../../../components/simulation-prazo-picker';

export interface RefinFiltersData {
  prazo: Prazo[];
  value: number;
  showFilters: boolean;
}

export interface RefinContractData {
  contratos: ContratoModel[];
  showFilters: boolean;
}

export interface RefinFormData
  extends SimulationMatriculaData,
    RefinFiltersData,
    RefinContractData {}
