import { Box } from '@chakra-ui/react';

import SelectAutocomplete from './index';

export default {
  title: 'SelectAutocomplete',
  component: SelectAutocomplete,
};

const optionsDefault = [
  { id: 11, name: 'Brasil', code: 'BR' },
  { id: 12, name: 'Bruney', code: 'BN' },
  { id: 13, name: 'Argentina', code: 'AR' },
  { id: 14, name: 'Uruguai', code: 'UR' },
  { id: 15, name: 'Paraguai', code: 'PA' },
];
const designProps = { h: '35px', backgroundColor: 'gray.100' };

const onChange = (value) => {
  // console.log('onChange', value);
};
const onInputChange = (value) => {
  // console.log('onInputChange', value);
};

const Template = (args) => (
  <Box m="10px" backgroundColor="gray.600">
    <SelectAutocomplete {...args} />
  </Box>
);

export const Default = Template.bind({});
Default.args = {
  ...designProps,
  options: optionsDefault,
  placeholder: 'Selecione um país',
  onChange,
  onInputChange,
};


export const withMulti = Template.bind({});
withMulti.args = {
  ...designProps,
  options: optionsDefault,
  defaultValue: [optionsDefault[1]],
  isMulti: true,
  onChange,
  onInputChange,
};

export const withControl = Template.bind({});
withControl.args = {
  ...designProps,
  options: optionsDefault,
  defaultValue: [optionsDefault[1]],
  isMulti: true,
  labelControl: 'Países',
  onChange,
  onInputChange,
};

export const customLabel = Template.bind({});
customLabel.args = {
  ...designProps,
  options: optionsDefault,
  defaultValue: optionsDefault[1],
  getOptionLabel: (option) => (`${option.name} (code: ${option.code})`),
  onChange,
  onInputChange,
};
