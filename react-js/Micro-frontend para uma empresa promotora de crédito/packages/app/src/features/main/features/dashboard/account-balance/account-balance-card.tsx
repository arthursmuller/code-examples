import { FC, useState } from 'react';

import { Box, Flex, IconButton, Text } from '@chakra-ui/react';

import {
  TabSaldoAtivaIcon,
  VisibilityHideIcon,
  VisibilityShowIcon,
} from '@pcf/design-system-icons';

import { DashboardCard } from '../components';

export const AccountBalanceCard: FC = () => {
  const [show, setShow] = useState<boolean>(false);

  return (
    <DashboardCard
      title="Saldo da conta"
      icon={TabSaldoAtivaIcon}
      body={
        <Flex direction="column">
          <Flex
            mt="16px"
            mb="14px"
            justifyContent="space-between"
            alignItems="center"
          >
            {show ? (
              <Text as="h2" textStyle="bold24_32" fontSize="32px">
                R$ 5.750,80
              </Text>
            ) : (
              <Box
                width="100%"
                height="40px"
                marginRight="20px"
                borderRadius="8px"
                backgroundColor="grey.200"
              />
            )}

            <IconButton
              variant="ghost"
              aria-label={show ? 'mostrar' : 'esconder'}
              color="grey.800"
              icon={
                !show ? (
                  <VisibilityShowIcon height="24px" width="24px" />
                ) : (
                  <VisibilityHideIcon height="24px" width="24px" />
                )
              }
              size="md"
              onClick={() => setShow(!show)}
            >
              {show ? 'Hide' : 'Show'}
            </IconButton>
          </Flex>

          {show && (
            <Text as="p" textStyle="bold12" color="primary.regular">
              Atualizado neste momento
            </Text>
          )}
        </Flex>
      }
    />
  );
};
