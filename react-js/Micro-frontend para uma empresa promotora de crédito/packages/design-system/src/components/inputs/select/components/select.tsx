import { useRef, useState, forwardRef } from 'react';

import { Flex } from '@chakra-ui/react';
import SelectSearch from 'react-select-search/dist/cjs';
import fuzzySearch from 'react-select-search/dist/cjs/fuzzySearch';

import { FormElRef } from '../../form-item';
import { BemSelectProps } from '../select.model';
import { useCalculateOffset } from './use-calculate-offset';
import { SelectActionIcons } from './select-action-icons';
import { selectSearchStyles } from './select-search-styles';
import { useSelectInternalState } from './use-select-internal-state';

export const Select = forwardRef<FormElRef, BemSelectProps>(
  (
    {
      options,
      defaultValue,
      onBlur,
      onChange,
      searchQuery,
      multiple,
      disabled,
      isLoading,
      retry,
      hasError,
      value: externalValue,
    },
    ref,
  ) => {
    const [value, textValue, handleChange] = useSelectInternalState(
      defaultValue,
      externalValue,
      onBlur,
      onChange,
    );

    const [currentSearch, setCurrentSearch] = useState<string>();
    const [isMenuOpen, setOpen] = useState<boolean>();
    const containerRef = useRef<HTMLDivElement>(null);
    const offsets = useCalculateOffset(containerRef, isMenuOpen);

    return (
      <Flex
        onKeyPress={(e) => e.key === 'Enter' && e.preventDefault()}
        width="100%"
        title={textValue}
        ref={containerRef}
        onFocus={() => setOpen(true)}
        onBlur={() => setOpen(false)}
        sx={selectSearchStyles(!!value, hasError, !!options.length, offsets)}
      >
        <SelectSearch
          search
          filterOptions={fuzzySearch}
          options={options}
          value={value}
          multiple={multiple}
          disabled={disabled}
          onChange={handleChange}
          getOptions={
            !searchQuery
              ? undefined
              : (query) => {
                  setCurrentSearch(query);
                  return searchQuery(query);
                }
          }
          placeholder={currentSearch}
          ref={ref}
          key={options.length}
        />

        <SelectActionIcons
          hasError={hasError}
          isLoading={isLoading}
          hasValue={!!value}
          disabled={disabled}
          isMenuOpen={isMenuOpen}
          hasSearch={!!searchQuery}
          onClear={() => handleChange('')}
          onRetry={() => retry && retry()}
        />
      </Flex>
    );
  },
);
