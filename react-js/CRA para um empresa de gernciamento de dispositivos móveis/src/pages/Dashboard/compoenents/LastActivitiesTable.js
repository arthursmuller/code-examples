import { Table, Tr, Td, Box } from '@chakra-ui/react';
import { array } from 'prop-types';

const propTypes = {
  data: array,
};

function LastActivitiesTable({ data }) {
  const renderTableData = () => {
    return data.map((item, index) => {
      return (
        <Tr key={index} fontSize="12px" letterSpacing="0.48px" color="#6e6e78">
          <Td p="20px 0">{item.description}</Td>
          <Td p="0" textAlign="right">
            {item.time}
          </Td>
        </Tr>
      );
    });
  };

  return (
    <>
      <Table>{data && renderTableData()}</Table>
      <Box mt="44px" textAlign="center" color="#0190fe" fontSize="14px">
        Ver todas
      </Box>
    </>
  );
}

LastActivitiesTable.propTypes = propTypes;
export default LastActivitiesTable;
