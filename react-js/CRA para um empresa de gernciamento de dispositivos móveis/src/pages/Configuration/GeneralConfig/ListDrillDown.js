import { Flex, Box } from '@chakra-ui/react';
import { object } from 'prop-types';

const propTypes = {
  data: object.isRequired,
};

function ListDrillDown({ data }) {
  const itemList = (label, value, color) => {
    return (
      <Flex>
        <Box
          fontSize="14px"
          fontWeight="bold"
          lineHeight="2.86"
          letterSpacing="0.56px"
          textAlign="left"
          color="#6e6e78"
          mr="5px"
        >
          {label}:
        </Box>
        <Box
          fontSize="14px"
          lineHeight="2.86"
          letterSpacing="0.56px"
          textAlign="left"
          color={color ? color : '#6e6e78'}
        >
          {value}
        </Box>
      </Flex>
    );
  };

  return (
    <Flex p="30px 0 30px 70px">
      <Flex flexDirection="column">
        {itemList('Monitorear GPS', data.track_gps)}
        {itemList('Precisión GPS', data.gps_precision)}
        {itemList('Bloquear también en Wi-Fi', data.block_wifi)}
        {itemList('Bloqueo total de URL', data.block_url)}
        {itemList('Bloqueo total de aplicaciones', data.block_apps)}
      </Flex>
      <Flex flexDirection="column" ml="100px">
        {itemList('E-mail de Aviso', data.warning_email)}
        {itemList('Permitir compartir internet (Hotspot)', data.hotspot)}
        {itemList('Permitir inicio en modo seguro', data.safe_start)}
        {itemList('Permitir agregar usuário', data.add_user)}
        {itemList(
          'Permitir instalación de SD Card Externo',
          data.install_external_sd
        )}
      </Flex>
    </Flex>
  );
}

ListDrillDown.propTypes = propTypes;
export default ListDrillDown;
