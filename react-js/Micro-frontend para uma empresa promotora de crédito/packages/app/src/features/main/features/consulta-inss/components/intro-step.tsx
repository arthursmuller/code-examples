import { FC, useState } from 'react';

import {
  Flex,
  Text,
  Grid,
  Button,
  Checkbox,
  useBreakpointValue,
} from '@chakra-ui/react';

import {
  GuidelineInfo,
  FullLayoutCard,
  useStepsContainerContext,
} from '@pcf/design-system';
import { InvoiceIcon, PropsPortabsAceiteIcon } from '@pcf/design-system-icons';

export const IntroStep: FC = () => {
  const { nextStep } = useStepsContainerContext();
  const [acceptedTerms, setAcceptedTerms] = useState(false);
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const onConfirm = (): void => {
    nextStep();
  };

  return (
    <Flex direction="column" alignItems="center">
      {!isMobile && (
        <Text textStyle="regular16" my={8} textAlign="center">
          Para melhorarmos a gestão de seus rendimentos e andamento da proposta,
          necessitamos solicitar acesso aos dados ao &quot;Meu INSS&quot;. Veja
          abaixo as facilidades:
        </Text>
      )}

      <Grid
        gridTemplateColumns={`repeat(${isMobile ? 1 : 2}, minmax(auto, 160px))`}
        gap={isMobile ? 16 : 6}
        my={isMobile ? 8 : 6}
      >
        <GuidelineInfo
          information="Rapidez na simulação das informações"
          icon={() => <InvoiceIcon height="40px" width="40px" />}
        />
        <GuidelineInfo
          information="Avaliação rápida do processo"
          icon={() => <PropsPortabsAceiteIcon height="40px" width="40px" />}
        />
      </Grid>

      <Checkbox
        alignItems="top"
        onChange={(next) => setAcceptedTerms(next.target.checked)}
        colorScheme="primary"
        my="42px"
      >
        <Text
          as="p"
          textStyle={acceptedTerms ? 'bold16' : 'regular16'}
          color={acceptedTerms ? 'primary.regular' : 'grey.800'}
          ml={2}
        >
          Eu autorizo o INSS/DATAPREV a disponibilizar suas informações para
          apoiar a contratação/simulação de Empréstimo Consignado/Cartão
          Consignado de benefícios do INSS para subsidiar a proposta pelo
          Banrisul.
        </Text>
      </Checkbox>

      <Flex justifyContent="flex-end" width="100%">
        <Button
          marginY={6}
          minWidth={['100%', '100%', '250px']}
          colorScheme="secondary"
          onClick={onConfirm}
          disabled={!acceptedTerms}
        >
          Continuar
        </Button>
      </Flex>
    </Flex>
  );
};

export const IntroStepCard: FC = () => (
  <FullLayoutCard>
    <IntroStep />
  </FullLayoutCard>
);
