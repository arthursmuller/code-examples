import { Box } from '@chakra-ui/react';
import {
  AutoComplete,
  AutoCompleteInput,
  AutoCompleteItem,
  AutoCompleteList,
  UseAutoCompleteProps,
  AutoCompleteInputProps,
} from '@choc-ui/chakra-autocomplete';
import React from 'react';

import { inputChakraStyles } from '../Input';

interface AutocompleteProps {
  children?: React.ReactNode;
  autoCompleteProps?: UseAutoCompleteProps;
  options: { value: string | number }[];
}

/**
 * @param {autoCompleteProps} autoCompleteProps
 * - Some proprieties:
 * -- autoCompleteProps.emphasize
 * -- autoCompleteProps.openOnFocus
 * -- autoCompleteProps.defaultValues
 * -- autoCompleteProps.emptyState
 * -- autoCompleteProps.multiple
 * -- autoCompleteProps.onChange
 * -- autoCompleteProps.onSelectOption
 */
const Autocomplete = ({
  autoCompleteProps,
  options,
  children,
}: AutocompleteProps) => {
  return (
    <AutoComplete
      openOnFocus
      emptyState={() => (<Box m="0 8px">Nenhuma opção encontrada</Box>)}
      {...autoCompleteProps}
    >
      {children}
      {/* <AutocompleteInput autoComplete="off" {...inputProps} /> */}
      <AutoCompleteList>
        {options.map((option) => (
          <AutoCompleteItem key={option.value} value={option.value || ''} />
        ))}
      </AutoCompleteList>
    </AutoComplete>
  );
};

const AutocompleteInput: React.FC<AutoCompleteInputProps> = ({
  children,
  ...inputProps
}: AutoCompleteInputProps) => {
  return (
    <AutoCompleteInput
      {...inputChakraStyles}
      autoComplete="off"
      {...inputProps}
    >
      {children}
    </AutoCompleteInput>
  );
};

export { Autocomplete, AutocompleteInput };	
