import { FC } from 'react';

import {
  Flex,
  Center,
  Button,
  Box,
  useBreakpointValue,
} from '@chakra-ui/react';

import { useProdutos } from '@pcf/core';
import { CustomHeading } from '@pcf/design-system';
import { CapturaLeadSection } from 'features/landing/components/captura-lead-section';
import { useLandingContext } from 'features/landing/landing.context';
import Mockup from 'features/landing/assets/mockup@2x.png';
import { BemImage } from 'features/components/images';

const PhoneMockup: FC<{
  w?: string;
  h?: string;
  deg?: number;
  direction: 'left' | 'right';
  top?: string;
  left?: string;
}> = ({ direction, top = '0', left = '0', w, h, deg: degProp }) => {
  const deg = degProp || (direction === 'left' ? -12 : 15);

  return (
    <Box position="absolute" top={top} left={left}>
      <Box
        sx={{
          zIndex: 9,
          marginTop: '-30px',
          width: w || ['235px', '235px', '284px', '284px'],
          height: h || ['389px', '389px', '471px', '471px'],
          transform: `rotate(${deg}deg)`,
          mt: '-68px',
        }}
      >
        <BemImage
          src={Mockup}
          alt="Celular com site da Bem Promotora exibido na tela"
          width={w || ['235px', '235px', '284px', '284px']}
          height={h || ['389px', '389px', '471px', '471px']}
        />
      </Box>
    </Box>
  );
};

export const SimulateBanner: FC<{ title?: string; product: string }> = ({
  title,
  product,
}) => {
  const { onShowSimulador } = useLandingContext();
  const isMobile = useBreakpointValue({ base: true, md: false });

  const { data, isLoading } = useProdutos();

  return (
    <>
      <Flex
        height={['330px', '330px', '380px']}
        background="secondary.mid-dark"
        paddingY={10}
        paddingX={6}
        direction="column"
      >
        {!isMobile ? (
          <Center flexGrow={1}>
            <Flex
              direction="column"
              layerStyle="card"
              padding="48px"
              paddingLeft="80px"
              position="relative"
            >
              <PhoneMockup direction="left" top="-75px" left="-260px" />

              <CustomHeading
                textStyle="bold40_48"
                color="secondary.mid-dark"
                marginBottom={10}
              >
                {title || 'Crédito Consignado'}
              </CustomHeading>

              <Flex>
                <Button
                  onClick={onShowSimulador}
                  disabled={!data}
                  isLoading={isLoading}
                >
                  Simule sua proposta
                </Button>
              </Flex>
            </Flex>
          </Center>
        ) : (
          <Flex direction="column" position="relative">
            <CustomHeading
              textStyle="bold40_48"
              color="white"
              marginBottom={10}
              maxWidth="320px"
            >
              Crédito Consignado
            </CustomHeading>

            <Flex>
              <Button onClick={onShowSimulador} isLoading={isLoading}>
                Fazer Simulação
              </Button>
            </Flex>

            <PhoneMockup
              w="400px"
              h="346px"
              deg={10}
              direction="right"
              top="30px"
              left="100px"
            />
          </Flex>
        )}
      </Flex>

      {!isLoading && !!data && (
        <CapturaLeadSection
          simpleSteps
          product={data.find((d) => d.sigla === product)}
        />
      )}
    </>
  );
};
