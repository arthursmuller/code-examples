import { FC, useEffect } from 'react';

import { useForm } from 'react-hook-form';
import { Flex, Grid, Button, useBreakpointValue } from '@chakra-ui/react';

import {
  BemTextInput,
  FormItemControl,
  useStepsContainerContext,
} from '@pcf/design-system';

import { SelectMunicipios } from '../../../components/select-municipios';
import { SelectUnidadesFederativas } from '../../../components/select-unidades-federativas';
import { SelectTiposLogradouro } from '../../../components/select-tipos-logradouro';

export interface CepSearchFormData {
  idNaturalidade?: string;
  idCidadeNatal?: string;
  bairro?: string;
  idTipoLogradouro?: string;
  logradouro?: string;
  cidadeNatal?: string;
}

interface CepSearchFormProps {
  initialData?: CepSearchFormData;
}

export const CepSearchForm: FC<CepSearchFormProps> = ({ initialData = {} }) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');
  const { previousStep, nextStep, data } = useStepsContainerContext();

  const defaultData = data?.idNaturalidade ? data : initialData;

  const {
    handleSubmit,
    control,
    watch,
    setValue,
    getValues,
    formState: { errors },
  } = useForm<CepSearchFormData>({ defaultValues: defaultData });

  const idNaturalidadeWatched = watch(
    'idNaturalidade',
    defaultData?.idNaturalidade,
  );

  const idCidadeNatalValue = getValues('idCidadeNatal');

  useEffect(() => {
    if (idCidadeNatalValue && !idNaturalidadeWatched) {
      setValue('idCidadeNatal', '', { shouldDirty: true });
    }
  }, [idNaturalidadeWatched, setValue]); // eslint-disable-line

  return (
    <Flex direction="column" height="100%">
      <Grid
        gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
        gridRowGap={[2, 2, 4]}
        gridColumnGap={6}
        marginBottom={[2, 2, 4]}
      >
        <FormItemControl
          name="idTipoLogradouro"
          label="Tipo de Logradouro"
          defaultValue={defaultData?.idTipoLogradouro}
          errorMessage={errors?.idTipoLogradouro?.message}
          control={control}
          as={SelectTiposLogradouro}
        />

        <FormItemControl
          label="Logradouro"
          name="logradouro"
          defaultValue={defaultData?.logradouro}
          errorMessage={errors?.logradouro?.message}
          control={control}
          as={BemTextInput}
        />
      </Grid>

      <Flex marginBottom={[2, 2, 4]} direction="column">
        <FormItemControl
          label="Bairro"
          name="bairro"
          defaultValue={defaultData?.bairro}
          errorMessage={errors?.bairro?.message}
          control={control}
          as={BemTextInput}
        />
      </Flex>

      <Flex marginBottom={[2, 2, 4]} direction="column">
        <FormItemControl
          name="idNaturalidade"
          required
          label="UF"
          defaultValue={defaultData?.idNaturalidade}
          errorMessage={errors?.idNaturalidade?.message}
          control={control}
          as={SelectUnidadesFederativas}
        />
      </Flex>

      <Flex marginBottom={[2, 2, 4]} direction="column" flex={1}>
        <FormItemControl
          name="idCidadeNatal"
          label="Cidade"
          required
          defaultValue={defaultData?.idCidadeNatal}
          municipioText={defaultData?.cidadeNatal}
          errorMessage={errors?.idCidadeNatal?.message}
          control={control}
          as={SelectMunicipios}
          disabled={!idNaturalidadeWatched}
          idUF={idNaturalidadeWatched}
        />
      </Flex>

      <Flex justifyContent="flex-end">
        {!isMobile && (
          <Button colorScheme="grey" onClick={previousStep} marginRight={6}>
            Voltar
          </Button>
        )}

        <Button
          onClick={handleSubmit(nextStep)}
          width={['100%', '100%', 'auto']}
        >
          Buscar CEP
        </Button>
      </Flex>
    </Flex>
  );
};
