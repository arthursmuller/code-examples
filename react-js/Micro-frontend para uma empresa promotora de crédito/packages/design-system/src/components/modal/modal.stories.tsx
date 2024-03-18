import { ReactElement } from 'react';

import { Story } from '@storybook/react';
import { Box, Button } from '@chakra-ui/react';

import { ModalProvider, useModal } from './modal-provider';
import { ModalConfig } from './modal-config.model';

import { ColorSchemes } from '../../bem-chakra-theme/foundations/colors';

export default {
  title: 'Dialogs/Modal',
  component: ModalProvider,
  decorators: [
    (StoryComp): ReactElement => (
      <ModalProvider>
        <StoryComp />
      </ModalProvider>
    ),
  ],
  argTypes: {
    type: {
      control: {
        type: 'select',
        options: ['success', 'error', 'alert'],
      },
    },
  },
};

const Template: Story<ModalConfig> = (props) => {
  const { showModal } = useModal();

  const onSubmit = (): void =>
    showModal({
      ...props,
    });

  return <Button onClick={onSubmit}>Open Dialog</Button>;
};

export const defaultDialog = Template.bind({});
defaultDialog.args = {
  title: 'Our default success modal',
  information: 'Your relevant information goes here',
  closeOnClickOverlay: false,
  onClose: () => {},
  closeText: 'Close',
};

export const overlayClose = Template.bind({});
overlayClose.args = {
  title: 'Our default success modal but overlay',
  information: 'You must click on the backdrop to close',
  closeOnClickOverlay: true,
};

export const errorDialog = Template.bind({});
errorDialog.args = {
  type: ColorSchemes.error,
  title: 'Our default error variant modal',
  information: 'Your relevant error information goes here',
  closeOnClickOverlay: true,
  onClose: () => {},
  closeText: 'Close',
};

export const alertDialog = Template.bind({});
alertDialog.args = {
  type: ColorSchemes.error,
  title: 'Our default alert/information variant modal',
  information: 'Your relevant alert/information information goes here',
  closeOnClickOverlay: true,
  onClose: () => {},
  closeText: 'Close',
};

export const customDialog = Template.bind({});
customDialog.args = {
  modal: () => (
    <Box backgroundColor="grey.100" borderRadius="full" padding="20px">
      This is a custom dialog. Anything can be added inside
    </Box>
  ),
};

export const confirmationDialog = Template.bind({});
confirmationDialog.args = {
  type: ColorSchemes.warning,
  title: 'Are you sure?',
  information: 'Irreversible operation!',
  closeOnClickOverlay: true,
  onClose: () => {},
  onConfirm: () => {},
  confirmText: 'Yes',
  closeText: 'No',
};
