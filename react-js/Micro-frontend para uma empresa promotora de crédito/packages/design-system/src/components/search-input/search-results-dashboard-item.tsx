import { FC, useEffect } from 'react';

import { ListItem, Text, Flex, Icon } from '@chakra-ui/react';

import { TabAjudaInativaIcon } from '@pcf/design-system-icons';

import { highligthWord } from './search-input.utils';
import { SearchItemModel } from './search-results-dashboard';

export const SearchResultsDashboardItem: FC<{
  option: SearchItemModel;
  keyword: string;
  isCurrent: boolean;
  setCurrentItemIndex: (newIndex: number) => void;
}> = ({ option, keyword, isCurrent, setCurrentItemIndex }) => {
  useEffect(() => {
    setCurrentItemIndex(0);
    return () => {
      setCurrentItemIndex(-1);
    };
  }, [setCurrentItemIndex, keyword]);

  return (
    <ListItem
      id={option.title + option.category}
      role="option"
      p="12px 18px 8px 18px"
      bg="grey.100"
      color="primary.regular"
      sx={
        !isCurrent
          ? {
              '.highlight-keyword': {
                fontWeight: 'bold',
              },
              '.search-results__description': {
                color: 'grey.700',
              },
              svg: {
                color: 'primary.regular',
              },
            }
          : {
              bg: 'primary.light',
              color: 'white',
              '.highlight-keyword': {
                fontWeight: 'bold',
              },
              '.search-results__description': {
                color: 'white',
              },
              svg: {
                color: 'white',
              },
            }
      }
      _hover={{
        bg: 'primary.light',
        color: 'white',
        '.highlight-keyword': {
          fontWeight: 'bold',
        },
        '.search-results__description': {
          color: 'white',
        },
        svg: {
          color: 'white',
        },
      }}
    >
      <Flex
        alignItems="center"
        overflow="hidden"
        maxW="100%"
        boxSizing="border-box"
        alignSelf="flex-start"
      >
        <Icon
          mr={4}
          w="26px"
          h="26px"
          as={option.icon || TabAjudaInativaIcon}
        />
        <Flex flexDir="column" overflow="hidden">
          <Text
            textStyle="regular16"
            dangerouslySetInnerHTML={{
              __html: highligthWord(option.title, keyword),
            }}
          />
          <Text
            as="span"
            display="block"
            textStyle="regular12"
            whiteSpace="nowrap"
            overflow="hidden"
            textOverflow="ellipsis"
            className="search-results__description"
          >
            <Text textStyle="bold12" as="span">
              {option.category} -{' '}
            </Text>
            {option.description}
          </Text>
        </Flex>
      </Flex>
    </ListItem>
  );
};
