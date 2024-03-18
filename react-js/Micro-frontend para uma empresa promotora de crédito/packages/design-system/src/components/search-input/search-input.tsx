import React from 'react';

import {
  Input,
  InputLeftElement,
  InputGroup,
  Icon,
  InputRightElement,
} from '@chakra-ui/react';

import { TabbarBuscaIcon } from '@pcf/design-system-icons';

import { useSearchInputContext } from './search-input.context';

// import { useKeyboardEvent } from '../../hooks/use-keyboard-event';
import { Loader } from '../loader';

interface SearchInputProps {
  onSearch: (value: string) => void;
  isLoading: boolean;
  value: string;
}

export const SearchInput: React.FC<SearchInputProps> = ({
  onSearch,
  isLoading,
  value,
}) => {
  const { searchInputRef, setIsSearchInputFocused } = useSearchInputContext();

  // useKeyboardEvent(
  //   'Escape',
  //   () => {
  //     searchInputRef.current.blur();
  //     setIsSearchInputFocused(false);
  //   },
  //   searchInputRef.current,
  // );

  return (
    <InputGroup flex="1" borderColor="primary.regular">
      <InputLeftElement
        boxSize="8"
        top="7px"
        left="4px"
        pointerEvents="none"
        color="grey.600"
      >
        <Icon as={TabbarBuscaIcon} boxSize="18px" color="grey.600" />
      </InputLeftElement>

      <Input
        role="search"
        autoComplete="off"
        onFocus={() => setIsSearchInputFocused(true)}
        onBlur={() => {
          setIsSearchInputFocused(false);
        }}
        ref={searchInputRef}
        _focus={{
          borderColor: 'primary.regular',
        }}
        border="1px"
        borderColor="grey.300"
        textStyle="regular12"
        borderRadius="lg"
        onChange={(e) => onSearch(e.target.value)}
        type="text"
        color="grey.600"
        value={value}
        placeholder="Digite aqui o que você procura"
      />

      {isLoading && (
        <InputRightElement
          top="7px"
          right="6px"
          boxSize="8"
          pointerEvents="none"
          color="grey.600"
        >
          <Loader />
        </InputRightElement>
      )}
    </InputGroup>
  );
};
