import { FC, useLayoutEffect } from 'react';

import { Box, Flex } from '@chakra-ui/react';

import {
  SwipeableContainer,
  BemErrorBoundary,
  BemErrorFallback,
} from '@pcf/design-system';
import { ContractsCard } from 'features/main/features/dashboard/contracts';
import { ProposalsCard } from 'features/main/features/dashboard/proposals';
import { AccountBalanceCard } from 'features/main/features/dashboard/account-balance';
import { AvatarContainer, QuickyButtons } from 'features/main/components';
import { usePageContext } from 'components/page/page.context';

export const DashboardMobile: FC = () => {
  const { setMenuBarConfig } = usePageContext();
  useLayoutEffect(
    () =>
      setMenuBarConfig &&
      setMenuBarConfig({
        menuBarColor: 'primary.gradient',
        contextMenuBarColor: 'primary.gradient',
        hasContextMenu: false,
      }),
    [],
  );

  return (
    <Flex flexDir="column" h="100%">
      <Box overflowY="auto" flex={1}>
        <Box bg="primary.gradient" px="24px" minH="220px">
          <Box>
            <BemErrorBoundary
              fallbackRender={(props) => (
                <BemErrorFallback {...props} schemeColor="white" />
              )}
            >
              <AvatarContainer />
            </BemErrorBoundary>
          </Box>

          <Box paddingBottom="24px" width="100%">
            <SwipeableContainer arrowPos="bottom" fixedPerPage={1}>
              <AccountBalanceCard />
              <ProposalsCard />
              <ContractsCard />
            </SwipeableContainer>
          </Box>
        </Box>

        <QuickyButtons py="32px" />
      </Box>
    </Flex>
  );
};

export default DashboardMobile;
