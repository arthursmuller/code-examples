import { FC } from 'react';

import { Flex, Grid, GridItem, Button } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  CpfInput,
  CustomHeading,
  FormItemControl,
  BemDateInput,
  BemTextInput,
} from '@pcf/design-system';
import { useAppContext } from 'app/app.context';

export interface ConsultaPropostaFormData {
  cpf: string;
  dataNascimento: string;
  token: string;
}

export interface ProposalSearchFormProps {
  onSubmit: (data: ConsultaPropostaFormData) => void;
  isSubmiting: boolean;
}

export const ProposalSearchForm: FC<ProposalSearchFormProps> = ({
  onSubmit,
  isSubmiting,
}) => {
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<ConsultaPropostaFormData>();

  const { currentCpf, setCurrentCpf } = useAppContext();

  function handleOnSubmit(data: ConsultaPropostaFormData): void {
    onSubmit(data);
  }

  return (
    <Flex
      layerStyle="card"
      flexDir="column"
      width="100%"
      maxWidth="822px"
      height="auto"
      p={['32px 24px 32px 24px', '32px 24px 32px 24px', '64px 108px 64px 108px']}
    >
      <CustomHeading
        as="h2"
        textStyle="bold24_32"
        color="secondary.mid-dark"
        textAlign="center"
        mb={[10, 10, 6]}
      >
        Consulta de Proposta
      </CustomHeading>

      <Grid
        gridTemplateColumns={['1fr', '1fr', '1fr 1fr', '1fr 1fr']}
        gap="4"
        as="form"
        onSubmit={handleSubmit(handleOnSubmit)}
      >
        <CpfInput
          control={control}
          errors={errors}
          defaultValue={currentCpf}
          onBlur={setCurrentCpf}
        />
        <FormItemControl
          label="Data de Nascimento"
          name="dataNascimento"
          required
          errorMessage={errors?.dataNascimento?.message}
          control={control}
          as={BemDateInput}
        />

        <GridItem gridColumn={['', '', 'span 2']}>
          <FormItemControl
            label="Token"
            name="token"
            defaultValue=""
            errorMessage={errors?.token?.message}
            control={control}
            as={BemTextInput}
            required
          />
        </GridItem>

        <GridItem gridColumn={['', '', 'span 2']}>
          <Button type="submit" isFullWidth isLoading={isSubmiting}>
            Consultar
          </Button>
        </GridItem>
      </Grid>
    </Flex>
  );
};
