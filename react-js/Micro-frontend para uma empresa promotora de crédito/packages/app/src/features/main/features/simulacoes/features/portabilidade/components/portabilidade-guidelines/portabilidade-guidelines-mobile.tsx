import { FC } from 'react';

import { useMount } from 'react-use';
import { Center, Flex, Image, Text } from '@chakra-ui/react';

import { useModal, ActionDialog, CustomHeading } from '@pcf/design-system';

import { PortabilidadeGuidelinesProps } from './portabilidade-guideline-props';
import Flow from './flow-with-badges.svg';

const steps = [
  'Você solicita crédito;',
  'A Instituição financeira original o concede;',
  'Você solicita a portabilidade;',
  'A Bem Promotora quita sua dívida;',
  'A Bem Promotora repassa para você os valores de portabilidade ou portabilidade e refinanaciamento.',
];

const PortabilidadeGuidelines: FC<PortabilidadeGuidelinesProps> = ({
  onClose,
}) => {
  const { showModal } = useModal();

  useMount(() =>
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <ActionDialog
          title="Ajuda"
          info={
            <Flex direction="column">
              <CustomHeading
                as="h3"
                textStyle="bold24"
                marginBottom="8px"
                color="secondary.regular"
                textAlign="center"
              >
                Como Funciona?
              </CustomHeading>

              <Image src={Flow} />

              {steps.map((step, i) => (
                <Flex key={`step-${i}`} marginBottom={2}>
                  <Center
                    minWidth={6}
                    height={6}
                    background="secondary.mid-dark"
                    borderRadius="full"
                    color="white"
                    marginRight={2}
                  >
                    {i + 1}
                  </Center>
                  <Text>{step}</Text>
                </Flex>
              ))}
            </Flex>
          }
          onConfirm={onClose}
          hasCancel={false}
          confirmLabel="Entendi!"
        />
      ),
    }),
  );

  return null;
};

export default PortabilidadeGuidelines;
