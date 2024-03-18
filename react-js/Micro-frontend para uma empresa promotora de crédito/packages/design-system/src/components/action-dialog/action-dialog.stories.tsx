import { FC, ReactElement } from 'react';

import { Story } from '@storybook/react';
import { Button } from '@chakra-ui/react';

import { ActionDialog, ActionDialogProps } from './action-dialog';

import { ModalProvider, useModal } from '../modal';

export default {
  title: 'Dialogs/Action Dialog',
  component: ActionDialog,
  decorators: [
    (StoryComp: FC): ReactElement => (
      <ModalProvider>
        <StoryComp />
      </ModalProvider>
    ),
  ],
};

const Template: Story<ActionDialogProps> = ({ ...props }) => {
  const { showModal } = useModal();

  const onSubmit = (): void =>
    showModal({
      closeOnClickOverlay: false,
      modal: <ActionDialog {...props} />,
    });

  return <Button onClick={onSubmit}>Open Dialog</Button>;
};

export const confirm = Template.bind({});
confirm.args = {
  title: 'Tell me',
  info: 'Is Chakra cool?',
  confirmLabel: 'Yes!',
  cancelLabel: 'Nah',
};

export const acknowledge = Template.bind({});
acknowledge.args = {
  hasCancel: false,
  title: 'Just FYI',
  info: 'Check at mobile and desktop screen sizes.',
  confirmLabel: 'Okay',
};
