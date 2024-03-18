import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Divider,
  Text,
} from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import Select, { components } from 'react-select';

import Input from '../../../components/Input';
import { useAppSelector } from '../../../hooks/useRedux';
import { listDeviceUsers } from '../../../store/deviceUser';
import { listGroups } from '../../../store/group';
import { listSubgroups } from '../../../store/subgroup';

const sendDocument = ({ open, onClose, onSend }) => {
  const dispatch = useDispatch();
  const { companies } = useSelector((state) => state.company);
  const { groups } = useSelector((state) => state.group);
  const { subgroups } = useSelector((state) => state.subgroup);
  const { deviceUsers: users } = useAppSelector((state) => state.user);
  const [companiesSelections, setCompaniesSelections] = useState([]);
  const [groupsSelections, setGroupsSelections] = useState([]);
  const [subgroupsSelections, setSubgroupsSelections] = useState([]);
  const [usersSelections, setUsersSelections] = useState([]);

  const handleChange = (name) => (value) => {
    switch (name) {
      case 'companies':
        return setCompaniesSelections(value);
      case 'groups':
        return setGroupsSelections(value);
      case 'subgroups':
        return setSubgroupsSelections(value);
      case 'users':
        return setUsersSelections(value);
    }
  };

  const Control = ({ children, ...props }) => {
    const style = {
      fontSize: '14px',
      lineHeight: '1.36',
      color: '#282832',
      marginLeft: '20px',
    };
    const { selected } = props.selectProps;
    return (
      <components.Control {...props}>
        {props.hasValue && <span style={style}>{selected}</span>}
        {children}
      </components.Control>
    );
  };

  const createOptions = (list) => {
    return list.map((obj) => ({
      value: obj.id,
      label: obj.name,
    }));
  };

  const send = () => {
    onSend({
      companies: companiesSelections,
      groups: groupsSelections,
      subgroups: subgroups,
      users: usersSelections,
    });
  };

  useEffect(() => {
    dispatch(listGroups());
    dispatch(listSubgroups());
    dispatch(listDeviceUsers());
  }, []);

  return (
    <Modal isOpen={open} onClose={onClose} isCentered size="xl">
      <ModalOverlay />
      <ModalContent h="560px" w="900px" maxWidth="initial">
        <ModalHeader d="flex" flexDirection="column">
          <Text m="0" fontSize="2xl" fontWeight="600" color="gray.500">
            <FormattedMessage id="document.modal.send_title" />
          </Text>
        </ModalHeader>
        <ModalBody d="flex" flexDirection="column" alignItems="center">
          <Box w="100%">
            <FormControl>
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="global.recipient" />
              </FormLabel>
              <Select
                options={createOptions(companies)}
                components={{
                  DropdownIndicator: null,
                  Control,
                }}
                isMulti
                onChange={handleChange('companies')}
                placeholder="Para empresas"
                selected="Empresas"
                value={companiesSelections}
              />
            </FormControl>
            <FormControl mt="15px">
              <Select
                options={createOptions(groups)}
                components={{
                  DropdownIndicator: null,
                  Control,
                }}
                isMulti
                onChange={handleChange('groups')}
                placeholder="Para grupos"
                selected="Grupos"
                value={groupsSelections}
              />
            </FormControl>
            <FormControl mt="15px">
              <Select
                options={createOptions(subgroups)}
                components={{
                  DropdownIndicator: null,
                  Control,
                }}
                isMulti
                onChange={handleChange('subgroups')}
                placeholder="Para subgrupos"
                selected="Subgrupos"
                value={subgroupsSelections}
              />
            </FormControl>
            <FormControl mt="15px">
              <Select
                options={createOptions(users)}
                components={{
                  DropdownIndicator: null,
                  Control,
                }}
                isMulti
                onChange={handleChange('users')}
                placeholder="Para usuários"
                selected="Usuários"
                value={usersSelections}
              />
            </FormControl>
            <FormControl mt="60px">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id="document.url" />
              </FormLabel>
              <Input
                inputProps={{
                  backgroundColor: 'white',
                }}
              />
            </FormControl>
          </Box>
        </ModalBody>
        <ModalFooter d="flex" flexDirection="column" alignSelf="center">
          <Box mb="19px" w="900px">
            <Divider borderColor="gray.600" orientation="horizontal" />
          </Box>
          <Box d="flex" flexDirection="row" alignSelf="flex-start" pl="25px">
            <Box mr="14px">
              <Button
                w="180px"
                h="45px"
                fontWeight="normal"
                colorScheme="blue"
                onClick={send}
              >
                <FormattedMessage id="global.send" />
              </Button>
            </Box>
            <Box>
              <Button
                w="180px"
                h="45px"
                fontWeight="normal"
                variant="ghost"
                color="#0190fe"
                onClick={onClose}
              >
                <FormattedMessage id="global.cancel" />
              </Button>
            </Box>
          </Box>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default sendDocument;
