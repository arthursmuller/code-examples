import { FC } from 'react';

import { Flex, Grid, GridItem, Button } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import qs from 'qs';
import { useLocation } from 'react-use';

import {
  CpfInput,
  EmailInput,
  FormItemControl,
  BemTextInput,
  PhoneInput,
  useModal,
  getDefaultErrorModalConfig,
} from '@pcf/design-system';
import { extractReadableErrorMessage, useGravarLead } from '@pcf/core';
import { useAppContext } from 'app/app.context';
import { translateQueryParams } from 'features/captura-lead/utils/captura-lead-query-params';
import { openLink } from 'features/components/utils';

interface DischargeDocumentFormData {
  cpf: string;
  email: string;
  nome: string;
  telefone: string;
  celular: string;
}

const TERMO_PATH =
  '/assets/documentos/Modelo_de_carta_de_Solicitacao_para_Quitacao_Total_de_Emprestimo.pdf';

export const DischargeDocumentForm: FC = () => {
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<DischargeDocumentFormData>();
  const { currentCpf, setCurrentCpf } = useAppContext();
  const { mutate: gravarLead, isLoading } = useGravarLead();
  const { latitude, longitude } = useAppContext();
  const { showModal } = useModal();
  const { search = '' } = useLocation();
  const queryParams = qs.parse(search, { ignoreQueryPrefix: true });

  function onSubmit(data: DischargeDocumentFormData): void {
    gravarLead(
      {
        latitude,
        longitude,
        ...data,
        quitacao: true,
        ...translateQueryParams(queryParams),
      },
      {
        onSuccess() {
          openLink(TERMO_PATH, true);
        },
        onError(error) {
          showModal(
            getDefaultErrorModalConfig({
              information: extractReadableErrorMessage(error),
            }),
          );
        },
      },
    );
  }

  return (
    <Flex
      layerStyle="card"
      flexDir="column"
      maxW="645px"
      width="100%"
      height="auto"
      minH="333px"
      p={['32px 24px', '32px 24px', '32px']}
      as="form"
      onSubmit={handleSubmit(onSubmit)}
    >
      <Grid
        gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
        columnGap={3}
        rowGap={2}
      >
        <GridItem gridColumn={['', '', 'span 2']}>
          <FormItemControl
            label="Nome Completo"
            name="nome"
            required={false}
            defaultValue=""
            control={control}
            as={BemTextInput}
          />
        </GridItem>
        <EmailInput
          label="E-mail *"
          errorMessage={errors?.email?.message}
          control={control}
          required
        />
        <CpfInput
          defaultValue={currentCpf}
          onBlur={setCurrentCpf}
          label="CPF *"
          control={control}
          errors={errors}
        />
        <PhoneInput
          label="Telefone"
          name="telefone"
          defaultValue=""
          errorMessage={errors?.telefone?.message}
          control={control}
          required={false}
          acceptMobilePhone={false}
        />
        <PhoneInput
          label="Celular *"
          name="celular"
          defaultValue=""
          errorMessage={errors?.celular?.message}
          control={control}
          acceptLandlinePhone={false}
          required
        />
      </Grid>

      <Flex justifyContent="flex-end">
        <Button type="submit" isLoading={isLoading}>
          Enviar
        </Button>
      </Flex>
    </Flex>
  );
};
