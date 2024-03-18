import { Box, Textarea, Text } from '@chakra-ui/react';
import React, { useEffect, useState, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useHistory, useRouteMatch } from 'react-router-dom';
import SimpleReactValidator from 'simple-react-validator';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import { validatorDefaultMessages } from '../../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import { listDeviceUserToFilter } from '../../../store/deviceUser';
import { createDocument } from '../../../store/document';
import { listGroupsToFilter } from '../../../store/group';
import { createMessage } from '../../../store/message/index';
import { listSubgroupsToFilter } from '../../../store/subgroup';
import { CompanyType } from '../../../types/company';
import { DeviceUserType } from '../../../types/deviceUser';
import { GroupType } from '../../../types/group';
import { SubgroupType } from '../../../types/subgroups';

const MessagesEdit = () => {
  const dispatch = useAppDispatch();
  const history = useHistory();
  const isMessages = !!useRouteMatch(routes.messages.register);

  const { company } = useAppSelector((state) => state.auth.user);
  const { groupsToFilter } = useAppSelector((state) => state.group);
  const { subgroupsToFilter } = useAppSelector((state) => state.subgroup);
  const { devicesUsersToFilter } = useAppSelector((state) => state.deviceUser);
  const { toaster: toasterMessage } = useAppSelector((state) => state.message);
  const { toaster: toasterDocument } = useAppSelector(
    (state) => state.document
  );

  const [filterCompany, setFilterCompany] = useState<CompanyType>();
  const [selectedGroup, setSelectedGroup] = useState<GroupType[]>([]);
  const [filterSubGroup, setFilterSubGroup] = useState<SubgroupType[]>([]);
  const [filterDeviceUser, setFilterDeviceUser] = useState<DeviceUserType[]>(
    []
  );
  const [message, setMessage] = useState<string>();

  const toaster = isMessages ? toasterMessage : toasterDocument;
  const createEntity = isMessages ? createMessage : createDocument;
  const linkList = (isMessages ? routes.messages : routes.documents).list;

  const keysIntl = {
    title: isMessages ? 'message_register.title' : 'document.title',
    description: isMessages
      ? 'message_register.title_text'
      : 'document.description',
    field_message: isMessages ? 'message.title' : 'document.field_message',
    send_label: isMessages ? 'message.send_label' : 'document.send_label',
    filter_title: isMessages ? 'message.filter_title' : 'document.filter_title',
  };

  const handlePrimary = () => {
    dispatch(
      createEntity({
        companyId: filterCompany ? filterCompany.id : null,
        groupIds: selectedGroup.map((groupsId) => groupsId.id),
        subGroupIds: filterSubGroup.map((subGroupsId) => subGroupsId.id),
        userIds: filterDeviceUser.map((usersId) => usersId.id),
        content: message,
      })
    );
  };

  const handleSecundary = () => {
    history.push(linkList);
  };

  const handleFilterGroup = (newFilter: GroupType[]) => {
    setSelectedGroup(newFilter);
  };

  const handleFilterCompany = (newFilter: CompanyType) => {
    setFilterCompany(newFilter);
  };

  const handleFilterGroupChange = (value) => {
    dispatch(listGroupsToFilter({ search: value }));
  };

  const handleFilterSubGroup = (newFilter) => {
    setFilterSubGroup(newFilter);
  };

  const handleFilterSubGroupChange = (value) => {
    dispatch(listSubgroupsToFilter({ search: value }));
  };

  const handleFilterDeviceUser = (newFilter) => {
    setFilterDeviceUser(newFilter);
  };

  const handleFilterDeviceUserChange = (value) => {
    dispatch(listDeviceUserToFilter({ search: value }));
  };

  const handleInputMessage = (newMessage) => {
    setMessage(newMessage.target.value);
  };

  useEffect(() => {
    if (toaster) {
      history.push(linkList);
    }
  }, [toaster]);

  useEffect(() => {
    dispatch(listGroupsToFilter());
  }, []);

  useEffect(() => {
    dispatch(listDeviceUserToFilter());
  }, []);

  useEffect(() => {
    dispatch(listSubgroupsToFilter());
  }, []);

  const intl = useIntl();

  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );

  return (
    <>
      <PageTitle
        title={<FormattedMessage id={keysIntl.title} />}
        description={<FormattedMessage id={keysIntl.description} />}
      />

      <FormContainer
        labelPrimary={<FormattedMessage id={keysIntl.send_label} />}
        disabledPrimary={!simpleValidator.current.allValid()}
        labelSecundary={<FormattedMessage id="global.cancel" />}
        handlePrimary={handlePrimary}
        handleSecundary={handleSecundary}
      >
        <Box w="100%">
          <Text mb="6" fontSize="2xl" fontWeight="600" color="gray.500">
            <FormattedMessage id={keysIntl.filter_title} />
          </Text>
          <FormControl
            textLabel={<FormattedMessage id="global.company_name" />}
          >
            <SelectAutocomplete
              options={[company]}
              value={filterCompany}
              placeholder={<FormattedMessage id="global.company_name" />}
              onChange={handleFilterCompany}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="global.group" />}>
            <SelectAutocomplete
              options={groupsToFilter}
              isMulti
              value={selectedGroup}
              onChange={handleFilterGroup}
              onInputChange={handleFilterGroupChange}
              placeholder={<FormattedMessage id="global.group" />}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="global.subgroup" />}>
            <SelectAutocomplete
              options={subgroupsToFilter}
              value={filterSubGroup}
              isMulti
              onChange={handleFilterSubGroup}
              onInputChange={handleFilterSubGroupChange}
              placeholder={<FormattedMessage id="global.subgroup" />}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="global.users" />}>
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={filterDeviceUser}
              isMulti
              onChange={handleFilterDeviceUser}
              onInputChange={handleFilterDeviceUserChange}
              placeholder={<FormattedMessage id="global.subgroup" />}
              getOptionLabel={(option) => 
                `${option.name || ''} ${option.phoneNumber}`}
            />
          </FormControl>
          <FormControl
            mt="60px"
            textLabel={<FormattedMessage id={keysIntl.field_message} />}
          >
            <Textarea h="180px" resize="none" onChange={handleInputMessage} />
            {simpleValidator.current.message('message', message, 'required')}
          </FormControl>
        </Box>
      </FormContainer>
    </>
  );
};

export default MessagesEdit;
