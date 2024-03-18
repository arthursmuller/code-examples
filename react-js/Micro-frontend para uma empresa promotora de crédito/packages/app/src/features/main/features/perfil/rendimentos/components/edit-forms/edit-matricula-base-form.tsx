import { FC, ReactElement } from 'react';

import { Button, Flex, Grid, Text } from '@chakra-ui/react';

import { useRendimentosQuery } from '@pcf/core';
import { UnloadPrompt } from 'components';

import { usePersistMatricula } from '../use-persist-matricula';
import { MatriculaInssFormModel } from '../../models/matricula-inss-form.model';
import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';
import { BaseEditMatriculaProps } from '../../models/base-edit-matricula-props';

interface EditMatriculaBaseFormProps extends BaseEditMatriculaProps {
  formHandle;
  matricula?: MatriculaInssFormModel | MatriculaSiapeFormModel;
  matriculaId?: number;
  children: ReactElement | Array<ReactElement>;
  title: string;
  isDirty: boolean;
}

export const EditMatriculaBaseForm: FC<EditMatriculaBaseFormProps> = ({
  children,
  formHandle,
  matricula,
  matriculaId,
  title,
  isDirty,
  onSuccess,
  useUnloadPrompt,
}) => {
  const { refetch } = useRendimentosQuery(undefined, { enabled: false });

  const { onSubmit, isSubmiting } = usePersistMatricula(
    {
      tipoConvenio: matricula.tipoConvenio,
      idContaCliente: matricula.idContaCliente,
      idFormaRecebimento: matricula.idFormaRecebimento,
    },
    matriculaId,
    onSuccess,
    () => {
      refetch();
    },
  );

  return (
    <Flex direction="column" flex={1} as="form" onSubmit={formHandle(onSubmit)}>
      <Text
        as="h2"
        textStyle="bold24_32"
        color="secondary.mid-dark"
        marginBottom="16px"
      >
        {title}
      </Text>

      {useUnloadPrompt && <UnloadPrompt shouldBlock={isDirty} />}

      {children}

      <Grid
        marginY={6}
        gridColumnGap={6}
        gridTemplateColumns={['1fr', '1fr', 'repeat(2, 1fr)']}
        gridTemplateAreas={[`"submit"`, `"submit"`, `".        submit"`]}
      >
        <Button
          gridArea="submit"
          colorScheme="secondary"
          type="submit"
          isLoading={isSubmiting}
          isFullWidth
          loadingText="Salvando"
        >
          Salvar alterações
        </Button>
      </Grid>
    </Flex>
  );
};
