import React, {
  FC,
  ComponentType,
  ReactElement,
  useState,
  useEffect,
} from 'react';

import {
  ListItem,
  UnorderedList,
  Text,
  Icon,
  ListProps,
  Flex,
} from '@chakra-ui/react';
import { GroupedVirtuoso } from 'react-virtuoso';

import { InfoIcon } from '@pcf/design-system-icons';

import { useSearchInputContext } from './search-input.context';

import { zIndexes } from '../../consts';
// import { useKeyPress } from '../../hooks/use-key-press';

export interface SearchItemModel {
  id: string;
  title: string;
  category: string;
  description: string;
  icon?: FC | ComponentType;
  route: string;
}

interface SearchResultsDashboardProps {
  results: SearchItemModel[];
  keyword: string;
  chakraProps?: ListProps;
  renderResult({
    option,
    isCurrent,
    index,
    setCurrentItemIndex,
  }: {
    option: SearchItemModel;
    isCurrent: boolean;
    index: number;
    setCurrentItemIndex: (newIndex: number) => void;
  }): ReactElement;
}

export const SearchResultsDashboard: FC<SearchResultsDashboardProps> = ({
  results,
  keyword,
  chakraProps = {},
  renderResult,
}) => {
  const {
    virtuosoRef,
    currentItemIndex,
    setCurrentItemIndex,
    isSearchInputFocused,
    // searchInputRef,
    // onPressEnter,
  } = useSearchInputContext();

  // const enterPressed = useKeyPress(searchInputRef.current, 'Enter');

  const searched = keyword && !!results.length && isSearchInputFocused;

  const helpItems = results.filter(
    (obj) => obj.category === 'Preciso de ajuda',
  );

  const actionItems = results.filter(
    (obj) => obj.category !== 'Preciso de ajuda',
  );

  const groupedResult = [...actionItems, ...helpItems];

  const [height, setHeight] = useState(0);

  const groupCounts = [
    groupedResult.length - helpItems.length,
    helpItems.length,
  ];

  // useEffect(() => {
  //   if (enterPressed) {
  //     onPressEnter(groupedResult[currentItemIndex]);
  //   }
  // }, [enterPressed, currentItemIndex, onPressEnter, groupedResult]);

  useEffect(() => {
    setCurrentItemIndex(0);
    return () => {
      setCurrentItemIndex(1);
    };
  }, [setCurrentItemIndex]);

  return (
    <UnorderedList
      borderRadius="8px 8px"
      py={searched ? 2 : 0}
      shadow="0px 4px 24px rgba(0, 0, 0, 0.15)"
      ml={0}
      listStyleType="none"
      bg="white"
      boxShadow="soft"
      flexGrow={1}
      flexDir="column"
      zIndex={zIndexes.absoluteElements}
      {...chakraProps}
    >
      {searched && (
        <GroupedVirtuoso
          style={{
            display: 'flex',
            flexDirection: 'column',
            flexGrow: 1,
            height: height ? `${height}px` : '50vh',
            maxHeight: '50vh',
          }}
          ref={virtuosoRef}
          totalListHeightChanged={(_height) => {
            setHeight(_height);
          }}
          groupCounts={groupCounts}
          groupContent={(index) => {
            if (groupCounts[index] !== 0) {
              return (
                <Flex
                  flexDirection="column"
                  alignItems="center"
                  justifyContent="center"
                  height="45px"
                  bgColor="white"
                  borderY="1px solid"
                  borderColor="grey.200"
                >
                  <Text textStyle="bold16" textColor="grey.700">
                    {index === 0 ? 'Ações' : 'Perguntas e respostas'}
                  </Text>
                </Flex>
              );
            }
            return null;
          }}
          itemContent={(index) => {
            return renderResult({
              option: groupedResult[index],
              isCurrent: index === currentItemIndex,
              index,
              setCurrentItemIndex,
            });
          }}
        />
      )}

      {!results.length && (
        <ListItem textAlign="center" p={5}>
          <Icon as={InfoIcon} w="30px" h="30px" color="secondary.regular" />
          <Text mt={2}>
            Nenhum Resultado para a palavara <strong>{keyword}</strong>.
          </Text>
          <Text>
            Tente uma nova palavra, por exemplo <strong>consignado</strong>.
          </Text>
        </ListItem>
      )}
    </UnorderedList>
  );
};
