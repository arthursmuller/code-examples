import { FC } from 'react';

import {
  Flex,
  Icon,
  ListItem,
  Text,
  LinkBox,
  LinkOverlay,
} from '@chakra-ui/react';
import { Link as ReactRouterDomLink, useHistory } from 'react-router-dom';
import { v4 as uuidv4 } from 'uuid';

import {
  SearchInput,
  SearchItemModel,
  SearchResultsDashboard,
  useDebounceSearch,
  highligthWord,
  SearchInputContextProvider,
} from '@pcf/design-system';
import { faqQuestions } from 'features/main/features/ajuda/ajuda-questions';
import { mainRoutePaths } from 'features/main/routes';
import {
  TabAjudaInativaIcon,
  TabProfileInativaIcon,
} from '@pcf/design-system-icons';
import { perfilOptions } from 'features/main/features/perfil/perfil.consts';

import { useAvaivalableMainItems } from './useAvailableMainItems';

const faqItems: SearchItemModel[] = faqQuestions.map((question) => {
  return {
    id: `${question.id}`,
    title: question.question,
    description: question.answer,
    category: 'Preciso de ajuda',
    route: mainRoutePaths.AJUDA,
    icon: TabAjudaInativaIcon,
  };
});

const perfilItems: SearchItemModel[] = perfilOptions
  .filter((option) => !option.disabled)
  .map(({ title, description, route }) => {
    return {
      id: uuidv4(),
      title,
      description,
      route,
      category: 'Ação',
      icon: TabProfileInativaIcon,
    };
  });

const hoverStyles = {
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
};

export const Search: FC = () => {
  const { availableMainItems } = useAvaivalableMainItems();
  const { result, keyword, isLoading, onSearch, resetSearch, searchText } =
    useDebounceSearch<SearchItemModel>(
      [...faqItems, ...availableMainItems, ...perfilItems],
      {
        keys: ['title', 'description'],
      },
    );

  const history = useHistory();

  const handleOnSelect = (option: SearchItemModel): void => {
    history.push(
      option.route,
      option.route === mainRoutePaths.AJUDA ? { questionId: option.id } : {},
    );
    resetSearch();
  };

  return (
    <Flex flexDir="column" position="relative" flexGrow={1}>
      <SearchInputContextProvider
        onPressEnter={(item) => handleOnSelect(item as SearchItemModel)}
      >
        <SearchInput
          value={searchText}
          onSearch={onSearch}
          isLoading={isLoading}
        />

        <SearchResultsDashboard
          chakraProps={{
            width: '100%',
            position: 'absolute',
            top: '55px',
            overflow: 'y',
          }}
          results={result}
          keyword={keyword}
          renderResult={({ option, isCurrent }) => (
            <LinkBox
              key={option.id}
              onMouseDown={() => handleOnSelect(option)}
              as={ListItem}
              id={option.title + option.category}
              role="option"
              p="12px 18px 8px 18px"
              bg="grey.100"
              color="primary.regular"
              sx={
                isCurrent
                  ? hoverStyles
                  : {
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
              }
              _hover={hoverStyles}
            >
              <LinkOverlay
                as={ReactRouterDomLink}
                display="flex"
                to={option.route}
                alignItems="center"
              >
                <Icon mr={4} w="26px" h="26px" as={option.icon} />
                <Flex flexDir="column" overflow="hidden">
                  <Text
                    textStyle="regular16"
                    dangerouslySetInnerHTML={{
                      __html: highligthWord(option.title, keyword),
                    }}
                  />
                  <Text
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
              </LinkOverlay>
            </LinkBox>
          )}
        />
      </SearchInputContextProvider>
    </Flex>
  );
};
