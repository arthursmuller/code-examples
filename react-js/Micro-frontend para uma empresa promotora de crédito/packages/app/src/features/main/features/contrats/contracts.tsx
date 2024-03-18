import { FC } from 'react';

import { Center } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';

export const Contracts: FC = () => {
  return (
    <Center flex="1">
      <CustomHeading textStyle="bold40" color="secondary.regular">
        Contratos
      </CustomHeading>
    </Center>
  );
};
