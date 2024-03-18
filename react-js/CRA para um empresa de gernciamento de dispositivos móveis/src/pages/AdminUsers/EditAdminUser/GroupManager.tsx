import { Box, Checkbox } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';

import FormSubtitle from '../../../components/FormSubtitle';
import Search from '../../../components/Icons/Search';
import Input from '../../../components/Input';
import { toggleCheckbox } from '../../../helper';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { adminUserSelected } from '../../../store/adminUser';
import { listGroups } from '../../../store/group';

const GroupManager = () => {
  const dispatch = useAppDispatch();
  const { groups, metadata } = useAppSelector((state) => state.group);
  const { adminUser } = useAppSelector((state) => state.adminUser);
  const [input, setInput] = useState('');

  useEffect(() => {
    dispatch(listGroups(metadata));
  }, []);

  const handleChance = (groupId: number) => {
    dispatch(
      adminUserSelected({
        groupIds: toggleCheckbox(adminUser?.groupIds, groupId),
      })
    );
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInput(e.target.value);
  };

  return (
    <Box mt="2%">
      <FormSubtitle
        subtitle={<FormattedMessage id="edit_admin.group" />}
        description={<FormattedMessage id="edit_admin.group_description" />}
      >
        <Box w="376px" mt="1%">
          <Input
            inputProps={{
              value: input || '',
              onChange: handleInputChange,
            }}
            leftElement={<Search boxSize={6} color="gray.600" />}
          />
        </Box>
      </FormSubtitle>
      <Box mt="2%" maxHeight="300px" overflowY="auto">
        <Box d="flex" flexDirection="row" flexWrap="wrap">
          {groups
            .filter((subgroup) => subgroup.name.includes(input))
            .map((group, index) => {
              const margin = index % 2;
              return (
                <Checkbox
                  key={index}
                  fontSize="sm"
                  color="gray.500"
                  m={margin == 0 && '15px 0'}
                  width="360px"
                  isChecked={adminUser.groupIds?.includes(group.id)}
                  onChange={() => handleChance(group.id)}
                >
                  {group.name}
                </Checkbox>
              );
            })}
        </Box>
      </Box>
    </Box>
  );
};

export default GroupManager;
