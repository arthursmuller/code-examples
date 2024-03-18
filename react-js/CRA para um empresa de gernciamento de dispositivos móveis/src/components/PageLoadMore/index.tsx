import { Box, Button } from '@chakra-ui/react';

import Expand from '../Icons/Expand';

interface PageLoadMoreProps {
  handleLoadMore: () => void;
}

const PageLoadMore = ({handleLoadMore}: PageLoadMoreProps) => {
  return (
    <Box
      w="90%"
      display="flex"
      justifyContent="center"
      alignItems="center"
      mt="20px"
    >
      <Button
        variant="link"
        color="blue.500"
        fontWeight="normal"
        onClick={() => handleLoadMore()}
      >
        <Expand boxSize={6} />
        Carregar mais
      </Button>
    </Box>
  );
};

export default PageLoadMore;
