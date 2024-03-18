import { FC } from 'react';

import { Controller, useForm } from 'react-hook-form';
import { Flex, Button, useBreakpointValue } from '@chakra-ui/react';
import { useMount } from 'react-use';

import {
  Loader,
  NoDataDisplay,
  RadioCardsGroup,
  useStepsContainerContext,
  BemErrorBoundary,
} from '@pcf/design-system';
import { CepModel, FindCepQuery, useFindCep } from '@pcf/core';

import { CepSearchResultCard } from './cep-search-result-card';
import { CepSearchFormData } from './cep-search-form';

interface CepSearchData {
  cep: CepModel;
}

interface CepSearchResultsContentProps {
  onSubmit?: (selection: CepModel) => void;
}

export const CepSearchResultsContent: FC<CepSearchResultsContentProps> = ({
  onSubmit,
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const {
    previousStep,
    finish,
    data: formData,
  } = useStepsContainerContext<CepSearchFormData>();

  const qs: FindCepQuery = {
    idUnidadeFederativa: formData.idNaturalidade
      ? +formData.idNaturalidade
      : undefined,
    idTipoLogradouro: formData.idTipoLogradouro
      ? +formData.idTipoLogradouro
      : undefined,
    idMunicipio: formData.idCidadeNatal ? +formData.idCidadeNatal : undefined,
    logradouro: formData.logradouro,
    bairro: formData.bairro,
  };

  const { isLoading, data: ceps = [] } = useFindCep(qs);

  const {
    handleSubmit,
    control,
    formState: { isValid },
    trigger,
  } = useForm<CepSearchData>({ mode: 'onChange' });

  useMount(trigger);

  const updateForm = (formData: CepSearchData): void => {
    onSubmit && onSubmit(formData.cep);
    finish();
  };

  return (
    <Flex direction="column" height="100%">
      <Flex
        marginBottom={[2, 2, 4]}
        direction="column"
        flex={1}
        overflowY="auto"
        marginRight={-3}
        paddingRight={3}
        maxHeight={['400px', '400px', 'unset']}
      >
        {isLoading && <Loader />}

        {!ceps.length && !isLoading && (
          <NoDataDisplay customPhrase="Nenhum CEP encontrado." />
        )}

        <Controller
          control={control}
          name="cep"
          defaultValue={undefined}
          rules={{ required: true }}
          render={({ field: { onChange, value } }) => (
            <RadioCardsGroup
              name="cep"
              onChange={(id) => onChange(ceps.find((c) => c.id === +id))}
              defaultValue={value?.id?.toString() || ''}
              minWidth="90%"
              fitMode="fit"
            >
              {ceps.map((cep) => (
                <CepSearchResultCard
                  key={cep.id}
                  value={cep.id.toString()}
                  cep={cep}
                />
              ))}
            </RadioCardsGroup>
          )}
        />
      </Flex>

      <Flex justifyContent="flex-end">
        {!isMobile && (
          <Button colorScheme="grey" onClick={previousStep} marginRight={6}>
            Voltar
          </Button>
        )}

        <Button
          onClick={handleSubmit(updateForm)}
          disabled={!isValid}
          width={['100%', '100%', 'auto']}
        >
          Selecionar e preencher
        </Button>
      </Flex>
    </Flex>
  );
};

export const CepSearchResults: FC<CepSearchResultsContentProps> = (props) => (
  <BemErrorBoundary>
    <CepSearchResultsContent {...props} />
  </BemErrorBoundary>
);
