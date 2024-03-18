import { useState, FC } from 'react';

import {
  Alert,
  AlertDescription,
  Center,
  CloseButton,
  Icon,
  useBreakpointValue,
  useToast,
} from '@chakra-ui/react';
import { useNetworkState, useUpdateEffect } from 'react-use';

import { InternetOffIcon, InternetOnIcon } from '@pcf/design-system-icons';

import { Toast } from '../toast';

export const TrackingUserConnection: FC = () => {
  const [hasLostConnection, setHasLostConnection] = useState(false);
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const { online } = useNetworkState();
  const toast = useToast();

  useUpdateEffect(() => {
    if (online && hasLostConnection) {
      toast.closeAll();
      if (!isMobile) {
        toast({
          position: 'bottom-right',
          render: ({ onClose }) => {
            return (
              <Toast
                icon={InternetOnIcon}
                title="Conexão à Internet reestabelecida"
                description="Parece que sua conexão à Internet já foi reestabelecida. Navegue tranquilamente."
                status="success"
                onClose={onClose}
              />
            );
          },
        });
      } else {
        toast({
          position: 'top',
          isClosable: true,
          render: ({ onClose }) => {
            return (
              <Alert
                alignItems="center"
                justifyContent="center"
                bg="success.regular"
                minH="32px"
                position="relative"
              >
                <Center
                  mr={2}
                  boxSize={6}
                  bg="success.dark"
                  borderRadius="full"
                >
                  <Icon as={InternetOnIcon} color="white" />
                </Center>

                <AlertDescription textStyle="bold14" color="white">
                  Conexão reestabelecida
                </AlertDescription>

                <CloseButton
                  color="white"
                  position="absolute"
                  right={2}
                  onClick={onClose}
                />
              </Alert>
            );
          },
        });
      }

      setHasLostConnection(false);
    }

    if (!online) {
      toast.closeAll();

      if (!isMobile) {
        toast({
          position: 'bottom-right',
          duration: null,
          render: ({ onClose }) => {
            return (
              <Toast
                icon={InternetOffIcon}
                title="Sem conexão de Internet"
                description="Parece que você está sem internet neste momento. Verifique sua conexão e tente novamente."
                status="error"
                onClose={onClose}
              />
            );
          },
        });
      } else {
        toast({
          position: 'top',
          duration: null,
          render: () => {
            return (
              <Alert
                alignItems="center"
                justifyContent="center"
                bg="error.regular"
                minH="32px"
              >
                <Center mr={2} boxSize={6} bg="error.dark" borderRadius="full">
                  <Icon as={InternetOffIcon} color="white" />
                </Center>
                <AlertDescription textStyle="bold14" color="white">
                  Sem conexão de Internet
                </AlertDescription>
              </Alert>
            );
          },
        });
      }

      setHasLostConnection(true);
    }
  }, [online]);

  return null;
};
