import { Text, Box, chakra, useTheme } from '@chakra-ui/react';
import { Table, TableProps as AntDTableProps } from 'antd';

import { Loader } from '@pcf/design-system';

import 'antd/lib/table/style/index.css';
import 'antd/lib/pagination/style/index.css';

export type TableProps<RecordType> = Omit<
  AntDTableProps<RecordType>,
  'components'
>;

const CustomTable = chakra.table;
const Thead = chakra.thead;
const Th = chakra.th;
const Tr = chakra.tr;
const Td = chakra.td;

const locale = {
  emptyText: (
    <Text w="100%" textStyle="regular16_20" textAlign="center" padding={10}>
      Não há dados
    </Text>
  ),
};

const customTableComponents = {
  table: ({ children }) => {
    return <CustomTable>{children}</CustomTable>;
  },
  header: {
    wrapper: ({ children }) => {
      return (
        <Thead
          mb={4}
          alignItems="center"
          borderBottom="1px solid"
          borderColor="secondary.mid-dark"
        >
          {children}
        </Thead>
      );
    },
    row: ({ children }) => {
      return <Tr h={10}>{children}</Tr>;
    },
    cell: ({ children }) => {
      return (
        <Th>
          <Text p={4} color="secondary.mid-dark" textStyle="bold14">
            {children}
          </Text>
        </Th>
      );
    },
  },
  body: {
    row: ({ children }) => {
      return <Tr _odd={{ bg: '#DDEAFF' }}>{children}</Tr>;
    },
    cell: ({ children, colSpan }) => {
      return (
        <Td color="grey.800" colSpan={colSpan} textStyle="regular14">
          {children}
        </Td>
      );
    },
  },
};

export function BemTable<RecordType extends object = any>({ // eslint-disable-line
  dataSource = [],
  columns,
  pagination,
  loading,
  ...otherProps
}: TableProps<RecordType>): JSX.Element {
  const { zIndices } = useTheme();

  let loadingStyles = {};

  if (loading) {
    loadingStyles = {
      '.ant-table table': {
        opacity: 0.4,
      },
    };
  }

  return (
    <Box
      position="relative"
      sx={{
        // antd overrides
        '.ant-table table': {
          borderCollapse: 'collapse',
        },
        // fix Pagination icon
        '.anticon svg': {
          display: 'block',
        },
        ...loadingStyles,
      }}
    >
      {loading && (
        <Loader position="absolute" w="100%" zIndex={zIndices.overlay} />
      )}
      <Table
        locale={locale}
        pagination={pagination}
        components={customTableComponents}
        dataSource={dataSource}
        columns={columns}
        loading={loading}
        {...otherProps}
      />
    </Box>
  );
}
