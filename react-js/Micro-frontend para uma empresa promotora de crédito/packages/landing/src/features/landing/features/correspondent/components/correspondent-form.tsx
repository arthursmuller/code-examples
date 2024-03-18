import { FC, useEffect, useState } from 'react';

import { Flex, Button, Grid, GridItem } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  BemTextInput,
  CustomHeading,
  EmailInput,
  FormItemControl,
  PhoneInput,
  CnpjInput,
  useModal,
} from '@pcf/design-system';
import {
  LeadCorrespondenteCriacaoModel,
  useGravarLeadCorrespondente,
} from '@pcf/core';

import { SelectUnidadesFederativas } from './select-unidades-federativas';
import { SelectMunicipios } from './select-municipios';

interface CorrespondentFormData {
  telefone: string;
  nome: string;
  email: string;
  cnpj: string;
  atividades: string;
  idMunicipio: string;
  idUf: string;
}

export const CorrespondentForm: FC = () => {
  const {
    handleSubmit,
    formState: { errors },
    control,
    setValue,
    watch,
    reset,
  } = useForm<CorrespondentFormData>();
  const { mutate, isLoading } = useGravarLeadCorrespondente();
  const { showModal } = useModal();

  // TODO: update React-hook-form, workaround for resetting form.
  const [key, setUpdateKey] = useState<number>(1);

  function onSubmit(data: CorrespondentFormData): void {
    const request: LeadCorrespondenteCriacaoModel = {
      telefone: data.telefone,
      nome: data.nome,
      email: data.email,
      cnpj: data.cnpj,
      atividades: data.atividades,
      idMunicipio: +data.idMunicipio,
    };

    mutate(request, {
      onSuccess: () => {
        showModal({
          title: 'Pronto! Seus dados de contato foram enviados',
          closeOnClickOverlay: false,
          closeText: 'Fechar',
          onClose: () => {
            reset({}, { keepDirty: false, keepErrors: false });
            setUpdateKey((key) => key + 1);
          },
        });
      },
      onError: () => {
        showModal({
          title:
            'Ops! Um erro ocorreu ao enviar suas informações, por favor, tente novamente',
          closeOnClickOverlay: false,
          closeText: 'Fechar',
          type: 'error',
        });
      },
    });
  }

  const [idUfWatched, idMunicipio] = watch(['idUf', 'idMunicipio']);

  useEffect(() => {
    if (!idUfWatched && idMunicipio) {
      setValue('idMunicipio', '', { shouldDirty: true });
    }
  }, [idUfWatched, setValue]); // eslint-disable-line

  return (
    <Flex
      key={key}
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      flexDir="column"
      alignItems="center"
      marginBottom="72px"
      marginX={6}
      textAlign="center"
    >
      <CustomHeading color="secondary.regular" textStyle="bold24_32">
        Envie seus dados e aguarde nosso contato.
      </CustomHeading>

      <Flex
        mt={10}
        layerStyle="card"
        direction="column"
        width={['100%', '100%', 645]}
      >
        <Grid
          paddingTop={2}
          gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
          columnGap={3}
          rowGap={2}
        >
          <FormItemControl
            label="Nome Completo"
            name="nome"
            defaultValue=""
            errorMessage={errors?.nome?.message}
            control={control}
            as={BemTextInput}
            required
          />

          <EmailInput
            label="E-mail"
            defaultValue=""
            errorMessage={errors?.email?.message}
            control={control}
            required
          />

          <CnpjInput
            control={control}
            defaultValue=""
            errorMessage={errors?.cnpj?.message}
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
            name="idUf"
            label="UF"
            defaultValue=""
            errorMessage={errors?.idUf?.message}
            required
            control={control}
            as={SelectUnidadesFederativas}
          />

          <FormItemControl
            name="idMunicipio"
            label="Cidade"
            defaultValue=""
            municipioText=""
            errorMessage={errors?.idMunicipio?.message}
            required
            control={control}
            as={SelectMunicipios}
            disabled={!idUfWatched}
            idUF={idUfWatched}
          />

          <GridItem gridColumn={['', '', 'span 2']}>
            <FormItemControl
              label="Descreva as atividades da sua empresa"
              name="atividades"
              defaultValue=""
              control={control}
              as={BemTextInput}
              type="textarea"
              height="128px"
            />
          </GridItem>
        </Grid>

        <Flex justifyContent="flex-end">
          <Button type="submit" isLoading={isLoading}>
            Enviar
          </Button>
        </Flex>
      </Flex>
    </Flex>
  );
};
