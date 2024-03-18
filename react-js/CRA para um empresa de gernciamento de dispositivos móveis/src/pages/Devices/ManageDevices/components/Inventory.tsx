import { Box, BoxProps } from '@chakra-ui/react';
import { useEffect } from 'react';
import { useIntl } from 'react-intl';

import CardHeader from '../../../../components/CardHeader';
import Text from '../../../../components/Text';
import { useAppDispatch, useAppSelector } from '../../../../hooks/useRedux';
import { listDeviceInventory } from '../../../../store/device';

type InventoryProps = BoxProps

const Inventory = ({...rest}: InventoryProps) => {
  const dispatch = useAppDispatch();
  const { inventory } = useAppSelector((state) => state.device);
  const intl = useIntl();
  useEffect(() => {
    dispatch(listDeviceInventory());
  }, []);

  return (
    <CardHeader
      {...rest}
      mb="3%"
      title={intl.formatMessage({ id: 'devices.inventory.title' })}
      description={
        <Box
          d="flex"
          flexDirection="row"
          flexWrap="wrap"
          gridRowGap="20px"
          gridColumnGap="2%"
        >
          {inventory.map(({ manufacturer, count }) => (
            <Text
              key={manufacturer}
              color="black.500"
              fontSize="sm"
              fontWeight="normal"
              m="0"
            >
              {`${manufacturer} : ${count ? count : 0}`}
            </Text>
          ))}
        </Box>
      }
    />
  );
};
export default Inventory;
