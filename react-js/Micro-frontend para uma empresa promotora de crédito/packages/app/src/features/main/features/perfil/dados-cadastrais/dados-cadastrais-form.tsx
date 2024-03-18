import { FC } from 'react';

import { Flex, Button, Grid } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  CustomHeading,
  EmailInput,
  FormItemControl,
  BemTextInput,
  PhoneInput,
  useModal,
  ColorSchemes,
  BemDateInput,
} from '@pcf/design-system';
import { useRequisicaoDadosPessoasMutation } from '@pcf/core';
import { useNavigatePathUp } from 'hooks';

interface RequisicaoDadosPessoaisForm {
  nome: string;
  sobrenome: string;
  dataNascimento: Date;
  nomeMae: string;
  email: string;
  telefone: string;
  motivo: string;
}

export const DadosCadastraisForm: FC = () => {
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<RequisicaoDadosPessoaisForm>();
  const navigateBack = useNavigatePathUp();

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
            closeText: 'Ok',
            onClose: navigateBack,
          });
        },
      },
    );
  }

  return (
    <Flex
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      direction="column"
      width="100%"
      marginBottom={10}
    >
      <Flex color="secondary.mid-dark">
        <CustomHeading
          marginTop={6}
          marginBottom={8}
          as="h3"
          textStyle={['bold24', 'bold24', 'bold32']}
        >
          Solicite seus dados cadastrais abaixo
        </CustomHeading>
      </Flex>

      <Grid
        gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
        gridRowGap={4}
        gridColumnGap={6}
      >
        <FormItemControl
          label="Nome"
          name="nome"
          required
          defaultValue=""
          control={control}
          as={BemTextInput}
          errorMessage={errors?.nome?.message}
          maxlength={100}
        />

        <FormItemControl
          label="Sobrenome"
          name="sobrenome"
          required
          defaultValue=""
          control={control}
          as={BemTextInput}
          errorMessage={errors?.sobrenome?.message}
          maxlength={100}
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
          maxlength={100}
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
          maxlength={8000}
        />

        <span />
        <span />

        <Button
          type="submit"
          isLoading={isLoading}
          isFullWidth
          colorScheme="secondary"
        >
          Solicitar dados
        </Button>
      </Grid>
    </Flex>
  );
};
