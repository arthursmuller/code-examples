import { Box, Flex } from '@chakra-ui/react';

import GuideForDownloadImg from '../../../assets/Images/guides-for-download.svg';
import Card from '../../../components/Card';

function GuidesForDownloadCard() {
  return (
    <Card p="40px 24px 37px 24px" mb="20px" w="476px">
      <Flex>
        <Flex flexDirection="column" w="203px" mr="18px">
          <Box
            as="h2"
            fontSize="32px"
            fontWeight="300"
            lineHeight="1.34"
            color="#282832"
          >
            Manuais para Downloads
          </Box>
          <Box
            fontSize="16px"
            fontWeight="300"
            lineHeight="1.75"
            color="#6e6e78"
            mt="20px"
          >
            Confira nossos documentos y materiales de apoyo
          </Box>
          <Box mt="27px" color="#0190fe" fontSize="14px">
            Saiba mais
          </Box>
        </Flex>
        <Flex>
          <img src={GuideForDownloadImg} width="208px" />
        </Flex>
      </Flex>
    </Card>
  );
}

export default GuidesForDownloadCard;
