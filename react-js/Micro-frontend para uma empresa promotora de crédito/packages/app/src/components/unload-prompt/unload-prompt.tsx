import { FC, useCallback, useEffect } from 'react';

import { Prompt } from 'react-router-dom';
import { useUnmount } from 'react-use';

import { useModal, ColorSchemes } from '@pcf/design-system';

interface UnloadPrompt {
  shouldBlock?: boolean;
  message?: string;
}

export const UnloadPrompt: FC<UnloadPrompt> = ({
  shouldBlock,
  message = 'Ao sair desta página você irá perder suas alterações cadastrais',
}) => {
  useEffect(() => {
    window.onbeforeunload = shouldBlock ? () => true : null;
  }, [shouldBlock]);

  useUnmount(() => {
    window.onbeforeunload = null;
  });

  return <Prompt when={shouldBlock} message={message || ''} />;
};

export const useUnloadPromptDialog = (): ((
  message: string,
  cb: (go: boolean) => void,
) => void) => {
  const { showModal } = useModal();

  const confirmNavigate = useCallback(
    (message: string, cb: (go: boolean) => void) => {
      showModal({
        closeOnClickOverlay: false,

        type: ColorSchemes.warning,
        title: 'Atenção',
        information: message,
        onCancel: () => cb(false),
        onConfirm: () => cb(true),
        confirmText: 'Sair',
        closeText: 'Continuar Editando',
      });
    },
    [showModal],
  );

  return confirmNavigate;
};
