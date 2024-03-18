import { ConvenioModel } from '@pcf/core';

import { TiposEmprestimo } from './tipos-emprestimo.enum';

import { SimulationMatriculaData } from '../../../components';
import { Prazo } from '../../../components/simulation-prazo-picker';

export interface ConsignadoValueData {
  tipoEmprestimo: TiposEmprestimo;
  value: number;
}

export interface ConsignadoTypeData {
  tipoConvenio: ConvenioModel;
}

export interface ConsignadoFiltersData {
  prazo: Prazo;
  showFilters: boolean;
}

export interface ConsignadoFormData
  extends ConsignadoValueData,
    ConsignadoTypeData,
    SimulationMatriculaData,
    ConsignadoFiltersData {}
