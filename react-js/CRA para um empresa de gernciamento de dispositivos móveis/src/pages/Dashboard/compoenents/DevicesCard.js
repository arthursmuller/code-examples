import { Box, Flex } from '@chakra-ui/react';

import SmartphoneImg from '../../../assets/Images/smartphone.svg';
import Card from '../../../components/Card';
import DeviceIcon from '../../../components/Icons/Device';

function DevicesCard() {
  return (
    <Card p="0" mr="24px">
      <Box p="24px" borderBottom="1px solid #d7d7dc">
        <Box
          fontSize="14px"
          lineHeight="1.36"
          letterSpacing="0.56px"
          color="#282832"
          mb="30.5px"
        >
          Dispositivos
        </Box>
        <Flex
          backgroundImage={`url(${SmartphoneImg})`}
          backgroundRepeat="no-repeat"
          backgroundPosition="right 5px"
        >
          <Flex flexDirection="column" mr="30px" b>
            <Box fontSize="40px" lineHeight="1.38" color="#00c3af">
              104
            </Box>
            <Box
              textTransform="uppercase"
              fontSize="14px"
              line-height="1.43"
              color="#6e6e78"
            >
              Activos
            </Box>
          </Flex>
          <Flex flexDirection="column">
            <Box fontSize="40px" lineHeight="1.38" color="#de6163">
              03
            </Box>
            <Box
              textTransform="uppercase"
              fontSize="14px"
              line-height="1.43"
              color="#6e6e78"
              whiteSpace="nowrap"
            >
              Sin comunicacíon
            </Box>
          </Flex>
        </Flex>
      </Box>
      <Box p="24px">
        <Flex alignItems="center" mb="10px">
          <Box fontSize="20px" lineHeight="1.35" color="#de6163">
            27
          </Box>
          <Box
            fontSize="14px"
            lineHeight="1.43"
            letterSpacing="0.56px"
            color="#6e6e78"
            ml="13px"
          >
            Sem compliance
          </Box>
        </Flex>
        <Flex alignItems="center" mb="10px">
          <Box fontSize="20px" lineHeight="1.35" color="#11193c">
            04
          </Box>
          <Box
            fontSize="14px"
            lineHeight="1.43"
            letterSpacing="0.56px"
            color="#6e6e78"
            ml="13px"
          >
            Em Device Owner
          </Box>
        </Flex>
        <Flex alignItems="center" mb="10px">
          <Box fontSize="20px" lineHeight="1.35" color="#11193c">
            12
          </Box>
          <Box
            fontSize="14px"
            lineHeight="1.43"
            letterSpacing="0.56px"
            color="#6e6e78"
            ml="13px"
          >
            Em Modo Quiosque
          </Box>
        </Flex>
        <Flex alignItems="center">
          <Box fontSize="20px" lineHeight="1.35" color="#de6163">
            09
          </Box>
          <Box
            fontSize="14px"
            lineHeight="1.43"
            letterSpacing="0.56px"
            color="#6e6e78"
            ml="13px"
          >
            APP Desatualizado
          </Box>
        </Flex>
      </Box>
    </Card>
  );
}

export default DevicesCard;
