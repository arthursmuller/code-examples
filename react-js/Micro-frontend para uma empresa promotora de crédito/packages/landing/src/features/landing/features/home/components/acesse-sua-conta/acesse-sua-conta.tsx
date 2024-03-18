import { FC } from 'react';

import { Flex, Box, Button, useBreakpointValue } from '@chakra-ui/react';
import { useFeatureFlag } from 'app/feature-toggle';

import { useMenuContext } from 'features/components';
import { CustomHeading, LoginPopoverElId } from '@pcf/design-system';

export const AcesseSuaConta: FC = () => {
  const { onToggleMobileExtraContent, onOpen } = useMenuContext();

  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');
  const { ACESSAR_CONTA } = useFeatureFlag();

  const onClick = (): void => {
    if (isMobile) {
      onOpen();
      onToggleMobileExtraContent();
    } else {
      const el = document.getElementsByClassName(
        LoginPopoverElId,
      )[0] as HTMLElement;
      el?.click();
    }
  };

  return (
    <Flex
      as="section"
      height="fit-content"
      width="100%"
      paddingX="71px"
      paddingY={['32px', '32px', '24px']}
      alignItems="center"
      background="linear-gradient(90deg, #FF7A00 0%, #E15100 100%)"
      justifyContent="space-between"
      flexDirection={['column', 'column', 'row', 'row']}
      boxShadow="0px -4px 24px rgba(239, 100, 1, 0.15)"
    >
      <Flex
        alignItems="center"
        flexDirection={['column', 'column', 'row', 'row']}
      >
        <CustomHeading
          as="h3"
          textStyle="bold20"
          color="white"
          textAlign="center"
        >
          Bem Promotora,
        </CustomHeading>
        <CustomHeading
          as="h3"
          textStyle="bold20"
          color="white"
          textAlign="center"
          marginLeft={[0, 0, '6px']}
          marginBottom={['32px', '32px', 0]}
        >
          você sempre Bem!
        </CustomHeading>
      </Flex>

      {ACESSAR_CONTA && (
        <Box>
          <Button colorScheme="grey" onClick={onClick}>
            Acesse sua Conta
          </Button>
        </Box>
      )}
    </Flex>
  );
};
