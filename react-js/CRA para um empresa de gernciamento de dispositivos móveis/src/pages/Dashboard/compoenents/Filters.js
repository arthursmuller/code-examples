import { ChevronDownIcon, ChevronUpIcon } from '@chakra-ui/icons';
import { Box, FormControl } from '@chakra-ui/react';
import { useState } from 'react';

import Card from '../../../components/Card';
import Input from '../../../components/Input';
import Select from '../../../components/Select';
import Text from '../../../components/Text';

function Filters({ groups, subgroups, users }) {
  const [filterOpened, setFilterOpened] = useState(true);

  const renderOptions = (list) => {
    return []; 
    // list?.map((obj) => (
    //   <option value={obj.id} key={obj.id}>
    //     {obj.name}
    //   </option>
    // ));
  };

  return (
    <Card
      backgroundColor="transparent"
      p={filterOpened ? '0 0 31px 0' : '0'}
      borderBottom="1px solid #d7d7dc"
      mb="31px"
      borderRadius="0"
      w="100%"
    >
      <Text
        margin="0 0 20px 0"
        color="gray.300"
        fontSize="md"
        fontWeight="bold"
      >
        {!filterOpened && (
          <ChevronDownIcon
            color="gray.600"
            boxSize="8"
            onClick={() => setFilterOpened(!filterOpened)}
            cursor="pointer"
          />
        )}
        {filterOpened && (
          <ChevronUpIcon
            color="gray.600"
            boxSize="8"
            onClick={() => setFilterOpened(!filterOpened)}
            cursor="pointer"
          />
        )}
        Filtrar
      </Text>
      <Box
        d="flex"
        flexDirection="row"
        display={filterOpened ? 'flex' : 'none'}
      >
        <Box w="100%" mr="24px">
          <FormControl w="100%" mr="24px">
            <Input
              inputProps={{
                placeholder: 'Empresa',
              }}
            />
          </FormControl>
        </Box>
        <Box w="100%" mr="24px">
          <FormControl w="100%">
            <Select placeholder="Grupo">{renderOptions(groups)}</Select>
          </FormControl>
        </Box>
        <Box w="100%" mr="24px">
          <FormControl>
            <Select placeholder="Subgrupo">{renderOptions(subgroups)}</Select>
          </FormControl>
        </Box>
        <Box w="100%">
          <FormControl>
            <Select placeholder="Usuários">{renderOptions(users)}</Select>
          </FormControl>
        </Box>
      </Box>
    </Card>
  );
}

export default Filters;
