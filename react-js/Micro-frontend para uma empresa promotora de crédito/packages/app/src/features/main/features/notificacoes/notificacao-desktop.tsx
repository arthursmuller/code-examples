import { FC } from 'react';

import {
  Flex,
  Popover,
  PopoverArrow,
  PopoverContent,
  PopoverTrigger,
  Divider,
  Text,
  Center,
  Grid,
} from '@chakra-ui/react';

import { NotificationAtivoIcon } from '@pcf/design-system-icons';
import {
  zIndexes,
  CustomHeading,
  Loader,
  ActionDialogCloseButton,
  BemErrorFallback,
} from '@pcf/design-system';
import { useNotificacoes } from '@pcf/core';
import { NavBarButton } from 'features/main/components';

import { NotificationLine, NotificationNoData } from './components';

export const NotificacoesDesktop: FC = () => {
  const { data, isLoading, isFetching, isError, error, refetch } =
    useNotificacoes(undefined, {
      refetchInterval: 1000 * 60 * 5,
      useErrorBoundary: false,
    });

  return (
    <Popover>
      {({ isOpen, onClose }) => (
        <>
          <PopoverTrigger>
            <NavBarButton
              icon={NotificationAtivoIcon}
              label={data?.length ?? ''}
              active={isOpen}
            />
          </PopoverTrigger>
          <PopoverContent
            border="none"
            zIndex={zIndexes.absoluteElements}
            marginTop="20px"
            boxShadow="card"
            width="100%"
            maxWidth="1000px"
            _focus={{
              boxShadow: 'card',
            }}
          >
            <PopoverArrow />
            {isOpen && (
              <Flex direction="column" padding={8} maxWidth="99vw">
                <Flex marginBottom={8}>
                  <CustomHeading
                    color="secondary.mid-dark"
                    textStyle="bold32"
                    flex={1}
                  >
                    Minhas notificações
                  </CustomHeading>

                  <ActionDialogCloseButton onClose={onClose} />
                </Flex>

                <Grid gridTemplateColumns="minmax(auto, 300px) 1fr">
                  <Text
                    minWidth="300px"
                    textStyle="bold14"
                    color="secondary.mid-dark"
                  >
                    Pendências
                  </Text>
                  <Text textStyle="bold14" color="secondary.mid-dark">
                    Detalhes das pendências
                  </Text>

                  <Divider
                    borderColor="secondary.mid-dark"
                    marginTop={2}
                    marginBottom={8}
                    gridColumnEnd="span 2"
                  />

                  {isError ? (
                    <BemErrorFallback
                      resetErrorBoundary={refetch}
                      error={error}
                    />
                  ) : (
                    <>
                      {(isLoading || isFetching) && (
                        <Center padding={4} gridColumnEnd="span 2">
                          <Loader />
                        </Center>
                      )}

                      {!isLoading && !isFetching && !data?.length && (
                        <NotificationNoData gridColumnEnd="span 2" />
                      )}

                      {!isLoading &&
                        !isFetching &&
                        data?.map((notification, index) => (
                          <NotificationLine
                            {...notification}
                            onClick={onClose}
                            key={`item-${notification.id}`}
                            showDivider={!!index}
                          />
                        ))}
                    </>
                  )}
                </Grid>
              </Flex>
            )}
          </PopoverContent>
        </>
      )}
    </Popover>
  );
};
