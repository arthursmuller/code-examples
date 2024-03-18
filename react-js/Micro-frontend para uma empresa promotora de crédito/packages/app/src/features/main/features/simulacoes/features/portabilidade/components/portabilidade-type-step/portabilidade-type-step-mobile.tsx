import { FC } from 'react';

import {
  Button,
  Text,
  Tabs,
  Tab,
  TabList,
  Flex,
  TabPanels,
  TabPanel,
  Center,
  Icon,
} from '@chakra-ui/react';

import { useStepsContainerContext } from '@pcf/design-system';
import { HomeNovapropostaIcon, HomeRefinIcon } from '@pcf/design-system-icons';

import { PortabilidadeTypeOption } from './portabilidade-type-option';
import { portabilidadeOptDescriptions } from './opts';

import {
  PortabilidadeType,
  PortabilidadeTypeFormData,
} from '../../models/portabilidade-form.model';

const TabOpt: FC<{ icon: FC; title: string }> = ({ icon, title }) => (
  <Tab
    _selected={{ fontWeight: 'bold', borderColor: 'secondary.regular' }}
    width="100%"
  >
    <Flex direction="column" justifyContent="center" alignItems="center">
      <Center
        borderRadius="full"
        padding={2}
        backgroundColor="white"
        width="fit-content"
        boxShadow="medium"
      >
        <Icon as={icon} height="24px" width="24px" color="secondary.regular" />
      </Center>
      <Text color="secondary.regular">{title}</Text>
    </Flex>
  </Tab>
);

const SimulationValueStepMobile: FC = () => {
  const { nextStep } = useStepsContainerContext<PortabilidadeTypeFormData>();

  return (
    <>
      <Center>
        <Text textStyle="bold16" color="secondary.regular">
          Modalidades de Portabilidade
        </Text>
      </Center>

      <Tabs align="center" variant="line" defaultIndex={2} height="100%">
        <TabList borderBottom="none">
          <TabOpt
            title="Portabilidade e Refinanciamento"
            icon={HomeRefinIcon}
          />
          <TabOpt title="Apenas Portabilidade" icon={HomeNovapropostaIcon} />
        </TabList>

        <TabPanels height="100%">
          <TabPanel height="100%" background="white">
            <PortabilidadeTypeOption
              title="Desejo apenas portar meu contrato"
              descriptions={portabilidadeOptDescriptions.portOnly}
            >
              <Button
                onClick={() => nextStep({ type: PortabilidadeType.Simular })}
                colorScheme="secondary"
              >
                Portar meu documento
              </Button>
            </PortabilidadeTypeOption>
          </TabPanel>
          <TabPanel height="100%" background="white">
            <PortabilidadeTypeOption
              title="Desejo portar e refinanciar meu contrato "
              descriptions={portabilidadeOptDescriptions.withRefin}
            >
              <Button
                onClick={() => nextStep({ type: PortabilidadeType.Portar })}
                colorScheme="secondary"
              >
                Portar meu contrato
              </Button>
            </PortabilidadeTypeOption>
          </TabPanel>
        </TabPanels>
      </Tabs>
    </>
  );
};

export default SimulationValueStepMobile;
