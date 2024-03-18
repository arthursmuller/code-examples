import { Story } from '@storybook/react';
import { useArgs } from '@storybook/client-api';
import { Button } from '@chakra-ui/react';

import { StepsContainer, StepsContainerProps } from './steps-container';
import { StepCard } from './step-card';

export default {
  title: 'Steps',
  component: StepsContainer,
};

interface StoryProps extends StepsContainerProps {
  pageSampleBackground: string;
}

const Template: Story<StoryProps> = ({ pageSampleBackground, ...props }) => {
  const [args, updateArgs] = useArgs();

  function setNext(nextStepNumber: number): void {
    updateArgs({ stepNumber: nextStepNumber, canForward: nextStepNumber < 2 });
  }

  const onForward = (): void => setNext(args.stepNumber + 1);
  const onBack = (): void | false =>
    args.stepNumber > 1 && setNext(args.stepNumber - 1);

  return (
    <div style={{ backgroundColor: pageSampleBackground }}>
      <StepsContainer
        {...props}
        {...(args as StepsContainerProps)}
        onBack={onBack}
        {...(args.onForward ? { onForward } : [])}
      >
        <StepCard
          title="First step"
          subTitle="Quick card"
          information="Describe your actions here"
        >
          {!args.onForward && <Button onClick={onForward}>Continue</Button>}
        </StepCard>
        <StepCard
          title="Second step"
          subTitle="Still Quick card"
          information="Describe your actions here"
        />
      </StepsContainer>
    </div>
  );
};

export const steps = Template.bind({});
steps.args = {
  onBack: () => null,
  stepNumber: 1,
};

export const stepsWithContainerAction = Template.bind({});
stepsWithContainerAction.args = {
  onBack: () => null,
  stepNumber: 1,
  onForward: () => {},
  canForward: true,
};

export const stepsWithTitle = Template.bind({});
stepsWithTitle.args = {
  onBack: () => null,
  stepNumber: 1,
  canForward: true,
  title: 'I got a title',
};

export const stepsWithSecondaryColor = Template.bind({});
stepsWithSecondaryColor.args = {
  onBack: () => null,
  stepNumber: 1,
  canForward: true,
  title: 'I got a title',
  colorScheme: 'secondary',
  pageSampleBackground: 'blue',
};
