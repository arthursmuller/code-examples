import { FC } from 'react';

import { Center } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';

export const Cartao: FC = () => {
  return (
    <Center flex="1">
      <CustomHeading textStyle="bold40" color="secondary.regular">
        Cartao
      </CustomHeading>
    </Center>
  );
};
