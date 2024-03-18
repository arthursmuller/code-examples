import { Story } from '@storybook/react';

import { ValidationText, ValidationTextProps } from './validation-text';

export default {
  title: 'Validation texts',
  component: ValidationText,
};

const Template: Story<ValidationTextProps> = (props) => {
  return <ValidationText {...props}>My component is valid</ValidationText>;
};

export const valid = Template.bind({});
valid.args = {
  hasError: false,
};

export const invalid = Template.bind({});
invalid.args = {
  hasError: true,
};
