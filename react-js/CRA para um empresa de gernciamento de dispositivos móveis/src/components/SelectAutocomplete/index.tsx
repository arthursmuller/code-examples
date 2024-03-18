import { Box } from '@chakra-ui/react';
import { Select } from 'chakra-react-select';
import React from 'react';
import { components as componentsChakra } from 'react-select';

import { inputChakraStyles } from '../Input';

interface SelectAutocompleteProps<T> {
  options: T[];
  value: T | T[];
  onChange?: (value: T | T[]) => void;
  onInputChange?: (value: string) => void;
  getOptionValue?: (option: T) => string;
  getOptionLabel?: (option: T) => string;
  placeholder?: React.ReactNode;
  isMulti?: boolean;
  disabled?: boolean;
  labelControl?: string;
}

const SelectAutocomplete: <
  T
>(
  props: SelectAutocompleteProps<T>
) => JSX.Element = ({
  value,
  options,
  onChange,
  onInputChange,
  getOptionValue,
  getOptionLabel,
  placeholder,
  isMulti,
  disabled,
  labelControl,
}) => {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const Control = ({ children, ...props }) => {
    const style = {
      fontSize: '14px',
      lineHeight: '1.36',
      color: '#282832',
      marginLeft: '20px',
    };
    const { labelControl } = props.selectProps;
    return (
      <componentsChakra.Control {...props}>
        {props.hasValue && labelControl && (
          <span style={style}>{labelControl}</span>
        )}
        {children}
      </componentsChakra.Control>
    );
  };

  return (
    <Select
      {...inputChakraStyles}
      options={options}
      isClearable
      isDisabled={disabled}
      components={{
        DropdownIndicator: null,
        NoOptionsMessage: () => <Box m="0 8px">Nenhuma opção encontrada</Box>,
        // TODO: lost style when Control is used
        // Control,
      }}
      onInputChange={onInputChange}
      onChange={onChange}
      // onSelectResetsInput={false}
      // closeMenuOnSelect={false}
      getOptionValue={(option) =>
        (getOptionValue && getOptionValue(option)) || `${option['id']}`
      }
      getOptionLabel={(option) =>
        (getOptionLabel && getOptionLabel(option)) || `${option['name']}`
      }
      isMulti={isMulti}
      placeholder={placeholder}
      labelControl={labelControl}
      value={value}
    />
  );
};

export default SelectAutocomplete;
