import { BemApiRetorno } from 'common/bem-api-retorno.model';
import { api } from 'common/client';

export interface SituacaoPropostaModel {
  proposta: number;
  descricaoSituacao?: string;
  explicacaoSituacao?: string;
}

export interface ConsultarSituacaoPropostaPayload {
  cpf: string;
  token: string;
  dataNascimento: string;
}

export const consultarProposta = async ({
  cpf,
  token,
  dataNascimento,
}: ConsultarSituacaoPropostaPayload): Promise<SituacaoPropostaModel> => {
  const response = await api.get<BemApiRetorno<SituacaoPropostaModel>>(
    `/clientes/${cpf}/consultas-proposta/${token}/situacao`,
    {
      params: {
        dataNascimento,
      },
    },
  );

  return response?.data.retorno;
};
