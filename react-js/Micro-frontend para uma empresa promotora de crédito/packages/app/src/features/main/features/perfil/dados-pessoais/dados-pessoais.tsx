import { FC } from 'react';

import { PageLayout, Loader } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

import {
  DadosPessoaisForm,
  DadosPessoaisFormData,
} from './dados-pessoais-form';
import { useDadosPessoais } from './use-dados-pessoais';

export const DadosPessoais: FC = () => {
  const navigateUp = useNavigatePathUp();
  const { isLoading, dadosPessoaisFormData } = useDadosPessoais();

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Meus dados pessoais</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        {isLoading ? (
          <Loader />
        ) : (
          <DadosPessoaisForm
            initialData={dadosPessoaisFormData as DadosPessoaisFormData}
          />
        )}
      </PageLayout.Content>
    </PageLayout>
  );
};
