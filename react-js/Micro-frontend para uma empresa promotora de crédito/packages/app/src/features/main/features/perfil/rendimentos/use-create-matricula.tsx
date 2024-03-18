import { FC } from 'react';

import { Flex, Icon, Text, Center } from '@chakra-ui/react';

import {
  CustomHeading,
  StepsActionDialogComp,
  StepsContainerProvider,
  useModal,
  useStepsContainerContext,
} from '@pcf/design-system';
import { useRendimentosQuery } from '@pcf/core';
import { AttentionIcon } from '@pcf/design-system-icons';

import { MatriculaInssFormModel } from './models/matricula-inss-form.model';
import { MatriculaSiapeFormModel } from './models/matricula-siape-form.model';
import {
  BankFormStep,
  BankWithSpecieFormStep,
  InstitutingFormStep,
  RegisterInssFormStep,
  RegisterSiapeFormStep,
  TypeFormStep,
  ValueFormStep,
} from './components/steps';

export const CreateMatricula: FC = () => {
  const { data } = useStepsContainerContext<
    MatriculaInssFormModel | MatriculaSiapeFormModel
  >();

  const isSiape = data.tipoConvenio?.nome?.split(' ')[0] === 'SIAPE';
  const isOutros = data.tipoConvenio?.nome?.split(' ')[0] === 'OUTROS';
  const isINSS = data.tipoConvenio?.nome?.split(' ')[0] === 'INSS';

  return (
    <StepsActionDialogComp>
      <TypeFormStep />

      {!isOutros && isSiape && !isINSS && <RegisterSiapeFormStep />}
      {!isOutros && isINSS && !isSiape && <RegisterInssFormStep />}

      {!isOutros && isSiape && <BankFormStep />}
      {!isOutros && isINSS && <BankWithSpecieFormStep />}

      {!isOutros && isSiape && <InstitutingFormStep />}
      {!isOutros && <ValueFormStep />}

      {isOutros && (
        <Center flexDir="column" py={8}>
          <Flex
            justifyContent="center"
            alignItems="center"
            borderRadius="full"
            bg="warning.light"
            h="50px"
            w="50px"
          >
            <Icon w="29px" h="27px" as={AttentionIcon} />
          </Flex>

          <CustomHeading mt={4} color="primary.light" textStyle="bold20">
            Atenção
          </CustomHeading>

          <Text textAlign="center" mt={6}>
            Infelizmente não trabalhamos com outros convênios, mas em breve
            traremos novidades. Aguarde!
          </Text>
        </Center>
      )}
    </StepsActionDialogComp>
  );
};

export const useCreateMatricula = (): (() => void) => {
  const { hideModal, showModal } = useModal();
  const { refetch } = useRendimentosQuery(undefined, { enabled: false });

  return (): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <StepsContainerProvider
          onCloseCb={() => {
            hideModal();
            refetch();
          }}
        >
          <CreateMatricula />
        </StepsContainerProvider>
      ),
    });
  };
};
