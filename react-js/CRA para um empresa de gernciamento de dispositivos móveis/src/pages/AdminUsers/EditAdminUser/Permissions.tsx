import { Box, Checkbox } from '@chakra-ui/react';
import React, { useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormSubtitle from '../../../components/FormSubtitle';
import { allPermissions } from '../../../data/permissions';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { adminUserSelected } from '../../../store/adminUser';
import { UserAdminRolesType } from '../../../types/userAdmin';

const Permissions = () => {
  const dispatch = useAppDispatch();

  const { adminUser } = useAppSelector((state) => state.adminUser);
  const myPermissions = adminUser?.role || {
    viewUsersGps: false,
    editUserRole: false,
    editConfig: false,
    editLimits: false,
    editActions: false,
  };

  const intl = useIntl();

  useEffect(() => {
    setRole({ ...myPermissions });
  }, []);

  const setRole = (role: UserAdminRolesType) => {
    dispatch(adminUserSelected({ role }));
  };

  const handleChance = (permissionCode: keyof UserAdminRolesType) => {
    setRole({
      ...myPermissions,
      [permissionCode]: !myPermissions[permissionCode],
    });
  };

  return (
    <Box mt="2%">
      <FormSubtitle
        subtitle={intl.formatMessage({ id: 'edit_admin.permissions.subtitle' })}
        description={intl.formatMessage({
          id: 'edit_admin.permissions.description',
        })}
      />

      <Box mt="2%" d="flex" flexDirection="row" flexWrap="wrap">
        {allPermissions.map((permission) => (
          <Box
            key={permission.code}
            d="flex"
            flexDirection="column"
            width="360px"
          >
            <Checkbox
              fontSize="sm"
              color="gray.500"
              isChecked={myPermissions[permission?.code]}
              onChange={() => handleChance(permission.code)}
            >
              <FormattedMessage id={permission.textIntlKey} />
            </Checkbox>
          </Box>
        ))}
      </Box>
    </Box>
  );
};

export default Permissions;
