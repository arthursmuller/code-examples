import { FC, ReactElement } from 'react';

import { Text, Button } from '@chakra-ui/react';

import { ActionDialogContainer } from './action-dialog-container';
import { ActionDialogContent } from './action-dialog-content';
import { ActionDialogHeader } from './action-dialog-header';

import { useModal } from '../modal';

export interface ActionDialogProps {
  title: string;
  info?: string | ReactElement;
  onCancel?: () => void;
  onConfirm?: () => void;
  cancelLabel?: string;
  confirmLabel?: string;
  hasCancel?: boolean;
}

export const ActionDialog: FC<ActionDialogProps> = ({
  title,
  info,
  onCancel,
  onConfirm,
  hasCancel = true,
  cancelLabel = 'não',
  confirmLabel = 'sim',
}) => {
  const { hideModal } = useModal();

  const handleCancel = (): void => {
    onCancel && onCancel();
    hideModal();
  };

  const handleConfirm = (): void => {
    onConfirm && onConfirm();
    hideModal();
  };

  return (
    <ActionDialogContainer>
      <ActionDialogHeader
        title={title}
        onClose={hasCancel ? handleCancel : handleConfirm}
      />
      <ActionDialogContent>
        {typeof info === 'string' ? (
          <Text
            as="p"
            textStyle="regular24"
            textAlign="center"
            marginBottom="16px"
          >
            {info}
          </Text>
        ) : (
          info
        )}

        <Button
          onClick={handleConfirm}
          colorScheme={hasCancel ? 'error' : 'primary'}
          marginTop="8px"
        >
          {confirmLabel}
        </Button>

        {hasCancel && (
          <Button onClick={handleCancel} colorScheme="grey" marginTop="8px">
            {cancelLabel}
          </Button>
        )}
      </ActionDialogContent>
    </ActionDialogContainer>
  );
};
