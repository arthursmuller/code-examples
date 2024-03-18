import { Box } from '@chakra-ui/react';
import { string, bool, any } from 'prop-types';

import Card from '../../../components/Card';

const propTypes = {
  title: string,
  last: bool,
  children: any,
};

const defaultProps = {
  last: false,
};

function DashboardCard({ title, last, children, ...props }) {
  return (
    <Card p="24px" mr={!last ? '24px' : '0'} {...props}>
      <Box
        fontSize="14px"
        lineHeight="1.36"
        letterSpacing="0.56px"
        color="#282832"
        mb="30.5px"
      >
        {title}
      </Box>
      {children}
    </Card>
  );
}

DashboardCard.propTypes = propTypes;
DashboardCard.defaultProps = defaultProps;
export default DashboardCard;
