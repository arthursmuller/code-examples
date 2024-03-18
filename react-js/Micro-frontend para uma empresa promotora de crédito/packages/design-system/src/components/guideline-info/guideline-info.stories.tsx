import { Grid } from '@chakra-ui/react';
import { Story } from '@storybook/react';

import { PasswordIcon, SimulacaoIcon } from '@pcf/design-system-icons';

import { GuidelineInfo, GuidelineInfoProps } from './guideline-info';

export default {
  title: 'Guideline Info',
  component: GuidelineInfo,
};

const Template: Story<GuidelineInfoProps> = (props) => {
  return (
    <Grid padding={10} gridTemplateColumns="1fr 1fr" gap={10} width="400px">
      <GuidelineInfo {...props} icon={PasswordIcon}>
        My content is self explanatory here
      </GuidelineInfo>
      <GuidelineInfo {...props} icon={SimulacaoIcon}>
        As much as this one
      </GuidelineInfo>
    </Grid>
  );
};

export const defaultInfo = Template.bind({});
