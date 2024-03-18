import { Box, Button, Flex, useUnmountEffect } from '@chakra-ui/react';
import { Login } from 'features/login';
import { useFeatureFlag } from 'app/feature-toggle';

import { Page } from 'features/components/page';
import { useMenuContext } from 'features/components/menu-bar';
import { Footer } from 'features/components';
import { BemErrorBoundary } from '@pcf/design-system';

import { headerLinks } from './header-links';

export const PublicTemplate: React.FC = ({ children }) => {
  const { onToggleMobileExtraContent, onClose } = useMenuContext();
  useUnmountEffect(onClose);

  const { ACESSAR_CONTA } = useFeatureFlag();

  return (
    <Page
      menuItems={headerLinks}
      showContextContent={false}
      inlineActions={
        ACESSAR_CONTA ? (
          <Box ml="74px">
            <Login isPopover />
          </Box>
        ) : null
      }
      menuMobileHeader={
        ACESSAR_CONTA ? (
          <Flex
            paddingTop="24px"
            paddingBottom="32px"
            w="100%"
            px={6}
            justifyContent="center"
            alignItems="center"
          >
            <Button
              h="72px"
              w="100%"
              boxShadow="0px 4px 24px rgba(24, 81, 158, 0.15)"
              colorScheme="grey"
              onClick={onToggleMobileExtraContent}
            >
              Acesse sua conta
            </Button>
          </Flex>
        ) : null
      }
      menuMobileAlternativeContent={
        <Login
          onPrevious={onToggleMobileExtraContent}
          onSuccess={() => {
            onToggleMobileExtraContent();
          }}
        />
      }
    >
      <BemErrorBoundary>{children}</BemErrorBoundary>

      <Footer />
    </Page>
  );
};
