import { FC } from 'react';

import { Box, Flex, Center } from '@chakra-ui/react';

import {
  CustomHeading,
  Loader,
  DateDisplay,
  DefaultFormatStrings,
} from '@pcf/design-system';
import { useClienteLogado } from '@pcf/core';

import { EditableAvatar } from '../editable-avatar';

export const AvatarContainer: FC = () => {
  const { data, isLoading } = useClienteLogado();

  return (
    <Flex
      flexDir={['row', 'row', 'column']}
      alignContent="center"
      alignItems="center"
    >
      {isLoading ? (
        <Center flex={1}>
          <Loader theme="white" />
        </Center>
      ) : (
        <>
          <EditableAvatar />

          <Box mt={[0, 0, '21px']} ml={['17px', '17px', 0]}>
            <CustomHeading
              as="h2"
              textStyle="bold24_32"
              color="white"
              textTransform="capitalize"
              textAlign={['start', 'start', 'center']}
            >
              Olá, {data?.nome}!
            </CustomHeading>

            <DateDisplay
              textStyle="regular16"
              color="white"
              textAlign={['initial', 'initial', 'center']}
              mt={[0, 0, '6px']}
              formatStr={DefaultFormatStrings.extended}
            />
          </Box>
        </>
      )}
    </Flex>
  );
};
