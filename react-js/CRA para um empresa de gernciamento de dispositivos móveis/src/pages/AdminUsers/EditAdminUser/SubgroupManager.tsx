import { Box, Checkbox } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';

import FormSubtitle from '../../../components/FormSubtitle';
import Search from '../../../components/Icons/Search';
import Input from '../../../components/Input';
import { toggleCheckbox } from '../../../helper';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import { adminUserSelected } from '../../../store/adminUser';
import { listSubgroups } from '../../../store/subgroup';

const SubgroupManager = () => {
  const dispatch = useAppDispatch();
  const { subgroups, metadata } = useAppSelector((state) => state.subgroup);
  const { adminUser } = useAppSelector((state) => state.adminUser);
  const [input, setInput] = useState('');

  useEffect(() => {
    dispatch(listSubgroups(metadata));
  }, []);

  const handleChance = (groupId: number) => {
    dispatch(
      adminUserSelected({
        subGroupIds: toggleCheckbox(adminUser?.subGroupIds, groupId),
      })
    );
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInput(e.target.value);
  };

  return (
    <Box mt="2%">
      <FormSubtitle
        subtitle={<FormattedMessage id="edit_admin.subgroup" />}
        description={<FormattedMessage id="edit_admin.subgroup_description" />}
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
          {subgroups
            .filter((subgroup) => subgroup.name.includes(input))
            .map((subgroup, index) => {
              const margin = index % 2;
              return (
                <Checkbox
                  key={index}
                  fontSize="sm"
                  color="gray.500"
                  m={margin == 0 && '15px 0'}
                  width="360px"
                  isChecked={adminUser.subGroupIds?.includes(subgroup.id)}
                  onChange={() => handleChance(subgroup.id)}
                >
                  {subgroup.name}
                </Checkbox>
              );
            })}
        </Box>
      </Box>
    </Box>
  );
};

export default SubgroupManager;
