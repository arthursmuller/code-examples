import { Flex } from '@chakra-ui/react';

import {
  getRendimentosQueryConfig,
  RendimentoResponseModel,
  Resource,
} from '@pcf/core';
import {
  BemErrorBoundary,
  Loader,
  rightToLeft,
  useModal,
  ActionDialogContent,
  ActionDialogContainer,
  ActionDialogHeader,
} from '@pcf/design-system';

import { EditMatriculaContent } from './edit-matricula-content';

export const useEditMatricula = (): ((rendimentoId: number) => void) => {
  const { hideModal, showModal } = useModal();

  return (rendimentoId: number): void => {
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <ActionDialogContainer>
          <ActionDialogHeader title="Edição de matrícula" onClose={hideModal} />
          <ActionDialogContent>
            <Flex
              direction="column"
              animation={`250ms ${rightToLeft} ease-in-out`}
            >
              <BemErrorBoundary>
                <Resource<RendimentoResponseModel[]>
                  path={getRendimentosQueryConfig().queryKey ?? ''}
                  render={({ data: rendimentos }) => (
                    <EditMatriculaContent
                      onSuccess={() => {
                        console.log('nada');
                      }}
                      rendimentoId={rendimentoId}
                      rendimentos={rendimentos}
                    />
                  )}
                  loaderComponent={<Loader />}
                />
              </BemErrorBoundary>
            </Flex>
          </ActionDialogContent>
        </ActionDialogContainer>
      ),
    });
  };
};
