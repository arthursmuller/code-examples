import { FC, useState } from 'react';

import { Checkbox, Flex, Button, Text } from '@chakra-ui/react';
import { useHistory } from 'react-router-dom';

import {
  PageLayout,
  useModal,
  getDefaultErrorModalConfig,
} from '@pcf/design-system';
import {
  extractReadableErrorMessage,
  useImportClientData,
  useNotificacoes,
} from '@pcf/core';
import { useSubRouteMenu } from 'features/main/components/use-sub-route-menu';
import { useNavigatePathUp } from 'hooks';

import { TermsAndConditions } from './components/terms-and-conditions';

const title = 'Importação de Dados Cadastrais';

export const ImportData: FC = () => {
  const [confirm, setConfirm] = useState<boolean>(false);
  const { mutate, isLoading } = useImportClientData();
  const { refetch } = useNotificacoes(undefined, { enabled: false });
  const { showModal } = useModal();
  const history = useHistory();
  const navigateUp = useNavigatePathUp();

  const submit = (): void => {
    mutate(null, {
      onSuccess: () => {
        refetch();

        showModal({
          title: 'Pronto! Seus dados foram importados',
          closeOnClickOverlay: false,
          closeText: 'Fechar',
          onClose: () => history.push('/'),
        });
      },
      onError(error) {
        showModal(
          getDefaultErrorModalConfig({
            information: extractReadableErrorMessage(error, {
              showFallbackMessage: true,
              fallbackMessage:
                'Não foi possível realizar a importação de dados. Por favor, tente novamente.',
            }),
          }),
        );
      },
    });
  };

  useSubRouteMenu(title);

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>{title}</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <TermsAndConditions />

        <Checkbox
          onChange={(next) => setConfirm(next.target.checked)}
          colorScheme="success"
          marginBottom="16px"
        >
          <Text as="p" textStyle="regular12" ml="16px">
            Ao clicar em concordo, você aceita nossa Política de Privacidade e
            os Termos de Uso.
          </Text>
        </Checkbox>

        <Flex justifyContent="flex-end" marginBottom={4}>
          <Button
            onClick={submit}
            colorScheme="success"
            disabled={!confirm}
            isLoading={isLoading}
            width={['100%', '100%', '50%']}
          >
            Concordo
          </Button>
        </Flex>
      </PageLayout.Content>
    </PageLayout>
  );
};
