import { FC } from 'react';

import { Stack, Flex, Box, Text } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import { BemImage } from 'features/components/images';

import PrivayBagde from '../assets/privacy@2x.png';

export const PrivacySection: FC = () => {
  return (
    <Flex
      bg="secondary.mid-dark"
      p={['52px 37px 47px 42px', '52px 37px 47px 42px', '62px 72px 56px 72px']}
    >
      <Stack
        borderRadius="8px"
        p="37px"
        bg="white"
        align="center"
        direction={['column', 'column', 'row']}
        spacing={['20px', '30px', '30px']}
      >
        <Box
          flexShrink={0}
          sx={{
            width: '224px',
            height: '224px',
            position: 'relative',
          }}
        >
          <BemImage
            width="224px"
            height="224px"
            src={PrivayBagde}
            alt="Bagde de privacidade"
          />
        </Box>

        <Box>
          <CustomHeading
            textAlign={['center', 'center', 'start']}
            color="secondary.mid-dark"
            textStyle="bold32"
            mb="37px"
          >
            Mas por que preciso me preocupar com privacidade?
          </CustomHeading>

          <Text
            color="secondary.mid-dark"
            textAlign={['center', 'center', 'start']}
          >
            A privacidade diz respeito ao seu direito de manter suas informações
            reservadas ou confidenciais, com acesso somente a quem você permita.
            Uma pessoa (ou empresa) com acesso às suas informações sem a sua
            autorização ou até contra a sua vontade poderia utilizá-las de
            maneira inadequada ou de forma prejudicial.
          </Text>
        </Box>
      </Stack>
    </Flex>
  );
};
