import { Box } from '@chakra-ui/react';
import { useIntl } from 'react-intl';
import { useHistory } from 'react-router-dom';

import { routeWithParameters } from '../../../helper';
import routes from '../../../routes';
import GraphIcon from '../../Icons/Graph';
import GroupsIcon from '../../Icons/Groups';
import TableActions from '../../TableActions';
import MenuItem from '../../TableActions/MenuItem';

interface MenuListPropsType {
  navigationProps?: {
    [k: string]: string | number;
  };
  showApplicationDeviceUsersItem?: boolean;
  showConsumptionHistory?: boolean;
}

const MenuList = ({
  navigationProps,
  showApplicationDeviceUsersItem,
  showConsumptionHistory,
}: MenuListPropsType) => {
  const intl = useIntl();
  const history = useHistory();

  const consumptionHistoryItem = {
    icon: <GraphIcon boxSize={6} color="white" mr="5px" />,
    text: intl.formatMessage({
      id: 'application_manage.table_action.storage',
    }),
    onClick: () =>
      history.push(
        routeWithParameters(
          navigationProps?.deviceUserId
            ? routes.application.consumptionHistoryByDeviceUser
            : routes.application.consumptionHistory,
          navigationProps
        )
      ),
  };

  const applicationDeviceUsersItem = {
    icon: <GroupsIcon boxSize={6} />,
    text: intl.formatMessage({
      id: 'application_manage.table_action.application',
    }),
    onClick: () =>
      history.push(
        routeWithParameters(
          routes.application.applicationDeviceUsers,
          navigationProps
        )
      ),
  };

  const menuItems = (
    <>
      {!!showConsumptionHistory && <MenuItem {...consumptionHistoryItem} />}
      {!!showApplicationDeviceUsersItem && (
        <MenuItem {...applicationDeviceUsersItem} />
      )}
    </>
  );

  return (
    <Box>
      <TableActions moreItems={menuItems} />
    </Box>
  );
};

export default MenuList;
