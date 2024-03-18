import { FC } from 'react';

import { Box, Flex, Text, Button, keyframes } from '@chakra-ui/react';

import { useLandingContext } from 'features/landing/landing.context';
import { CustomHeading } from '@pcf/design-system';
import { BemImage } from 'features/components/images';

import LandingImg from '../../assets/idoso.jpg';

const moveLeftToRight = keyframes`
  0% {
    opacity: 0;
    transform: translateX(-10rem);
    }
  80% {
    transform: translateX(1rem);
    }
  100% {
    opacity: 1;
    transform: translate(0);
    }
`;

const moveRightToLeft = keyframes`
  0% {
    opacity: 0;
    transform: translateX(10rem);
    }
  80% {
    transform: translateX(-1rem);
    }
  100% {
    opacity: 1;
    transform: translate(0);
    }
`;

const moveBottomToUp = keyframes`
  0% {
    opacity: 0;
    transform: translateY(3rem);
    }
  100% {
    opacity: 1;
    transform: translate(0);
    }
`;

export const Header: FC = () => {
  const { onShowSimulador } = useLandingContext();

  return (
    <Flex height={['512px', '512px', '720px']} as="header" position="relative">
      <BemImage
        src={LandingImg}
        alt="Homem branco com barba e cabelos brancos, óculos de grau, encostado em um muro e usando um celular com fones de ouvido"
        priority
        position="right"
        zIndex="-1"
      />

      <Flex
        flexDirection="column"
        height={['520px', '720px', '720px', '720px']}
        position="absolute"
      >
        <Flex
          px={['25px', '25px', '71px', '71px']}
          ml={[0, 0, 0, 104]}
          flexDirection="column"
          flexShrink={1}
          flexGrow={1}
          justifyContent="center"
          pt="80px"
        >
          <CustomHeading as="h1">
            <Text
              as="span"
              display="block"
              textStyle="headline"
              color="white"
              sx={{
                animation: `1s ${moveLeftToRight} ease-out`,
              }}
            >
              Crédito Consignado
            </Text>
            <Text
              as="span"
              display="block"
              textStyle="headline"
              color="white"
              sx={{
                animation: `1s ${moveRightToLeft} ease-out`,
              }}
            >
              é na bem promotora
            </Text>
          </CustomHeading>

          <Box position="relative" maxWidth="390px">
            <Box
              mt="27px"
              layerStyle="card"
              w="100%"
              maxWidth="390px"
              pb={[6, 6, 4]}
              sx={{
                position: ['absolute', 'initial', 'initial', 'initial'],
                top: [0, 0, '450px'],
                animation: `${moveBottomToUp} .5s ease-out .75s`,
                animationFillMode: 'backwards',
                '@media screen and (max-width: 360px)': {
                  h3: {
                    fontSize: '1em',
                  },
                  p: {
                    fontSize: '0.80em',
                  },
                },
              }}
            >
              <Text textStyle="bold20" color="secondary.mid-dark">
                BEM FÁCIL, BEM RÁPIDO
              </Text>
              <Text
                as="p"
                textStyle="regular16"
                color="secondary.mid-dark"
                mt="5px"
              >
                Faça uma simulação agora mesmo!
              </Text>
              <Box mt="10px" width={['100%', '264px', '264px', '264px']}>
                <Button
                  colorScheme="secondary"
                  isFullWidth
                  onClick={onShowSimulador}
                >
                  Fazer Simulação
                </Button>
              </Box>
            </Box>
          </Box>
        </Flex>
      </Flex>
    </Flex>
  );
};
