import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import {
  Drawer,
  StepsContainerWrapped,
  useStepsContainerContext,
  rightToLeft,
} from '@pcf/design-system';
import { ArrowLeftIcon, StatusCloseErrorIcon } from '@pcf/design-system-icons';

import { CepSearchForm } from './cep-search-form';
import { CepSearchResults } from './cep-search-results';
import { CepSearchDisplay } from './cep-search';

interface DrawerStepContentProps {
  onClick?: () => void;
  title: string;
}

const DrawerStepContent: FC<DrawerStepContentProps> = ({
  onClick,
  title,
  children,
}) => {
  const { previousStep } = useStepsContainerContext();

  return (
    <Flex
      direction="column"
      height="100%"
      animation={`250ms ${rightToLeft} ease-in-out`}
    >
      <Drawer.Title
        onClick={onClick || previousStep}
        icon={onClick ? StatusCloseErrorIcon : ArrowLeftIcon}
        title={title}
        color="secondary.mid-dark"
      />

      <Drawer.Body marginTop={4}>{children}</Drawer.Body>
    </Flex>
  );
};

export const CepSearchDesktop: FC<CepSearchDisplay> = ({
  buttonRender,
  data,
  onSubmit,
}) => (
  <Drawer
    buttonRender={buttonRender}
    content={({ onClose }) => (
      <StepsContainerWrapped size="md" showHeader={false} onClose={onClose}>
        <DrawerStepContent title="Consulta de CEP" onClick={onClose}>
          <CepSearchForm initialData={data} />
        </DrawerStepContent>
        <DrawerStepContent title="Selecione seu CEP">
          <CepSearchResults onSubmit={onSubmit} />
        </DrawerStepContent>
      </StepsContainerWrapped>
    )}
  />
);

export { CepSearchDesktop as default };
