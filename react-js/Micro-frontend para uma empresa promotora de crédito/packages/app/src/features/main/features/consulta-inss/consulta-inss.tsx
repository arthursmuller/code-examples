import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import { useHistory } from 'react-router-dom';

import { StepsContainerWrapped } from '@pcf/design-system';
import { useSubRouteMenu } from 'features/main/components/use-sub-route-menu';

import { CadadastroRendimentoCard, IntroStepCard } from './components';

const title = 'Consulta de Benefício Meu INSS ';

export const ConsultaInss: FC = () => {
  useSubRouteMenu(title);
  const history = useHistory();

  return (
    <Flex
      h="100%"
      flex="1"
      backgroundColor="secondary.regular"
      pt={['0', '0', '24px', '24px']}
    >
      <StepsContainerWrapped
        title={title}
        size="lg"
        containerHeight="full"
        colorScheme="white"
        onClose={history.goBack}
      >
        <IntroStepCard />
        <CadadastroRendimentoCard />
      </StepsContainerWrapped>
    </Flex>
  );
};
