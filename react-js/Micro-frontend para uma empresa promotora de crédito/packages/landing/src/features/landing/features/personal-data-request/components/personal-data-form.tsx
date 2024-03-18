import { FC } from 'react';

import { Flex, Text, Button, Grid } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  EmailInput,
  FormItemControl,
  BemTextInput,
  PhoneInput,
  useModal,
  ColorSchemes,
  BemDateInput,
  CustomHeading,
  getDefaultErrorModalConfig,
} from '@pcf/design-system';
import {
  extractReadableErrorMessage,
  useRequisicaoDadosPessoasMutation,
} from '@pcf/core';

interface RequisicaoDadosPessoaisForm {
  nome: string;
  sobrenome: string;
  dataNascimento: Date;
  nomeMae: string;
  email: string;
  telefone: string;
  motivo: string;
}

export const PersonalDataRequestForm: FC = () => {
  const {
    handleSubmit,
    formState: { errors },
    control,
    reset,
  } = useForm<RequisicaoDadosPessoaisForm>({ defaultValues: {} });

  const { mutate, isLoading } = useRequisicaoDadosPessoasMutation();
  const { showModal } = useModal();

  function onSubmit(data: RequisicaoDadosPessoaisForm): void {
    mutate(
      {
        nome: data.nome,
        sobrenome: data.sobrenome,
        dataNascimento: data.dataNascimento,
        nomeMae: data.nomeMae,
        email: data.email,
        motivo: data.motivo,
        telefoneCompleto: {
          ddd: data.telefone.slice(0, 2),
          telefone: data.telefone.slice(2),
        },
      },
      {
        onSuccess() {
          showModal({
            title: 'Seus dados foram solicitados',
            information: 'Em breve entraremos em contato com você',
            type: ColorSchemes.success,
            closeOnClickOverlay: true,
            onClose: () => reset(),
            closeText: 'Ok',
          });
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
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      direction="column"
      maxW="645px"
      width="100%"
      height="auto"
      minH="333px"
    >
      <Grid
        layerStyle="card"
        flexDir="column"
        p={['32px 24px', '32px 24px', '32px']}
        marginY={8}
        alignItems="center"
        gridTemplateColumns="1fr"
        gap={4}
      >
        <Flex
          color="secondary.mid-dark"
          justifyContent="center"
          width={['264px', '100%', '100%']}
        >
          <CustomHeading
            marginBottom={6}
            as="h3"
            textStyle={['bold32', 'bold32', 'bold40']}
            textAlign="center"
          >
            Acesso de Dados Pessoais
          </CustomHeading>
        </Flex>

        <Text color="grey.800" textAlign="center" marginBottom={6}>
          Para obter suas informações pessoais dentro da empresa, informe os
          dados abaixo, e entraremos em contato com você.
        </Text>

        <FormItemControl
          label="Nome"
          name="nome"
          required
          defaultValue=""
          control={control}
          as={BemTextInput}
          errorMessage={errors?.nome?.message}
        />

        <FormItemControl
          label="Sobrenome"
          name="sobrenome"
          required
          defaultValue=""
          control={control}
          as={BemTextInput}
          errorMessage={errors?.sobrenome?.message}
        />

        <FormItemControl
          label="Data de Nascimento"
          name="dataNascimento"
          errorMessage={errors?.dataNascimento?.message}
          control={control}
          as={BemDateInput}
          required
        />

        <FormItemControl
          label="Nome da Mãe"
          name="nomeMae"
          required
          defaultValue=""
          control={control}
          as={BemTextInput}
          errorMessage={errors?.nomeMae?.message}
        />

        <EmailInput
          label="E-mail"
          errorMessage={errors?.email?.message}
          control={control}
          required
        />

        <PhoneInput
          label="Telefone"
          name="telefone"
          defaultValue=""
          errorMessage={errors?.telefone?.message}
          control={control}
          required
        />

        <FormItemControl
          label="Motivo"
          name="motivo"
          defaultValue=""
          control={control}
          as={BemTextInput}
          type="textarea"
          height="128px"
          required
          errorMessage={errors?.motivo?.message}
        />
      </Grid>

      <Flex>
        <Button type="submit" isLoading={isLoading} isFullWidth>
          Solicitar dados
        </Button>
      </Flex>
    </Flex>
  );
};
