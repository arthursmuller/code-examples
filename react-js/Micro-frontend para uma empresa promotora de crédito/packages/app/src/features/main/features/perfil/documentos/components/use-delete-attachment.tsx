import { useQueryClient } from 'react-query';

import { useModal, ActionDialog } from '@pcf/design-system';
import { ANEXOS_QUERY_ENDPOINT, useExcluirAnexo } from '@pcf/core';

export const useDeleteAttachment = (id: string | number): (() => void) => {
  const { showModal } = useModal();
  const { mutate: post } = useExcluirAnexo({ id });
  const queryCache = useQueryClient();

  const deleteAttachment = (): void => {
    const onConfirm = (): void => {
      post(undefined, {
        onSuccess() {
          queryCache.invalidateQueries(ANEXOS_QUERY_ENDPOINT);
        },
      });
    };

    showModal({
      closeOnClickOverlay: false,
      modal: (
        <ActionDialog
          title="Excluir anexo"
          onConfirm={onConfirm}
          cancelLabel="NÃO"
          confirmLabel="SIM, quero excluir"
          info="Esta ação é irreversível. Uma vez que você excluir o anexo, não será possível recuperá-lo."
        />
      ),
    });
  };

  return deleteAttachment;
};
