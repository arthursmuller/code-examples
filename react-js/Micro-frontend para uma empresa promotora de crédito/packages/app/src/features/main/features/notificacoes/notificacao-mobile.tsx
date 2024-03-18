import { FC } from 'react';

import { Center } from '@chakra-ui/react';

import { ArrowLeftIcon } from '@pcf/design-system-icons';
import { useNotificacoes } from '@pcf/core';
import { Drawer, Loader, DrawerProps } from '@pcf/design-system';

import { NotificationLine, NotificationNoData } from './components';

export const NotificacoesMobile: FC<DrawerProps> = ({
  buttonRender: ButtonRender,
}) => {
  const { data, isLoading, isFetching } = useNotificacoes(undefined, {
    refetchInterval: 1000 * 60 * 5,
  });

  return (
    <Drawer
      buttonRender={({ onClick }) => (
        <ButtonRender onClick={onClick} notifications={data?.length ?? 0} />
      )}
      content={({ onClose }) => (
        <>
          <Drawer.Title
            onClick={onClose}
            icon={ArrowLeftIcon}
            iconProps={{
              marginRight: '2px',
              width: '10.67px',
              height: '16px',
              fill: 'white',
            }}
            title="Minhas notificações"
            color="secondary.mid-dark"
          />

          <Drawer.Body marginTop={4} flexDirection="column" display="flex">
            {(isLoading || isFetching) && (
              <Center padding={4} flex={1}>
                <Loader />
              </Center>
            )}

            {!isLoading && !isFetching && !data?.length && (
              <NotificationNoData marginTop="40%" />
            )}

            {!isLoading &&
              !isFetching &&
              data?.map((notification, index) => (
                <NotificationLine
                  {...notification}
                  onClick={onClose}
                  key={notification.id}
                  showDivider={!!index}
                />
              ))}
          </Drawer.Body>
        </>
      )}
    />
  );
};
