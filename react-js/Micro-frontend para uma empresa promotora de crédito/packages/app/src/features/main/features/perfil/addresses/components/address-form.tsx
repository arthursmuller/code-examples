import { FC, useEffect } from 'react';

import NumberFormat from 'react-number-format';
import { GridItem, Button, Grid, Checkbox, Flex } from '@chakra-ui/react';
import { Controller, useForm } from 'react-hook-form';
import { isValidCEP } from '@brazilian-utils/brazilian-utils';
import { Link } from 'react-router-dom';

import { BemTextInput, FormItemControl } from '@pcf/design-system';
import { CepModel, getEndereco } from '@pcf/core';
import { UnloadPrompt } from 'components/unload-prompt';

import { CepSearch } from './cep-search';

import { SelectMunicipios } from '../../components/select-municipios';
import { SelectUnidadesFederativas } from '../../components/select-unidades-federativas';
import { SelectTiposLogradouro } from '../../components/select-tipos-logradouro';
import { PerfilRoutesPaths } from '../../perfil.routes.enum';

const mobileTemplateAreas = `
"titulo"
"cep"
"busca-cep"
"tipo-logradouro"
"logradouro"
"numero"
"complemento"
"bairro"
"uf"
"cidade"
"checkbox"
"salvar"
"novo"
`;

const desktopTemplateAreas = `
"titulo             ."
"cep                busca-cep"
"tipo-logradouro    logradouro"
"numero             complemento"
"bairro             uf"
"cidade             checkbox"
"novo               salvar"
`;

export interface AddressFormData {
  titulo: string;
  cep?: string;
  idNaturalidade?: string;
  idCidadeNatal?: string;
  cidadeNatal?: string;
  bairro?: string;
  idTipoLogradouro: string;
  logradouro: string;
  numero: string;
  complemento: string;
  enderecoPrincipal: boolean;
}

interface AddressFormProps {
  onSuccess(data: AddressFormData): void;
  initialData?: AddressFormData;
  isSubmiting?: boolean;
  hasToBePrincipal?: boolean;
  showNewLink?: boolean;
}

export const AddressForm: FC<AddressFormProps> = ({
  onSuccess,
  initialData,
  isSubmiting,
  hasToBePrincipal,
  showNewLink = true,
}) => {
  const {
    handleSubmit,
    control,
    watch,
    setValue,
    formState: { isDirty, errors },
    reset,
  } = useForm<AddressFormData>({ defaultValues: initialData });

  function onSubmit(data: AddressFormData): void {
    reset({}, { keepDirty: false, keepValues: true });
    onSuccess(data);
  }

  const updateValues = (endereco: CepModel): void => {
    const { bairro, cidade, logradouro, tipoLogradouro, cep } = endereco;

    if (cep) {
      setValue('cep', cep);
    }

    if (bairro) {
      setValue('bairro', bairro);
    }

    if (cidade?.uf.id) {
      setValue('idNaturalidade', String(cidade?.uf.id));
    }

    if (cidade?.id) {
      setValue('idCidadeNatal', String(cidade.id));
    }

    if (tipoLogradouro?.id) {
      setValue('idTipoLogradouro', String(tipoLogradouro?.id));
    }

    if (logradouro) {
      setValue('logradouro', logradouro);
    }
  };

  async function handleCEPblur(e): Promise<void> {
    const { value: cep } = e.target;

    if (cep && isValidCEP(cep)) {
      try {
        const endereco = await getEndereco(cep.replace('-', ''));

        endereco && updateValues(endereco);
      } catch (error) {
        console.log(error); //eslint-disable-line
      }
    }
  }

  const idNaturalidadeWatched = watch(
    'idNaturalidade',
    initialData?.idNaturalidade,
  );

  const [
    idNaturalidade,
    idCidadeNatal,
    bairro,
    cidadeNatal,
    logradouro,
    idTipoLogradouro,
  ] = watch([
    'idNaturalidade',
    'idCidadeNatal',
    'bairro',
    'cidadeNatal',
    'logradouro',
    'idTipoLogradouro',
  ]);
  const data = {
    idNaturalidade,
    idCidadeNatal,
    bairro,
    cidadeNatal,
    logradouro,
    idTipoLogradouro,
  };

  useEffect(() => {
    if (idCidadeNatal && !idNaturalidadeWatched) {
      setValue('idCidadeNatal', '', { shouldDirty: true });
    }
  }, [idNaturalidadeWatched, setValue]); // eslint-disable-line

  return (
    <Grid
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      my={8}
      gridRowGap={[2, 2, 4]}
      gridColumnGap={6}
      alignItems="center"
      gridTemplateColumns={['1fr', '1fr', 'repeat(2, 1fr)']}
      gridTemplateAreas={[
        mobileTemplateAreas,
        mobileTemplateAreas,
        desktopTemplateAreas,
      ]}
    >
      <UnloadPrompt shouldBlock={isDirty} />

      <GridItem gridArea="titulo">
        <FormItemControl
          label="Título (opcional)"
          name="titulo"
          defaultValue={initialData?.titulo}
          errorMessage={errors?.titulo?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="cep">
        <FormItemControl
          label="CEP"
          name="cep"
          defaultValue={initialData?.cep}
          errorMessage={errors?.cep?.message}
          control={control}
          as={NumberFormat}
          onBlur={handleCEPblur}
          format="#####-###"
          mask="_"
          required
          rules={{
            validate: {
              cepValidator(cep) {
                if (!isValidCEP(cep)) {
                  return 'CEP inválido';
                }

                return true;
              },
            },
          }}
        />
      </GridItem>

      <GridItem
        gridArea="busca-cep"
        justifyContent={['center', 'flex-start', 'flex-start']}
        display="flex"
      >
        <CepSearch onSubmit={updateValues} data={data} />
      </GridItem>

      <GridItem gridArea="tipo-logradouro">
        <FormItemControl
          name="idTipoLogradouro"
          label="Tipo de Logradouro"
          defaultValue={initialData?.idTipoLogradouro}
          errorMessage={errors?.idTipoLogradouro?.message}
          required
          control={control}
          as={SelectTiposLogradouro}
        />
      </GridItem>

      <GridItem gridArea="logradouro">
        <FormItemControl
          label="Logradouro"
          name="logradouro"
          required
          defaultValue={initialData?.logradouro}
          errorMessage={errors?.logradouro?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="numero">
        <FormItemControl
          label="Número"
          name="numero"
          required
          defaultValue={initialData?.numero}
          errorMessage={errors?.numero?.message}
          control={control}
          as={NumberFormat}
          isNumericString
        />
      </GridItem>

      <GridItem gridArea="complemento">
        <FormItemControl
          label="Complemento (opcional)"
          name="complemento"
          defaultValue={initialData?.complemento}
          errorMessage={errors?.complemento?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="bairro">
        <FormItemControl
          label="Bairro"
          name="bairro"
          required
          defaultValue={initialData?.bairro}
          errorMessage={errors?.bairro?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="uf">
        <FormItemControl
          name="idNaturalidade"
          label="UF"
          defaultValue={initialData?.idNaturalidade}
          errorMessage={errors?.idNaturalidade?.message}
          required
          control={control}
          as={SelectUnidadesFederativas}
        />
      </GridItem>

      <GridItem gridArea="cidade">
        <FormItemControl
          name="idCidadeNatal"
          label="Cidade"
          defaultValue={initialData?.idCidadeNatal}
          municipioText={initialData?.cidadeNatal}
          errorMessage={errors?.idCidadeNatal?.message}
          required
          control={control}
          as={SelectMunicipios}
          disabled={!idNaturalidadeWatched}
          idUF={idNaturalidadeWatched}
        />
      </GridItem>

      <GridItem gridArea="checkbox">
        <Controller
          name="enderecoPrincipal"
          control={control}
          defaultValue={initialData?.enderecoPrincipal || hasToBePrincipal}
          render={({ field: { name, onChange, value } }) => {
            return (
              <Checkbox
                name={name}
                onChange={(e) => onChange(e.target.checked)}
                colorScheme="secondary"
                size="sm"
                mb="16px"
                defaultChecked={!!value}
                isDisabled={hasToBePrincipal}
              >
                Marcar estas informações como meu endereço principal
              </Checkbox>
            );
          }}
        />
      </GridItem>

      <GridItem gridArea="novo" pt={6}>
        {showNewLink && (
          <Flex flexDirection={['column', 'column', 'row-reverse']}>
            <Button
              colorScheme="secondary"
              size="sm"
              variant="link"
              as={Link}
              to={`${PerfilRoutesPaths.enderecos}/novo`}
            >
              Cadastrar novo Endereço
            </Button>
          </Flex>
        )}
      </GridItem>

      <GridItem gridArea="salvar" pt={6}>
        <Button
          colorScheme="secondary"
          type="submit"
          isLoading={isSubmiting}
          isFullWidth
          loadingText="Salvando"
        >
          Salvar Alterações
        </Button>
      </GridItem>
    </Grid>
  );
};
