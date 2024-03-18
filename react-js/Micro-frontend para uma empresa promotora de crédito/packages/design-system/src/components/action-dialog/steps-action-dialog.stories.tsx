import { FC, ReactElement } from 'react';

import { Story } from '@storybook/react';
import { Button, Flex } from '@chakra-ui/react';

import {
  StepsActionDialog,
  StepsActionDialogProps,
} from './steps-action-dialog';

import { ModalProvider, useModal } from '../modal';
import { useStepsContainerContext } from '../steps-container';

export default {
  title: 'Dialogs/Steps Action Dialog',
  component: StepsActionDialog,
  decorators: [
    (StoryComp: FC): ReactElement => (
      <ModalProvider>
        <StoryComp />
      </ModalProvider>
    ),
  ],
};

const Template: Story<StepsActionDialogProps> = ({ ...props }) => {
  const { showModal } = useModal();

  const onSubmit = (): void =>
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <StepsActionDialog {...props}>
          <StepsTemplate title="First step" action="nextStep" />
          <StepsTemplate title="Second step" action="nextStep" />
          <StepsTemplate title="Final step" action="finish" />
        </StepsActionDialog>
      ),
    });

  return <Button onClick={onSubmit}>Open Dialog</Button>;
};

const StepsTemplate: FC<{ title: string; action: 'finish' | 'nextStep' }> = ({
  title,
  action,
}) => {
  const stepContext = useStepsContainerContext();

  return (
    <Flex direction="column">
      {title}
      <Button onClick={() => stepContext[action]()}>{action}</Button>
    </Flex>
  );
};

export const steps = Template.bind({});
