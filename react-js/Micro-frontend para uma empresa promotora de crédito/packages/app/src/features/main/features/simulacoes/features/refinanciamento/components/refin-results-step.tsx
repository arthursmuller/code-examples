import { FC, useMemo } from 'react';

import {
  useProdutoOperacaoRefinQuery,
  SimulacaoNovoRefinRequisicaoModel,
  useSimularRefinQuery,
  getProdutoOperacaoRefinQueryConfig,
} from '@pcf/core';
import { FullLayoutCard, useStepsContainerContext } from '@pcf/design-system';
import { queryCacheConfig } from 'app/app-providers';

import { ResultsDisplay } from '../../../components/results-display';
import { RefinFormData } from '../model/refin-form.model';
import { Prazo } from '../../../components/simulation-prazo-picker';
import { usePostIntencaoOperacao } from '../../../utils/use-post-intencao-operacao';

queryCacheConfig.prefetchQuery(getProdutoOperacaoRefinQueryConfig());

const flatRanges = (prazo: Prazo): number | number[] =>
  Array.isArray(prazo)
    ? Array.from({ length: prazo[1] - prazo[0] + 1 }, (_, i) => prazo[0] + i)
    : (prazo as number);

export const RefinResultsStep: FC = () => {
  const { data } = useStepsContainerContext<RefinFormData>();

  const request: SimulacaoNovoRefinRequisicaoModel = useMemo(
    () => ({
      prestacao:
        data.value || data.contratos.reduce((p, cur) => p + cur.prestacao, 0),
      retornarSomenteOperacoesViaveis: true,

      prazos: !data.prazo ? [84, 96] : data.prazo.flatMap(flatRanges),
      contratosRefinanciamento: data.contratos.map((c) => ({
        contrato: c.contrato,
      })),
      idConvenio: data.matricula.convenio.id,
    }),
    [data],
  );

  const queryProps = useSimularRefinQuery(request, {
    useErrorBoundary: false,
  });

  const { data: produtoOp } = useProdutoOperacaoRefinQuery();

  const { post, isPosting } = usePostIntencaoOperacao(
    produtoOp,
    data.matricula.id,
    {
      contratos: data.contratos.map((c) => ({
        contrato: c.contrato,
      })),
    },
  );

  return (
    <FullLayoutCard
      title={
        !data.prazo
          ? (queryProps.data?.length &&
              'Resultados dos prazos disponíveis para você') ||
            undefined
          : 'Prazo personalizado'
      }
    >
      <ResultsDisplay
        queryProps={queryProps}
        onSubmit={post}
        isSubmitting={isPosting}
      />
    </FullLayoutCard>
  );
};
