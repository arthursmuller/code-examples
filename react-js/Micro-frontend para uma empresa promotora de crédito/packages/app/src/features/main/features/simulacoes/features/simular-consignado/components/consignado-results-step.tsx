import { FC, useMemo } from 'react';

import { UseQueryResult } from 'react-query';

import {
  useSimularConsignadoQuery,
  getProdutoOperacaoConsignadoQueryConfig,
  useProdutoOperacaoConsignadoQuery,
  SimulacaoNovoRetornoModel,
  SimulacaoNovoRequisicaoModel,
} from '@pcf/core';
import { FullLayoutCard, useStepsContainerContext } from '@pcf/design-system';
import { queryCacheConfig } from 'app/app-providers';
import { useFeatureFlags } from 'app';

import { ConsignadoFormData } from '../models/consignado-form.model';
import { TiposEmprestimo } from '../models/tipos-emprestimo.enum';
import { ResultsDisplay } from '../../../components/results-display';
import { SimulationResult } from '../../../models';
import { usePostIntencaoOperacao } from '../../../utils/use-post-intencao-operacao';

queryCacheConfig.prefetchQuery(getProdutoOperacaoConsignadoQueryConfig());

export const ConsignadoResultsStep: FC<{
  onSetContinueWithAutoFillFlow: (continueWithAutoFlow: boolean) => void;
}> = ({ onSetContinueWithAutoFillFlow }) => {
  const { data, nextStep } = useStepsContainerContext<ConsignadoFormData>();
  const { flags } = useFeatureFlags();

  const request: SimulacaoNovoRequisicaoModel = useMemo(
    () => ({
      // idConvenio: data.tipoConvenio.id,
      idConvenio: data.matricula.convenio.id,
      retornarSomenteOperacoesViaveis: true,

      ...(data.tipoEmprestimo === TiposEmprestimo.valorTotal
        ? { valorOperacao: data.value }
        : { valorPrestacao: data.value }),
    }),
    [data],
  );

  const queryProps = useSimularConsignadoQuery(request, {
    useErrorBoundary: false,
  });

  const { data: produtoOp } = useProdutoOperacaoConsignadoQuery(undefined, {
    useErrorBoundary: false,
  });

  const { post, isPosting } = usePostIntencaoOperacao(
    produtoOp,
    data.matricula.id,
    {},
    flags.INCLUSAO_PROPOSTA_NOVA
      ? {
          information: 'Deseja prosseguir com a sua operação de crédito?',
          title: 'Inclusão de proposta',
          onConfirm: () => {
            onSetContinueWithAutoFillFlow(true);
            nextStep();
          },
          onCancel: () => {
            onSetContinueWithAutoFillFlow(false);
            nextStep();
          },
          onClose: () => {},
          closeOnClickOverlay: false,
          confirmText: 'Sim, quero continuar',
          closeText: 'Não, quero orientação',
        }
      : {},
  );

  const { data: queryResults } = queryProps;

  const filteredResults: SimulationResult[] = useMemo(() => {
    if (!queryResults?.length) return [];

    let filterResults: SimulationResult[] = [];

    if (data.prazo) {
      const filterFC = Array.isArray(data.prazo)
        ? (r: SimulacaoNovoRetornoModel) =>
            r.prazo >= (data.prazo as number[])[0] &&
            r.prazo <= (data.prazo as number[])[1]
        : (r: SimulacaoNovoRetornoModel) => r.prazo === data.prazo;

      filterResults = queryResults
        .filter(filterFC)
        .map((r) => ({ ...r, isCustomInterval: true }));
    }

    const defaultResults = queryResults
      .filter((r) => !filterResults.find((f) => f.prazo === r.prazo))
      .slice(0, 3);

    return [...filterResults, ...defaultResults];
  }, [queryResults, data.prazo]);

  return (
    <FullLayoutCard
      title={
        !data.prazo
          ? (filteredResults?.length &&
              'Resultados dos prazos disponíveis para você') ||
            undefined
          : 'Prazo personalizado'
      }
    >
      <ResultsDisplay
        queryProps={
          { ...queryProps, data: filteredResults } as UseQueryResult<
            SimulationResult[],
            Error
          >
        }
        onSubmit={post}
        isSubmitting={isPosting}
      />
    </FullLayoutCard>
  );
};
