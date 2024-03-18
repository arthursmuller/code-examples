import { Box } from '@chakra-ui/react';
import React from 'react';
import { useIntl } from 'react-intl';

import PagePagination from '../../../components/PagePagination';
import Table from '../../../components/Table/Table';
import { formatBytesTo } from '../../../helper/bytes';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listCompanyConsumptionData } from '../../../store/company';
import { ListMetadata } from '../../../types/generic_list';

const DataTable = ({ data, allFilters }) => {
  const intl = useIntl();

  const dispatch = useAppDispatch();

  const { consumptionsDataMetadata } = useAppSelector((state) => state.company);
  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(
      listCompanyConsumptionData(
        {
          ...consumptionsDataMetadata,
          ...newMetadata,
        },
        allFilters
      )
    );
  };

  const columns = useSorting(
    [
      {
        accessor: 'dateConsumption',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.tabs_date',
        }),
      },
      {
        accessor: 'user',
        canSort: true,
        header: intl.formatMessage({
          id: 'global.user',
        }),
      },
      {
        accessor: 'phone',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.phone',
        }),
      },
      {
        accessor: 'group',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.group',
        }),
      },
      {
        accessor: 'subGroup',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.sub_group',
        }),
      },
      {
        accessor: 'consumption',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.package_consumption',
        }),
      },
      {
        accessor: 'roaming',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.roaming',
        }),
      },
      {
        accessor: 'carrierNetwork',
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.provider',
        }),
      },
    ],
    consumptionsDataMetadata
  );

  const rowData = data.map((consumption) => ({
    cells: [
      {
        field: 'dateConsumption',
        value: `${intl.formatDate(consumption.consumptionDate)} ${intl.formatTime(
          consumption.consumptionDate
        )}`,
      },
      {
        field: 'user',
        value: consumption?.user,
      },
      {
        field: 'phone',
        value: consumption?.phoneNumber,
      },
      {
        field: 'group',
        value: consumption?.group,
      },
      {
        field: 'subgroup',
        value: consumption?.subGroup,
      },
      {
        field: 'consumption',
        value: formatBytesTo({bytes: consumption?.consumption}),
      },
      {
        field: 'roaming',
        value: consumption?.roaming ? `${intl.formatMessage({
          id: 'global.yes',
        })}` : `${intl.formatMessage({
          id: 'global.no',
        })}`,
      },
      {
        field: 'carrierNetwork',
        value: consumption?.carrierNetwork,
      },
    ],
  }));

  return (
    <>
      <Box w="90%">
        <Table
          headerColumns={columns}
          rows={rowData}
          handleSort={handleMetadata}
        />
      </Box>
      <PagePagination
        pagination={consumptionsDataMetadata}
        onPageChange={handleMetadata}
      />
    </>
  );
};

export default DataTable;
