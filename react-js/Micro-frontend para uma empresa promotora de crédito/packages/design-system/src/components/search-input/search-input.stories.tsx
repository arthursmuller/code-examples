import { FC, ReactElement } from 'react';

import { TabNovoInativaIcon } from '@pcf/design-system-icons';

import { SearchInputContextProvider } from './search-input.context';
import { SearchInput, useDebounceSearch } from '.';
import {
  SearchResultsDashboard,
  SearchItemModel,
} from './search-results-dashboard';
import { SearchResultsDashboardItem } from './search-results-dashboard-item';

export default {
  title: 'Search Input',
  component: SearchInput,
  decorators: [
    (StoryComp): ReactElement => (
      <SearchInputContextProvider onPressEnter={(v) => console.log(v)}>
        <StoryComp />
      </SearchInputContextProvider>
    ),
  ],
};

const ITEMS: SearchItemModel[] = [
  {
    id: '1',
    route: '/',
    title: 'Início',
    category: 'Ação',
    description: 'Ir para página inicial',
    icon: TabNovoInativaIcon,
  },
  {
    id: '2',
    route: '/',
    title: 'Meu Perfil',
    category: 'Ação',
    description: 'Ir para página de perfil',
    icon: null,
  },
  {
    id: '3',
    route: '/',
    title: 'Minhas Solicitações',
    category: 'Ação',
    description: 'Ir para página de minhas solicitações',
    icon: null,
  },
  {
    id: '4',
    route: '/',
    title: 'Crédito Consignado',
    category: 'Ação',
    description: 'Simular novo consignado',
    icon: null,
  },
  {
    id: '5',
    route: '/',
    title: 'Fazer Portabilidade',
    category: 'Ação',
    description: 'Fazer Portabilidade',
    icon: null,
  },
  {
    id: '6',
    route: '/',
    title: 'Refinanciar Consignado',
    category: 'Ação',
    description: 'Fazer Portabilidade',
    icon: null,
  },
  {
    id: '7',
    route: '/',
    title: 'Solicitar Cartão',
    category: 'Ação',
    description: 'Portabilidade',
    icon: null,
  },
  {
    id: '8',
    route: '/',
    title: 'Preciso de Ajuda',
    category: 'Ação',
    description: 'Ação - Ir para a página de ajuda',
    icon: null,
  },
  // AJUDA
  {
    id: '13',
    route: '/',
    title: 'REFIN',
    description:
      'O crédito consignado é um empréstimo para pessoa física com desconto',
    category: 'Preciso de ajuda',
    icon: null,
  },
  {
    id: '14',
    route: '/',
    title: 'REFIN',
    description:
      'O crédito consignado é um empréstimo para pessoa física com desconto',
    category: 'Preciso de ajuda',
    icon: null,
  },
  {
    id: '15',
    route: '/',
    title: 'REFIN',
    description:
      'O crédito consignado é um empréstimo para pessoa física com desconto',
    category: 'Preciso de ajuda',
    icon: null,
  },
  {
    id: '16',
    route: '/',
    title: 'REFIN',
    description:
      'O crédito consignado é um empréstimo para pessoa física com desconto',
    category: 'Preciso de ajuda',
    icon: null,
  },
  {
    id: '17',
    route: '/',
    title: 'REFIN',
    description:
      'O crédito consignado é um empréstimo para pessoa física com desconto',
    category: 'Preciso de ajuda',
    icon: null,
  },
];

export const Default: FC = () => {
  const { result, keyword, isLoading, onSearch, searchText } =
    useDebounceSearch<SearchItemModel>(ITEMS, {
      keys: ['title', 'description'],
    });

  return (
    <>
      <SearchInput
        value={searchText}
        onSearch={onSearch}
        isLoading={isLoading}
      />

      <SearchResultsDashboard
        results={result}
        keyword={keyword}
        renderResult={({ option, isCurrent, setCurrentItemIndex }) => (
          <SearchResultsDashboardItem
            option={option}
            isCurrent={isCurrent}
            keyword={keyword}
            setCurrentItemIndex={setCurrentItemIndex}
          />
        )}
      />
    </>
  );
};
