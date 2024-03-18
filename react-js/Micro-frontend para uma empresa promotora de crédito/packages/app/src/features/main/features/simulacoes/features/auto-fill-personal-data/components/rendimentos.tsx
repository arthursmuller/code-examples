import { FC } from 'react';

import { Button, Flex } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import { ListaMatriculas } from 'features/main/features/perfil/rendimentos/components/lista-matriculas';
import { useCreateMatricula } from 'features/main/features/perfil/rendimentos/use-create-matricula';
import { useRendimentosQuery } from '@pcf/core';
import { useEditMatricula } from 'features/main/features/perfil/rendimentos/use-edit-matricula-modal';

import { BaseLayoutStep } from './base-step-layout';

export const Rendimentos: FC = () => {
  const { data, isLoading } = useRendimentosQuery(undefined);
  const showEditDialog = useEditMatricula();
  const showCreateDialog = useCreateMatricula();

  const hasError = !data?.length;
  const errorMessage = 'É necessário cadastrar um benefício';

  const { control } = useForm<{
    matricula: string;
  }>({ mode: 'onChange' });

  return (
    <BaseLayoutStep isLoading={isLoading}>
      <Flex flexDir="column">
        <ListaMatriculas control={control} onEdit={showEditDialog} />

        <Button
          gridArea="create"
          variant="link"
          color="secondary.regular"
          onClick={showCreateDialog}
        >
          Cadastrar novo Benefício
        </Button>
      </Flex>

      <BaseLayoutStep.Footer hasErrors={hasError} errorMessage={errorMessage} />
    </BaseLayoutStep>
  );
};
