import { FC } from 'react';

import { useParams, useHistory } from 'react-router-dom';

import { PageLayout, Loader, BemErrorBoundary } from '@pcf/design-system';
import {
  getRendimentosQueryConfig,
  RendimentoResponseModel,
  Resource,
} from '@pcf/core';
import { useNavigatePathUp } from 'hooks';

import { EditMatriculaContent } from './edit-matricula-content';

import { PerfilRoutesPaths } from '../perfil.routes.enum';

export const EditMatricula: FC = () => {
  const navigateUp = useNavigatePathUp();
  const params = useParams();
  const history = useHistory();

  const rendimentoID = parseInt(
    (params as { [key: string]: string }).matriculaId,
    10,
  );

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Matrículas disponíveis</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <BemErrorBoundary>
          <Resource<RendimentoResponseModel[]>
            path={getRendimentosQueryConfig().queryKey ?? ''}
            render={({ data: rendimentos }) => (
              <EditMatriculaContent
                rendimentoId={rendimentoID}
                rendimentos={rendimentos}
                onSuccess={() => {
                  history.push(PerfilRoutesPaths.rendimentos);
                }}
              />
            )}
            loaderComponent={<Loader />}
          />
        </BemErrorBoundary>
      </PageLayout.Content>
    </PageLayout>
  );
};
