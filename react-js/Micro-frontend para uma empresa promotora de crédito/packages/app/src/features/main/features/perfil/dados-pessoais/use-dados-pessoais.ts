import { useClienteLogado } from '@pcf/core';

import { DadosPessoaisFormData } from './dados-pessoais-form';

export function useDadosPessoais(): {
  isLoading: boolean;
  dadosPessoaisFormData: DadosPessoaisFormData | null;
} {
  const { data: clienteLogado, isLoading } = useClienteLogado();
  const dadosPessoaisFormData: DadosPessoaisFormData | null = !clienteLogado
    ? null
    : {
        ...clienteLogado,
        idGenero: clienteLogado.genero?.id.toString(),
        idEstadoCivil: clienteLogado.estadoCivil?.id.toString(),
        idGrauInstrucao: clienteLogado.grauInstrucao?.id.toString(),
        idNaturalidade: clienteLogado.cidadeNatal?.uf.id.toString(),
        idCidadeNatal: clienteLogado.cidadeNatal?.id.toString(),
        deficienteVisual: clienteLogado.deficienteVisual ? 'true' : 'false',
        dataNascimento: clienteLogado?.dataNascimento
          ? new Date(clienteLogado?.dataNascimento)
          : undefined,
      };

  return { isLoading, dadosPessoaisFormData };
}
