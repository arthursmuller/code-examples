import React, { FC, useState } from 'react';

import { Flex } from '@chakra-ui/react';

import {
  consultarProposta,
  SituacaoPropostaModel,
  extractReadableErrorMessage,
} from '@pcf/core';
import {
  formatDate,
  DefaultFormatStrings,
  useQuickToast,
} from '@pcf/design-system';
import { SubPageHeader } from 'features/landing/components/sub-page-header';
import { AdvantagesSection } from 'features/landing/components/advantages-section/advantages-section';

import {
  ConsultaPropostaFormData,
  ProposalSearchForm,
  ProposalSearchResultCard,
} from './components';
import ProposalSearchImg from './assets/consulte-sua-proposta@2x.jpg';

export const ProposalSearch: FC = () => {
  const toast = useQuickToast();
  const [isLoading, setIsLoading] = useState(false);
  const [situacaoProposta, setSituacaoProposta] =
    useState<SituacaoPropostaModel | null>(null);

  async function handleSubmit({
    dataNascimento,
    ...rest
  }: ConsultaPropostaFormData): Promise<void> {
    setIsLoading(true);

    try {
      const response = await consultarProposta({
        dataNascimento: formatDate(
          new Date(dataNascimento),
          DefaultFormatStrings.api,
        ),
        ...rest,
      });

      setSituacaoProposta(response);
    } catch (error) {
      const mensagem = extractReadableErrorMessage(error);

      toast('Houve um problema na requisição', mensagem);
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <>
      <SubPageHeader
        backgroundImage={ProposalSearchImg}
        backgroundImageAlt="Mulher de cabelos escuros sentada ao lado de um homem de cabelos brancos e óculos de grau, sorriem enquanto olham para um tablet"
        position={['67%', '67%', 'right top']}
      >
        <SubPageHeader.Title
          title="Consulte aqui a sua proposta"
          width={['270px', '270px', '570px']}
        />
        <SubPageHeader.Subtitle
          subtitleOrange=""
          subtitle="É Seguro! É Tranquilo!  É Bem Promotora!"
          width={['220px', '220px', '500px']}
        />
      </SubPageHeader>

      <Flex
        marginTop={['-100px', '-100px', '-20px', '-20px']}
        marginBottom="56px"
        marginX={[5, 5, '74px']}
        justifyContent="center"
      >
        {!situacaoProposta?.proposta && (
          <ProposalSearchForm isSubmiting={isLoading} onSubmit={handleSubmit} />
        )}

        {situacaoProposta?.proposta && (
          <ProposalSearchResultCard situacaoProposta={situacaoProposta} />
        )}
      </Flex>

      <AdvantagesSection />
    </>
  );
};
