import { Story } from '@storybook/react';

import { BemErrorFallback, BemErrorFallbackProps } from './bem-error-fallback';

export default {
  title: 'Error/Bem Error FallBack',
  component: BemErrorFallback,
};

const Template: Story<BemErrorFallbackProps> = (args) => {
  return <BemErrorFallback {...args} />;
};

export const Default = Template.bind({});
Default.args = {
  error: new Error('some error!'),
  resetErrorBoundary: () => {},
  isAlert: false,
};
