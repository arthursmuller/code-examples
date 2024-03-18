import { SimulacaoNovoRetornoModel } from '@pcf/core';

export interface SimulationResult extends SimulacaoNovoRetornoModel {
  isCustomInterval?: boolean;
}
