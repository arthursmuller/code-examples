import { FC, useState } from 'react';

import {
  Flex,
  Text,
  Grid,
  Divider,
  Button,
  RadioGroup,
  Radio,
} from '@chakra-ui/react';

import {
  FullLayoutCard,
  useStepsContainerContext,
  GuidelineInfo,
} from '@pcf/design-system';
import { InvoiceIcon, PropsPortabsAceiteIcon } from '@pcf/design-system-icons';

import { RequestMarginSiape } from '../model';

enum AllowSiapeConsult {
  yes = 'yes',
  no = 'no',
}

export const SiapeMarginPendingStep: FC = () => {
  const [radioConfirm, setRadionConfirm] = useState<AllowSiapeConsult>();

  const { nextStep } = useStepsContainerContext<RequestMarginSiape>();

  const onConfirm = (): void => {
    nextStep({ allowSiapeConsult: radioConfirm === AllowSiapeConsult.yes });
  };

  return (
    <Flex direction="column" alignItems="center">
      <Text marginBottom={9} marginTop={5} textAlign="center">
        Identificamos que seus dados de margem não foram liberados
      </Text>
      <Grid gridTemplateColumns="repeat(2, minmax(auto, 160px))" gap={6}>
        <GuidelineInfo
          information="Rapidez na simulação das informações"
          icon={() => <InvoiceIcon height="40px" width="40px" />}
        />
        <GuidelineInfo
          information="Avaliação rapida do processo"
          icon={() => <PropsPortabsAceiteIcon height="40px" width="40px" />}
        />
      </Grid>

      <Flex paddingX={8} width="100%">
        <Divider mt={6} mb={8} borderColor="grey.400" />
      </Flex>

      <RadioGroup
        onChange={(val) => setRadionConfirm(val as AllowSiapeConsult)}
        value={radioConfirm}
        name="confirm"
      >
        <Grid
          gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
          gap={6}
          marginX={8}
        >
          <Radio value={AllowSiapeConsult.yes}>
            Liberar meus dados de margem SIAPE
          </Radio>
          <Radio value={AllowSiapeConsult.no}>
            Não desejo liberar meus dados de margem SIAPE
          </Radio>
        </Grid>
      </RadioGroup>
      <Flex justifyContent="flex-end" width="100%">
        <Button
          marginY={6}
          minWidth={['100%', '100%', '250px']}
          colorScheme="secondary"
          disabled={!radioConfirm}
          onClick={onConfirm}
        >
          Continuar
        </Button>
      </Flex>
    </Flex>
  );
};

export const SiapeMarginPendingStepCard: FC = () => (
  <FullLayoutCard>
    <SiapeMarginPendingStep />
  </FullLayoutCard>
);
