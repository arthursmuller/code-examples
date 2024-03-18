import { Box } from '@chakra-ui/react';
import React, { useEffect } from 'react';
import { FormattedMessage } from 'react-intl';
import { useParams, useHistory } from 'react-router-dom';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import Input from '../../../components/Input';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import { useAppSelector, useAppDispatch } from '../../../hooks/useRedux';
import routes from '../../../routes';
import {
  deviceUserSelected,
  editDeviceUser,
  getDeviceUser,
} from '../../../store/deviceUser';
import { listGroupsToFilter } from '../../../store/group';
import {
  listSubgroupsToFilter,
  subgroupFilterClear,
} from '../../../store/subgroup';
import { GroupType } from '../../../types/group';
import { SubgroupType } from '../../../types/subgroups';

const EditDeviceUsers = () => {
  const { id } = useParams<{ id: string }>();
  const history = useHistory();

  const dispatch = useAppDispatch();
  const { deviceUser, toaster } = useAppSelector((state) => state.deviceUser);
  const { groupsToFilter } = useAppSelector((state) => state.group);
  const { subgroupsToFilter } = useAppSelector((state) => state.subgroup);

  useEffect(() => {
    dispatch(getDeviceUser(parseInt(id)));
  }, [id]);

  useEffect(() => {
    dispatch(listGroupsToFilter());
  }, []);

  useEffect(() => {
    if (deviceUser.group?.id) {
      dispatch(listSubgroupsToFilter(undefined, deviceUser.group?.id));
    } else {
      dispatch(subgroupFilterClear());
    }
  }, [deviceUser.group?.id]);

  useEffect(() => {
    dispatch(getDeviceUser(parseInt(id)));
  }, [id]);

  useEffect(() => {
    if (toaster) {
      history.push(routes.deviceUsers.manage);
    }
  }, [toaster]);

  const handleInputChange = (e) => {
    handleChange(e.target.name, e.target.value);
  };

  const handleChange = (name: string, value: unknown) => {
    dispatch(deviceUserSelected({ [name]: value }));
  };

  const handleInputGroupFilter = (value: string) => {
    dispatch(listGroupsToFilter({ search: value }));
  };

  const handleInputSubgroupFilter = (value: string) => {
    if (deviceUser.group?.id) {
      dispatch(listSubgroupsToFilter({ search: value }, deviceUser.group?.id));
    } else {
      dispatch(subgroupFilterClear());
    }
  };

  const submit = () => {
    dispatch(editDeviceUser(deviceUser));
  };

  const handleSecundary = () => {
    history.push(routes.deviceUsers.manage);
  };

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="edit_deviceuser.title" />}
        description={<FormattedMessage id="edit_deviceuser.description" />}
      />
      <FormContainer
        labelPrimary={<FormattedMessage id="global.update" />}
        handlePrimary={submit}
        labelSecundary={<FormattedMessage id="global.cancel" />}
        handleSecundary={handleSecundary}
      >
        <Box d="flex" flexDirection={{ xl: 'row', lg: 'column' }}>
          <FormControl
            textLabel={<FormattedMessage id="edit_deviceuser.name" />}
          >
            <Input
              inputProps={{
                name: 'name',
                value: deviceUser.name || '',
                onChange: handleInputChange,
              }}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="edit_deviceuser.id_number_form" />}
          >
            <Input
              inputProps={{
                name: 'federalCode',
                value: deviceUser.federalCode || '',
                onChange: handleInputChange,
              }}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="edit_deviceuser.group" />}
          >
            <SelectAutocomplete
              options={groupsToFilter}
              onChange={(value: GroupType) => {
                handleChange('group', value);
                handleChange('groupId', value?.id || value);
              }}
              onInputChange={handleInputGroupFilter}
              placeholder="Grupos"
              value={deviceUser.group}
            />
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="edit_deviceuser.subgroup" />}
          >
            <SelectAutocomplete
              options={deviceUser.group?.id && subgroupsToFilter}
              onChange={(value: SubgroupType) => {
                handleChange('subGroup', value);
                handleChange('subGroupId', +value?.id || value);
              }}
              onInputChange={handleInputSubgroupFilter}
              placeholder="Subgrupos"
              value={deviceUser.subGroup}
            />
          </FormControl>
        </Box>
      </FormContainer>
    </>
  );
};

export default EditDeviceUsers;
