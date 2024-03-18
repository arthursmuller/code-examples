import { FC } from 'react';

import { Button, Grid } from '@chakra-ui/react';

import { useStepsContainerContext } from '@pcf/design-system';

import { PortabilidadeTypeOption } from './portabilidade-type-option';
import { portabilidadeOptDescriptions } from './opts';

import {
  PortabilidadeType,
  PortabilidadeTypeFormData,
} from '../../models/portabilidade-form.model';

const SimulationValueStepDesktop: FC = () => {
  const { nextStep } = useStepsContainerContext<PortabilidadeTypeFormData>();

  return (
    <Grid gap={8} gridTemplateColumns="1fr 1fr" marginTop={5}>
      <PortabilidadeTypeOption
        title="Desejo apenas portar meu contrato"
        descriptions={portabilidadeOptDescriptions.portOnly}
      >
        <Button
          onClick={() => nextStep({ type: PortabilidadeType.Portar })}
          colorScheme="secondary"
        >
          Portar meu documento
        </Button>
      </PortabilidadeTypeOption>
      <PortabilidadeTypeOption
        title="Desejo portar e refinanciar meu contrato "
        descriptions={portabilidadeOptDescriptions.withRefin}
      >
        <Button
          onClick={() => nextStep({ type: PortabilidadeType.Simular })}
          colorScheme="secondary"
          whiteSpace="unset"
        >
          Simular Portabilidade e Refinanciamento
        </Button>
      </PortabilidadeTypeOption>
    </Grid>
  );
};

export default SimulationValueStepDesktop;
