import { Box } from '@chakra-ui/react';
import React from 'react';
import { useIntl } from 'react-intl';

import PagePagination from '../../../components/PagePagination';
import Table from '../../../components/Table/Table';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import {
  listCompanyConsumptionSms,
} from '../../../store/company';
import { ListMetadata } from '../../../types/generic_list';

const SmsTable = ({ data, allFilters }) => {
  const intl = useIntl();

  const dispatch = useAppDispatch();

  const { consumptionsSmsMetadata } = useAppSelector((state) => state.company);
  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(
      listCompanyConsumptionSms(
        {
          ...consumptionsSmsMetadata,
          ...newMetadata,
        },
        allFilters
      )
    );
  };

  const columns = useSorting(
    [
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.tabs_date',
        }),
        accessor: 'sendDate',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.user',
        }),
        accessor: 'user',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.phone',
        }),
        accessor: 'phone',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.group',
        }),
        accessor: 'group',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.sub_group',
        }),
        accessor: 'subGroup',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.target_number',
        }),
        accessor: 'to',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.roaming',
        }),
        accessor: 'roaming',
      },
      {
        canSort: true,
        header: intl.formatMessage({
          id: 'company_consumption.provider',
        }),
        accessor: 'carrierNetwork',
      },
    ],
    consumptionsSmsMetadata
  );

  const rowData = data.map((consumption) => ({
    cells: [
      {
        field: 'sendDate',
        value: `${intl.formatDate(consumption.sendDate)} ${intl.formatTime(
          consumption?.sendDate
        )}`,
      },
      {
        field: 'user',
        value: consumption?.user,
      },
      {
        field: 'phone',
        value: consumption?.phone,
      },
      {
        field: 'group',
        value: consumption?.group,
      },
      {
        field: 'subGroup',
        value: consumption?.subGroup,
      },
      {
        field: 'to',
        value: consumption?.to,
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
    <Box w="90%">
      <Table
        headerColumns={columns}
        rows={rowData}
        handleSort={handleMetadata}
      />
      <PagePagination
        pagination={consumptionsSmsMetadata}
        onPageChange={handleMetadata}
      />
    </Box>
  );
};

export default SmsTable;
