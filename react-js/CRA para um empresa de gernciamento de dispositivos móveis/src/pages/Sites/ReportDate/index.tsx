import { Box } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';

import DatePicker from '../../../components/Datepicker';
import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PageLoadMore from '../../../components/PageLoadMore';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import { Timeline } from '../../../components/Timeline';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { listDeviceUserToFilter } from '../../../store/deviceUser';
import { listDataFirst, listDataMore, reportsSitesDateListClean } from '../../../store/reports';

const ReportDate = () => {
  const dispatch = useAppDispatch();
  const { sitesDate, metadataDate } = useAppSelector((state) => state.reports);
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  const [form, setForm] = useState({ user: null, accessedAt: null });

  useEffect(() => {
    if (form.user?.id) {
      dispatch(
        listDataFirst(metadataDate, {
          userId: form.user.id,
          accessedAt: form?.accessedAt?.toISOString().split('T')[0],
        })
      );
    } else {
      dispatch(reportsSitesDateListClean());
    }
  }, [form]);

  useEffect(() => {
    dispatch(listDeviceUserToFilter());
  }, []);

  const handleDateSelect = (field) => (value) => {
    setForm({ ...form, [field]: value });
  };

  const handleLoadMore = () => {
    dispatch(listDataMore({ ...metadataDate, page: +metadataDate.page + 1 }));
  };

  const handleDeviceUsers = (value) => {
    setForm({ ...form, user: value });
  };

  const handleDeviceUsersInput = (value) => {
    dispatch(listDeviceUserToFilter({search: value}));
  };

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="date_report.title" />}
        description={<FormattedMessage id="date_report.title_text" />}
      />

      <FormContainer>
      <PageFilter>
        <FormControl
          textLabel={<FormattedMessage id="date_report.select_user" />}
        >
          <SelectAutocomplete
            options={devicesUsersToFilter}
            value={form.user}
            onChange={handleDeviceUsers}
            onInputChange={handleDeviceUsersInput}
            getOptionLabel={(option) =>
              `${option.name || ''} ${option.phoneNumber}`
            }
            placeholder={<FormattedMessage id="sites_url.field_user" />}
          />
        </FormControl>
        <FormControl textLabel={<FormattedMessage id="global.date" />}>
          <DatePicker
            name="date"
            selected={form.accessedAt}
            onChange={handleDateSelect('accessedAt')}
          />
        </FormControl>
      </PageFilter>
      </FormContainer>

      <PageActions />

      <Box width="90%" display="flex" alignItems="center" mt="60px">
        <Timeline rows={sitesDate} />
      </Box>
      {sitesDate?.length > 0 && metadataDate < metadataDate.totalPages && (
        <PageLoadMore handleLoadMore={handleLoadMore} />
      )}
    </>
  );
};

export default ReportDate;
