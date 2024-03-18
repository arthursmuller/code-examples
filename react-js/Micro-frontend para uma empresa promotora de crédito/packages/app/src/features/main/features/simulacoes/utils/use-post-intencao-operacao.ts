import { useHistory } from 'react-router-dom';
import { useQueryClient } from 'react-query';

import {
  InformacaoProdutoOperacao,
  IntencaoOperacaoRequisicaoModel,
  INTENCAO_OPERACAO_ENDPOINT,
  SimulacaoNovoRetornoModel,
  useIntencaoOperacaoMutation,
} from '@pcf/core';
import { useJwt } from 'app/auth/useJwt';
import { ModalConfig, useModal } from '@pcf/design-system';

type basicTypes = string | number | string[] | number;

interface extraParams {
  [key: string]:
    | basicTypes
    | ((simulacao: SimulacaoNovoRetornoModel) => basicTypes | extraParams)
    | extraParams
    | extraParams[];
}

const objectMap = (
  obj: basicTypes | extraParams,
  data: SimulacaoNovoRetornoModel,
): extraParams =>
  Object.fromEntries(
    Object.entries(obj).map(([key, value]) => [
      key,
      typeof value === 'function'
        ? value(data)
        : (!!value &&
            typeof value === 'object' &&
            !Array.isArray(value) &&
            objectMap(value, data)) ||
          value,
    ]),
  );

export const usePostIntencaoOperacao = (
  produtoOp: InformacaoProdutoOperacao,
  idRendimentoCliente: number,
  extraParams: extraParams = {},
  successModalProps: ModalConfig = {},
): {
  post: ({ simulacao }: { simulacao: SimulacaoNovoRetornoModel }) => void;
  isPosting: boolean;
} => {
  const { mutate, isLoading: isPosting } = useIntencaoOperacaoMutation();
  const { userId } = useJwt();
  const { showModal } = useModal();
  const history = useHistory();
  const queryCache = useQueryClient();

  const post = ({
    simulacao,
  }: {
    simulacao: SimulacaoNovoRetornoModel;
  }): void => {
    const submitRequest: IntencaoOperacaoRequisicaoModel = {
      idUsuario: userId,
      idTipoOperacao: produtoOp?.idTipoOperacao ?? 0,
      idProduto: produtoOp?.idProduto ?? 0,
      prestacao: simulacao?.prestacao,
      valorAuxilioFinanceiro: simulacao?.valorAF,
      taxaMes: simulacao?.taxaMes,
      taxaAno: simulacao?.taxaAno,
      prazo: +simulacao?.prazo || 0,
      valorFinanciado: simulacao?.valorFinanciado,
      primeiroVencimento: simulacao?.primeiroVcto,
      idRendimentoCliente,
      custoEfetivoTotalMes: simulacao?.cetMes,
      custoEfetivoTotalAno: simulacao?.cetAno,
      impostoOperacaoFinanceira: simulacao?.valorIOF,
      ...objectMap(extraParams, simulacao),
    };

    mutate(submitRequest, {
      onSuccess() {
        queryCache.invalidateQueries(INTENCAO_OPERACAO_ENDPOINT);

        showModal({
          title: 'Pronto!',
          information:
            'A sua Solicitação de Proposta foi enviada com sucesso. Agora, é so aguardar.',
          closeOnClickOverlay: false,
          onClose: () => history.push('/'),
          ...successModalProps,
        });
      },
    });
  };

  return { post, isPosting };
};
