import { useEffect, FC } from 'react';

import { GridItem, Button, Grid } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import { format } from 'date-fns';
import { useQueryClient } from 'react-query';

import {
  useAtualizarCliente,
  getClienteLogadoQueryConfig,
  extractReadableErrorMessage,
} from '@pcf/core';
import {
  BemTextInput,
  useModal,
  BemDateInput,
  CpfInput,
  FormItemControl,
  getDefaultErrorModalConfig,
} from '@pcf/design-system';
import { UnloadPrompt } from 'components/unload-prompt';

import {
  SelectDeficienteVisual,
  SelectEstadosCivil,
  SelectGenero,
  SelectGrausInstrucao,
} from './components';

import { SelectMunicipios, SelectUnidadesFederativas } from '../components';

const mobileTemplateAreas = `
"cpf"
"nome-completo"
"sexo"
"data-nascimento"
"filiacao-1"
"filiacao-2"
"grau-instrucao"
"estado-civil"
"naturalidade"
"cidade"
"deficiente-visual"
"salvar"
`;

const desktopTemplateAreas = `
"cpf           cpf               nome-completo       nome-completo"
"sexo          data-nascimento   filiacao-1          filiacao-1"
"filiacao-2    filiacao-2        grau-instrucao      grau-instrucao"
"estado-civil  estado-civil      naturalidade        naturalidade"
"cidade        cidade            deficiente-visual   deficiente-visual"
".             .                 salvar              salvar"
`;

export interface DadosPessoaisFormData {
  cpf: string;
  idGenero: string | undefined;
  idEstadoCivil: string | undefined;
  idGrauInstrucao: string | undefined;
  idCidadeNatal?: string | undefined;
  idNaturalidade: string | undefined;
  nome: string;
  dataNascimento: Date | undefined;
  filiacao1: string;
  filiacao2: string;
  deficienteVisual: string;
  email: string | undefined;
}

interface DadosPessoaisFormProps {
  initialData: DadosPessoaisFormData;
  renderCustomSubmit?: ({
    isLoading,
  }: {
    isLoading: boolean;
  }) => React.ReactElement;
  onSuccess?: () => void;
}

export const DadosPessoaisForm: FC<DadosPessoaisFormProps> = ({
  initialData,
  renderCustomSubmit,
  onSuccess,
}) => {
  const {
    handleSubmit,
    control,
    watch,
    setValue,
    getValues,
    formState: { errors, isDirty },
    reset,
  } = useForm<DadosPessoaisFormData>({
    defaultValues: initialData,
  });

  const { mutate: atualizarCliente, isLoading: isUpdating } =
    useAtualizarCliente();

  const idNaturalidadeWatched = watch(
    'idNaturalidade',
    initialData.idNaturalidade,
  );

  const idCidadeNatalValue = getValues('idCidadeNatal');

  useEffect(() => {
    if (idCidadeNatalValue && !idNaturalidadeWatched) {
      setValue('idCidadeNatal', '', { shouldDirty: true });
    }
  }, [idNaturalidadeWatched, setValue]); // eslint-disable-line

  const { showModal } = useModal();
  const queryCache = useQueryClient();

  function onSubmit(data: DadosPessoaisFormData): void {
    const {
      idGenero,
      idEstadoCivil,
      idGrauInstrucao,
      idCidadeNatal,
      nome,
      dataNascimento,
      filiacao1,
      filiacao2,
      deficienteVisual,
    } = data;

    const request = {
      idGenero: Number(idGenero),
      idEstadoCivil: Number(idEstadoCivil),
      idGrauInstrucao: Number(idGrauInstrucao),
      idCidadeNatal: Number(idCidadeNatal),
      nome,
      dataNascimento: format(dataNascimento as Date, 'yyyy-MM-dd'),
      filiacao1,
      filiacao2,
      deficienteVisual: deficienteVisual === 'true',
      email: 'placeholder@todo.todo',
    };

    if (isDirty) {
      atualizarCliente(request, {
        onSuccess(updatedCliente) {
          queryCache.setQueryData(
            getClienteLogadoQueryConfig().queryKey ?? '',
            updatedCliente,
          );

          reset({}, { keepDirty: false, keepValues: true });

          if (onSuccess) {
            onSuccess();
          } else {
            showModal({
              title: 'Dados Atualizados!',
              information: 'Seus dados foram atualizados com sucesso!',
              closeOnClickOverlay: true,
              closeText: 'Ok',
              onClose: () => {},
            });
          }
        },
        onError(error) {
          showModal(
            getDefaultErrorModalConfig({
              information: extractReadableErrorMessage(error),
            }),
          );
        },
      });
    } else if (onSuccess) {
      onSuccess();
    }
  }

  return (
    <Grid
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      my={8}
      gridRowGap={4}
      gridColumnGap={6}
      w="100%"
      gridTemplateColumns={['1fr', '1fr', 'repeat(4, 1fr)']}
      gridTemplateAreas={[
        mobileTemplateAreas,
        mobileTemplateAreas,
        desktopTemplateAreas,
      ]}
    >
      <UnloadPrompt shouldBlock={isDirty} />

      <GridItem gridArea="cpf">
        <CpfInput
          control={control}
          defaultValue={initialData?.cpf}
          errors={errors}
          disabled
          hasStatusIcon
        />
      </GridItem>

      <GridItem gridArea="nome-completo">
        <FormItemControl
          label="Nome Completo"
          name="nome"
          required
          defaultValue={initialData?.nome}
          errorMessage={errors?.nome?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="sexo">
        <FormItemControl
          name="idGenero"
          label="Sexo"
          defaultValue={initialData?.idGenero}
          errorMessage={errors?.idGenero?.message}
          required
          control={control}
          as={SelectGenero}
        />
      </GridItem>

      <GridItem gridArea="data-nascimento">
        <FormItemControl
          label="Data de Nascimento"
          name="dataNascimento"
          required
          errorMessage={errors?.dataNascimento?.message}
          defaultValue={initialData?.dataNascimento}
          control={control}
          as={BemDateInput}
        />
      </GridItem>

      <GridItem gridArea="filiacao-1">
        <FormItemControl
          label="Filiação 1"
          name="filiacao1"
          required
          defaultValue={initialData?.filiacao1}
          errorMessage={errors?.filiacao1?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="filiacao-2">
        <FormItemControl
          label="Filiação 2"
          name="filiacao2"
          required
          defaultValue={initialData?.filiacao2}
          errorMessage={errors?.filiacao2?.message}
          control={control}
          as={BemTextInput}
        />
      </GridItem>

      <GridItem gridArea="grau-instrucao">
        <FormItemControl
          name="idGrauInstrucao"
          label="Grau de Instrução"
          defaultValue={initialData?.idGrauInstrucao}
          errorMessage={errors?.idGrauInstrucao?.message}
          required
          control={control}
          as={SelectGrausInstrucao}
        />
      </GridItem>

      <GridItem gridArea="estado-civil">
        <FormItemControl
          name="idEstadoCivil"
          label="Estado Civil"
          defaultValue={initialData?.idEstadoCivil}
          errorMessage={errors?.idEstadoCivil?.message}
          required
          control={control}
          as={SelectEstadosCivil}
        />
      </GridItem>

      <GridItem gridArea="naturalidade">
        <FormItemControl
          name="idNaturalidade"
          label="Naturalidade"
          defaultValue={initialData.idNaturalidade}
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
          defaultValue={initialData.idCidadeNatal}
          errorMessage={errors?.idCidadeNatal?.message}
          required
          control={control}
          as={SelectMunicipios}
          disabled={!idNaturalidadeWatched}
          idUF={idNaturalidadeWatched}
        />
      </GridItem>

      <GridItem gridArea="deficiente-visual">
        <FormItemControl
          name="deficienteVisual"
          label="Deficiente Visual"
          defaultValue={initialData.deficienteVisual}
          errorMessage={errors?.deficienteVisual?.message}
          required
          control={control}
          as={SelectDeficienteVisual}
        />
      </GridItem>

      {(renderCustomSubmit &&
        renderCustomSubmit({ isLoading: isUpdating })) || (
        <GridItem gridArea="salvar" pt={6}>
          <Button
            colorScheme="secondary"
            type="submit"
            isFullWidth
            isLoading={isUpdating}
            loadingText="Salvando"
          >
            Salvar
          </Button>
        </GridItem>
      )}
    </Grid>
  );
};
