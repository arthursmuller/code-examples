import { Box, Flex } from '@chakra-ui/react';

import HelpImg from '../../../assets/Images/dashboard-help.svg';
import Card from '../../../components/Card';

function HelpCard() {
  return (
    <Card p="0" mt="40px" w="100%" overflow="hidden">
      <Flex justifyContent="space-between">
        <Flex p="40px 0 0 44px" flexDirection="column">
          <Box
            as="h2"
            fontSize="32px"
            fontWeight="300"
            lineHeight="1.34"
            color="#282832"
          >
            Alguma dúvida?
          </Box>
          <Box
            fontSize="16px"
            fontWeight="300"
            lineHeight="1.75"
            color="#6e6e78"
            mt="20px"
            w="260px"
          >
            Acesse nossa página de FAQ e pesquise por sua dúvida.
          </Box>
          <Box mt="31px" color="#0190fe" fontSize="14px">
            Perguntas frequentes
          </Box>
        </Flex>
        <Flex>
          <img src={HelpImg} />
        </Flex>
      </Flex>
    </Card>
  );
}

export default HelpCard;
