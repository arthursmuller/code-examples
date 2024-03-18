import { FC } from 'react';

import { useRouteMatch } from 'react-router-dom';

import { Loader, PageLayout } from '@pcf/design-system';
import { IntencaoOperacaoModel, useIntencaoOperacao } from '@pcf/core';
import { useSubRouteMenu } from 'features/main/components/use-sub-route-menu';
import { useNavigatePathUp } from 'hooks';

import { ResumoSolicitacaoInfo } from './resumo-solicitacao-info';

const title = 'Resumo da Solicitação';

export const ResumoSolicitacao: FC = () => {
  useSubRouteMenu(title);
  const navigateUp = useNavigatePathUp();
  const { params } = useRouteMatch();

  const { intencaoOperacaoId } = params as { intencaoOperacaoId: string };

  const { isLoading, data: intencaoOperacao } = useIntencaoOperacao({
    id: intencaoOperacaoId,
  });

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>{title}</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content hasErrorBoundary>
        {isLoading ? (
          <Loader />
        ) : (
          <ResumoSolicitacaoInfo
            intencaoOperacao={intencaoOperacao as IntencaoOperacaoModel}
          />
        )}
      </PageLayout.Content>
    </PageLayout>
  );
};
