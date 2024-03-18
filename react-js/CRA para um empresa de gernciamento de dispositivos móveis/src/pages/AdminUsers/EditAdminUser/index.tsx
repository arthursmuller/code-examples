import { Box, useRadioGroup } from '@chakra-ui/react';
import React, { useState, useEffect, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useParams, useHistory } from 'react-router-dom';
import SimpleReactValidator from 'simple-react-validator';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import Input from '../../../components/Input';
import PageTitle from '../../../components/PageTitle';
import RadioButton from '../../../components/RadioButton';
import Select from '../../../components/Select';
import { idioms } from '../../../data/idioms';
import { timestampData } from '../../../data/timestamp';
import { PrivilegeEnum } from '../../../helper';
import { getMode, ModeObject } from '../../../helper/mode';
import { validatorDefaultMessages } from '../../../helper/validador';
import { usePrivilege } from '../../../hooks/usePrivilege';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import {
  editAdminUser,
  getAdminUsers,
  adminUserSelected,
  createAdminUser,
  adminUserSelectedClear,
} from '../../../store/adminUser';
import GroupManager from './GroupManager';
import Permissions from './Permissions';
import SubgroupManager from './SubgroupManager';

type radioTabs = 'company' | 'group_manager' | 'subgroup_manager';

const EditAdminUser = () => {
  const { accessFilterCompany, accessFilterGroup, accessFilterSubgroup } =
    usePrivilege();
  const { id } = useParams<{ id: string }>();
  const history = useHistory();
  const dispatch = useAppDispatch();
  const [selectedTab, setSelectedTab] = useState<radioTabs>('company');
  const {
    user: { company },
  } = useAppSelector((state) => state.auth);
  const { adminUser, showToaster } = useAppSelector((state) => state.adminUser);

  const CRUDMode = getMode(id);

  const initialValues = {
    gmt: 'UTC',
    language: 'pt-br',
  };

  const intl = useIntl();

  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );

  useEffect(() => {
    if (!adminUser.gmt) {
      dispatch(adminUserSelected({ gmt: initialValues.gmt }));
    }
    if (!adminUser.language) {
      dispatch(adminUserSelected({ language: initialValues.language }));
    }
  }, [adminUser.language, adminUser.gmt]);

  useEffect(() => {
    if (CRUDMode === ModeObject.UPDATE) {
      dispatch(getAdminUsers(parseInt(id)));
    }
    return () => {
      dispatch(adminUserSelectedClear());
    };
  }, [id, CRUDMode]);

  useEffect(() => {
    if (showToaster) {
      history.push(routes.adminUsers.manage);
    }
  }, [showToaster]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(adminUserSelected({ [e.target.name]: e.target.value }));
  };

  const handleInputBlur = (e: React.ChangeEvent<HTMLInputElement>) => {
    simpleValidator.current.showMessageFor(e.target.name);
  };

  const ruleOptions: {
    id: radioTabs;
    label: unknown;
    authorized: boolean;
    privilegeId: PrivilegeEnum;
  }[] = [
    {
      id: 'company',
      label: intl.formatMessage({ id: 'edit_admin.privilege_company' }),
      authorized: accessFilterCompany,
      privilegeId: PrivilegeEnum.COMPANY,
    },
    {
      id: 'group_manager',
      label: intl.formatMessage({ id: 'edit_admin.privilege_group' }),
      authorized: accessFilterGroup,
      privilegeId: PrivilegeEnum.GROUP,
    },
    {
      id: 'subgroup_manager',
      label: intl.formatMessage({ id: 'edit_admin.privilege_subgroup' }),
      authorized: accessFilterSubgroup,
      privilegeId: PrivilegeEnum.SUBGROUP,
    },
  ];

  useEffect(() => {
    setSelectedTab(
      ruleOptions.find(
        ({ privilegeId }) => privilegeId === adminUser.privilegeId
      )?.id
    );
  }, [adminUser.privilegeId]);

  useEffect(() => {
    if (CRUDMode === ModeObject.CREATE) {
      const radioDefaultValueNewRegister = accessFilterCompany
        ? 'company'
        : accessFilterGroup
        ? 'group_manager'
        : 'subgroup_manager';
      dispatch(
        adminUserSelected({
          privilegeId: ruleOptions.find(
            ({ id }) => id === radioDefaultValueNewRegister
          )?.privilegeId,
        })
      );
    }
  }, [accessFilterCompany, accessFilterGroup, CRUDMode]);

  const changeRadio = (value: radioTabs) => {
    dispatch(
      adminUserSelected({
        privilegeId: ruleOptions.find(({ id }) => id === value)?.privilegeId,
      })
    );
  };

  const { getRadioProps } = useRadioGroup({
    name: 'ruleOptions',
    onChange: changeRadio,
    value: selectedTab,
  });

  const renderTab = () => {
    switch (selectedTab) {
      case 'company':
        return <></>;
      case 'group_manager':
        return <GroupManager />;
      case 'subgroup_manager':
        return <SubgroupManager />;
    }
  };

  const handlePrimary = () => {
    if (CRUDMode === ModeObject.CREATE) {
      dispatch(createAdminUser(adminUser));
    } else {
      dispatch(editAdminUser(adminUser));
    }
  };

  const handleSecundary = () => {
    history.push(routes.adminUsers.manage);
  };

  return (
    <>
      <PageTitle
        title={
          CRUDMode === ModeObject.CREATE ? (
            <FormattedMessage id="edit_admin.user" />
          ) : (
            <FormattedMessage id="edit_admin.register" />
          )
        }
        description={<FormattedMessage id="edit_admin.description_new" />}
      />
      <FormContainer
        labelPrimary={
          CRUDMode === ModeObject.CREATE ? (
            <FormattedMessage id="global.register" />
          ) : (
            <FormattedMessage id="global.update" />
          )
        }
        disabledPrimary={!simpleValidator.current.allValid()}
        handlePrimary={handlePrimary}
        labelSecundary={<FormattedMessage id="global.cancel" />}
        handleSecundary={handleSecundary}
      >
        <Box d="flex" flexDirection="row">
          <FormControl textLabel={<FormattedMessage id="edit_admin.name" />}>
            <Input
              inputProps={{
                name: 'name',
                value: adminUser?.name || '',
                onChange: handleInputChange,
                onBlur: handleInputBlur,
              }}
            />
            {simpleValidator.current.message(
              'name',
              adminUser.name,
              'alpha_num_dash_space'
            )}
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="edit_admin.federalCode" />}
          >
            <Input
              inputProps={{
                name: 'federalCode',
                value: adminUser?.federalCode || '',
                onChange: handleInputChange,
                onBlur: handleInputBlur,
              }}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="edit_admin.email" />}>
            <Input
              inputProps={{
                id: 'email',
                type: 'email',
                name: 'email',
                value: adminUser?.email || '',
                onChange: handleInputChange,
                onBlur: handleInputBlur,
              }}
            />
            {simpleValidator.current.message(
              'email',
              adminUser.email,
              'required|email'
            )}
          </FormControl>
        </Box>
        <Box d="flex" flexDirection="row" mt="30px">
          <FormControl textLabel={<FormattedMessage id="edit_admin.company" />}>
            <Input
              inputProps={{
                backgroundColor: 'white',
                disabled: true,
                placeholder: 'Empresa',
                name: 'company',
                value: company?.name || '',
              }}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="edit_admin.gmt" />}>
            <Select
              name="gmt"
              value={adminUser?.gmt || ''}
              onChange={handleInputChange}
            >
              {timestampData.map((timestamp) => (
                <option key={timestamp.code} value={timestamp.code}>
                  {timestamp.label}
                </option>
              ))}
            </Select>
            {simpleValidator.current.message('gmt', adminUser?.gmt, 'required')}
          </FormControl>
          <FormControl
            textLabel={<FormattedMessage id="edit_admin.language" />}
          >
            <Select
              name="language"
              value={adminUser?.language || ''}
              onChange={handleInputChange}
            >
              {idioms.map((idiom) => (
                <option key={idiom.code} value={idiom.code}>
                  {idiom.text}
                </option>
              ))}
            </Select>
            {simpleValidator.current.message(
              'language',
              adminUser?.language,
              'required'
            )}
          </FormControl>
        </Box>
        <Box d="flex" flexDirection="row" mt="30px">
          <FormControl
            textLabel={<FormattedMessage id="edit_admin.password" />}
          >
            <Input
              inputProps={{
                type: 'password',
                name: 'password',
                value: adminUser?.password || '',
                onChange: handleInputChange,
              }}
            />
            {CRUDMode === ModeObject.CREATE &&
              simpleValidator.current.message(
                'password',
                adminUser?.password,
                'required'
              )}
          </FormControl>
        </Box>
        <Box mt="3%">
          <Box d="flex" flexDirection="row" w="100%">
            {ruleOptions
              .filter((rule) => rule.authorized)
              .map((option) => {
                const radio = getRadioProps({
                  value: option.id,
                });
                return (
                  <RadioButton key={option.id} {...radio}>
                    {option.label}
                  </RadioButton>
                );
              })}
          </Box>
        </Box>

        {renderTab()}
        <Permissions />
      </FormContainer>
    </>
  );
};

export default EditAdminUser;
