import { Story } from '@storybook/react';

import {
  SwipeableContainerProps,
  SwipeableContainer,
} from './swipeable-container';

import { ColorSchemes } from '../../bem-chakra-theme/foundations/colors';

export default {
  title: 'Swipeable container',
  component: SwipeableContainer,
};

interface StoryProps extends SwipeableContainerProps {
  sampleNum: number;
}

const Template: Story<StoryProps> = ({ sampleNum }) => {
  return (
    <>
      <h1> Resize your window to update display.</h1>
      <SwipeableContainer>
        {new Array(sampleNum).fill(undefined).map((_, index) => (
          <div
            style={{
              width: '200px',
              height: '200px',
              backgroundColor: index % 2 ? 'blue' : 'red',
            }}
            key={`${index}`}
          />
        ))}
      </SwipeableContainer>
    </>
  );
};

export const dynamicContainer = Template.bind({});
dynamicContainer.args = {
  schemeColor: ColorSchemes.secondary,
  sampleNum: 10,
};

export const fixedContainer = Template.bind({});
fixedContainer.args = {
  schemeColor: ColorSchemes.secondary,
  sampleNum: 10,
  fixedPerPage: 1,
};
