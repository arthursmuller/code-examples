import { FC } from 'react';

import { Button, GridItem } from '@chakra-ui/react';

import {
  DadosPessoaisForm,
  useDadosPessoais,
} from 'features/main/features/perfil/dados-pessoais';

import { BaseLayoutStep } from './base-step-layout';

import { useAutoFillPersonalDataContext } from '../auto-fill-personal-data.context';

export const DadosBasicos: FC = () => {
  const { dadosPessoaisFormData, isLoading } = useDadosPessoais();
  const { onNext } = useAutoFillPersonalDataContext();

  return (
    <BaseLayoutStep isLoading={isLoading}>
      <DadosPessoaisForm
        initialData={dadosPessoaisFormData}
        onSuccess={() => {
          onNext();
        }}
        renderCustomSubmit={({ isLoading: isSubmiting }) => (
          <GridItem gridArea="salvar" pt={6} justifySelf="end">
            <Button
              colorScheme="secondary"
              type="submit"
              isLoading={isSubmiting}
            >
              Continuar
            </Button>
          </GridItem>
        )}
      />
    </BaseLayoutStep>
  );
};
