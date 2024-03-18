import { FC } from 'react';

import { PageLayout } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

import { DadosCadastraisForm } from './dados-cadastrais-form';

export const DadosCadastrais: FC = () => {
  const navigateUp = useNavigatePathUp();

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Meus Dados Cadastrais</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <DadosCadastraisForm />
      </PageLayout.Content>
    </PageLayout>
  );
};
