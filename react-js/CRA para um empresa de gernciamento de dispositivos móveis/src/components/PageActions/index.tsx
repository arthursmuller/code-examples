import { Box } from '@chakra-ui/react';
import React, { useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { Link } from 'react-router-dom';

import Button from '../Button';
import Copy from '../Icons/Copy';
import Download from '../Icons/Download';
import Search from '../Icons/Search';
import Input from '../Input';

interface PageActionsProps {
  initialSearch?: string;
  onSearch?: (value: string) => void;
  onExcel?: () => void;
  onCopy?: () => void;
  linkNew?: string;
  labelButtonNew?: React.ReactNode;
}

const defaultProps = {
  labelButtonNew: <FormattedMessage id='global.register_new' />,
  initialSearch: '',
};

const PageActions: React.FC<PageActionsProps> = ({
  initialSearch,
  onSearch,
  onExcel,
  onCopy,
  linkNew,
  labelButtonNew,
}: PageActionsProps) => {
  const [searchFilterUndebounced, setSearchFilterUndebounced] =
    useState(initialSearch);

  return (
    <Box
      w="90%"
      d="flex"
      flexDirection="row"
      justifyContent="space-between"
      m="3% 0 3% 0"
    >
      <Box w="376px" ml="1.5%">
        {onSearch && (
          <Input
            inputProps={{
              "data-testid": 'search-input',
              backgroundColor: 'white',
              value: searchFilterUndebounced,
              onChange: (e: React.ChangeEvent<HTMLInputElement>) => {
                setSearchFilterUndebounced(e.target.value);
                onSearch(e.target.value);
              },
            }}
            leftElement={<Search boxSize={6} color="gray.600" />}
          />
        )}
      </Box>
      <Box alignSelf="center" mr="1.5%" d="flex" gridColumnGap="40px">
        <Button
          color="blue"
          variant="link"
          fontWeight="normal"
          disabled={!onExcel}
        >
          <Download boxSize={6} />
          <FormattedMessage id="global.excel" />
        </Button>
        <Button
          color="blue"
          variant="link"
          fontWeight="normal"
          disabled={!onCopy}
        >
          <Copy boxSize={6} />
          <FormattedMessage id="global.copy" />
        </Button>
        {linkNew && (
          <Link to={linkNew}>
            <Button
              bg="blue.500"
              color="white"
              h="45px"
              w="176px"
              fontWeight="400"
              _hover={{ opacity: '70%' }}
            >
              {labelButtonNew}
            </Button>
          </Link>
        )}
      </Box>
    </Box>
  );
};

PageActions.defaultProps = defaultProps;

export default PageActions;
