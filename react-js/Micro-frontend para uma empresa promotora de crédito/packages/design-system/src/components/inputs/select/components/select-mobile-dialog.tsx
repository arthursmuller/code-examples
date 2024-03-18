import { FC, useMemo, useState } from 'react';

import { Button, Flex, Icon } from '@chakra-ui/react';
import AutoSizer from 'react-virtualized-auto-sizer';
import fuzzySearch from 'react-select-search/dist/cjs/fuzzySearch';
import { Virtuoso } from 'react-virtuoso';

import { TabbarBuscaIcon } from '@pcf/design-system-icons';

import { FormItem } from '../../form-item';
import { zIndexes } from '../../../../consts';
import { ActionDialogHeader } from '../../../action-dialog';
import { fadeIn } from '../../../../animations/fade-in';
import { BemTextInput } from '../../text-input';
import { useDebounce } from '../../../../hooks';

export const SelectMobileDialog: FC<{
  value: any;
  options: any[];
  onClose: () => void;
  label: string;
  optionProps: any;
}> = ({ value, options, onClose, label, optionProps }) => {
  const [searchValue, setSearchValue] = useState<string>('');
  const debouncedSearch = useDebounce(searchValue, 300).toLowerCase();

  const filteredOptions = useMemo(
    () =>
      fuzzySearch(options)(debouncedSearch).map((option) => {
        const isSelected = option.value === (value as any);
        const display = option.name.replace(
          new RegExp(debouncedSearch, 'gi'),
          (match) => `<span style="background-color: yellow">${match}</span>`,
        );

        return { ...option, name: display, isSelected };
      }),
    [debouncedSearch],
  );

  return (
    <Flex
      direction="column"
      layerStyle="card"
      width="100%"
      height="100%"
      zIndex={zIndexes.absoluteElements}
      padding={0}
      animation={`250ms ${fadeIn} ease-in-out`}
      overflow="hidden"
      flex={1}
    >
      <Flex backgroundColor="secondary.regular" paddingTop={4}>
        <ActionDialogHeader onClose={onClose} title={label} />
      </Flex>

      <Flex padding={4}>
        <FormItem label="Pesquisar" icon={<Icon as={TabbarBuscaIcon} />}>
          <BemTextInput
            defaultValue={searchValue}
            onChange={({ target: { value } }) => setSearchValue(value)}
          />
        </FormItem>
      </Flex>
      <Flex direction="column" overflowY="auto" flex={1}>
        <AutoSizer>
          {({ height, width }) => (
            <Virtuoso
              style={{ height, width }}
              totalCount={filteredOptions.length}
              itemContent={(index) => (
                <Button
                  variant="ghost"
                  colorScheme="grey"
                  {...(optionProps as any)}
                  value={filteredOptions[index].value}
                  backgroundColor={
                    filteredOptions[index].isSelected ? 'grey.200' : 'white'
                  }
                  color={
                    filteredOptions[index].isSelected
                      ? 'primary.light'
                      : 'grey.800'
                  }
                  height="auto"
                  minH="55px"
                  width="100%"
                >
                  <div
                    dangerouslySetInnerHTML={{
                      __html: filteredOptions[index].name,
                    }}
                  />
                </Button>
              )}
            />
          )}
        </AutoSizer>
      </Flex>
    </Flex>
  );
};
