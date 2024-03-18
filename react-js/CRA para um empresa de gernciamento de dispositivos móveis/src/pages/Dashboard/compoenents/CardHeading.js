import { Box, Flex } from '@chakra-ui/react';
import { any, string, bool } from 'prop-types';
import { FormattedMessage } from 'react-intl';

import Card from '../../../components/Card';

const propTypes = {
  qty: any,
  title: string,
  icon: any,
  last: bool,
  qtyColor: string,
};

const defaultProps = {
  qtyColor: '#282832',
};

function CardHeading({ last, qty, icon, title, qtyColor }) {
  return (
    <Card mr={!last ? '24px' : 'none'} p="24px">
      <Flex justify="space-between">
        <Box fontSize="40px" lineHeight="1.38" color={qtyColor}>
          {qty}
        </Box>
        <Box>{icon}</Box>
      </Flex>
      <Box
        textTransform="uppercase"
        fontSize="14px"
        lineHeight="1.43"
        letterSpacing="0.56px"
        mt="7px"
        color="#6e6e78"
      >
        <FormattedMessage id={title} />
      </Box>
    </Card>
  );
}

CardHeading.propTypes = propTypes;
CardHeading.defaultProps = defaultProps;
export default CardHeading;
