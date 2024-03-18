import { FC, lazy } from 'react';

import {
  Button,
  ButtonProps,
  ComponentWithAs,
  Icon,
  useBreakpointValue,
} from '@chakra-ui/react';

import { SearchIcon } from '@pcf/design-system-icons';
import { CepModel } from '@pcf/core';

import { CepSearchFormData } from './cep-search-form';

const DrawerDesktop = lazy(() => import('./cep-search-desktop'));
const ModalMobile = lazy(() => import('./cep-search-mobile'));

interface CepSearch {
  onSubmit?: (selection: CepModel) => void;
  data?: CepSearchFormData;
}

export interface CepSearchDisplay extends CepSearch {
  buttonRender: ComponentWithAs<'button', ButtonProps>;
}

export const CepSearch: FC<CepSearch> = ({ onSubmit, data }) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const Container = isMobile ? ModalMobile : DrawerDesktop;

  return (
    <Container
      onSubmit={onSubmit}
      data={data}
      buttonRender={(props) => (
        <Button
          mb="16px"
          leftIcon={
            <Icon as={SearchIcon} boxSize="14px" color="primary.regular" />
          }
          colorScheme="primary"
          size="sm"
          variant="link"
          {...props}
        >
          Não sabe o seu CEP? Clique aqui!
        </Button>
      )}
    />
  );
};
