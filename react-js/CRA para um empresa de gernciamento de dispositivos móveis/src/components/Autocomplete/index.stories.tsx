import { Autocomplete } from './index';

export default {
  title: 'Autocomplete',
  component: Autocomplete,
};

const optionsDefault = [
  { value: 'Brasil' },
  { value: 'Bruney' },
  { value: 'Argentina' },
  { value: 'Uruguai' },
  { value: 'Paraguai' },
];
const designProps = { h: '35px' };

const Template = (args) => <Autocomplete {...args} />;

export const Default = Template.bind({});
Default.args = {
  options: optionsDefault,
  inputProps: {
    ...designProps,
  }
};

export const withEmphasize = Template.bind({});
withEmphasize.args = {
  options: optionsDefault,
  autoCompleteProps: {
    emphasize: true,
  },
  inputProps: {
    ...designProps,
  }
};
