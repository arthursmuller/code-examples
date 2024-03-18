import { FC } from 'react';

import {
  Flex,
  Text,
  Box,
  useBreakpointValue,
  Link,
  Icon,
  Center,
} from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import { ExternallinkIcon } from '@pcf/design-system-icons';
import { BemImage } from 'features/components/images';

import QRCode from '../assets/banrisul-card-qr-code.jpeg';

// const MyImage = chakra(NextImage, {
//   baseStyle: { maxH: 120, maxW: 120 },
//   shouldForwardProp: (prop) => ['width', 'height', 'src', 'alt'].includes(prop),
// });

export const BanrisulCardBanner: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: true, lg: false });

  return (
    <Center
      background="primary.gradient"
      paddingY={['32px', '32px', '48px']}
      direction="row"
      justifyContent={!isMobile ? 'space-around' : 'start'}
      marginBottom={[8, 8, 12]}
      minH={['220px', '220px', '220px', '520px']}
    >
      <Box w={['100%', '100%', '100%', '40%']} p="60px 25px 60px 35px">
        <CustomHeading
          textStyle={isMobile ? 'regular32' : 'regular40'}
          color="white"
        >
          Conheça mais sobre o{' '}
          <Text as="b" color="white">
            nosso produto
          </Text>
        </CustomHeading>

        <Text mt="27px" color="white" textStyle="regular20">
          Consulte o contrato, a cartilha e outros documentos importantes do seu
          Cartão de Crédito Banrisul{' '}
          {!isMobile && (
            <>
              em <br />
            </>
          )}
          <Link
            color="secondary.mid-dark"
            whiteSpace="nowrap"
            textDecoration="underline"
            href="https://www.banrisul.com.br/cartoesdocumentos"
            isExternal
          >
            {isMobile ? 'clicando aqui' : 'banrisul.com.br/cartoesdocumentos'}
            <Icon
              color="secondary.mid-dark"
              ml={3}
              as={ExternallinkIcon}
              boxSize={4}
            />
          </Link>
        </Text>
      </Box>

      {!isMobile && (
        <Flex
          direction="column"
          justifyContent="center"
          alignItems="center"
          w="317px"
        >
          <Text textAlign="center" textStyle="bold20" color="white" mb={4}>
            Ou Aponte a câmera do celular para a imagem abaixo:
          </Text>

          <Box
            position="relative"
            sx={{
              borderRadius: 16,
              minH: '250px',
              minW: '250px',
              maxH: 120,
              maxW: 120,
            }}
          >
            <BemImage src={QRCode} alt="Banrisul Card" />
          </Box>

          <Text color="white" mt={6} textStyle="regular14" textAlign="center">
            Necessário celular com tecnologia compatível a leitura de QR Code.
          </Text>
        </Flex>
      )}
    </Center>
  );
};
