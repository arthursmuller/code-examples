import { Box, Flex } from '@chakra-ui/react';

import VideoTutorialsImg from '../../../assets/Images/video-tutorials.svg';
import Card from '../../../components/Card';

function VideoTutorialsCard() {
  return (
    <Card p="0" w="476px" overflow="hidden" height="100%">
      <Flex>
        <Flex p="40px 0 32px 24px" flexDirection="column" w="203px" mr="18px">
          <Box
            as="h2"
            fontSize="32px"
            fontWeight="300"
            lineHeight="1.34"
            color="#282832"
          >
            Videos tutoriais
          </Box>
          <Box
            fontSize="16px"
            fontWeight="300"
            lineHeight="1.75"
            color="#6e6e78"
            mt="20px"
          >
            Confira nossos vídeos explicativos.
          </Box>
          <Box mt="27px" color="#0190fe" fontSize="14px">
            Saiba mais
          </Box>
        </Flex>
        <Flex marginRight="-20px">
          <img src={VideoTutorialsImg} />
        </Flex>
      </Flex>
    </Card>
  );
}

export default VideoTutorialsCard;
