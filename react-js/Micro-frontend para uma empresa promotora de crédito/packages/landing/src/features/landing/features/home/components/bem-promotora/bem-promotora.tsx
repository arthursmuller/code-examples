import React, { FC } from 'react';

import {
  Flex,
  Box,
  Text,
  Button,
  Icon,
  useBreakpointValue,
} from '@chakra-ui/react';
import Link from 'next/link';

import { CustomHeading } from '@pcf/design-system';
import { ArrowOutlineRightIcon } from '@pcf/design-system-icons';
import { RoutesEnum } from 'app/routes/routes.enum';
import { BemImage } from 'features/components/images';

import Idosa from '../../assets/idosa.jpg';

export const BemPromotora: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <Flex
      as="section"
      width="100%"
      height={['750px', '700px', '560px', '560px']}
      position="relative"
    >
      <Box
        position="relative"
        width="100%"
        height={['365px', '560px', '560px', '560px']}
      >
        <BemImage
          src={Idosa}
          alt="Mulher sentada, apoiando seus braços em uma bengala."
          position={isMobile ? '-100px' : 'left'}
          zIndex="-1"
        />
      </Box>

      <Box
        position="absolute"
        top={['unset', 'unset', '0']}
        bottom="0"
        right="0"
        margin="auto"
        width={['95%', '90%', '50%', '60%']}
        height="fit-content"
        layerStyle="card-without-border-radius-right"
        pl="40px"
        color="secondary.mid-dark"
      >
        <CustomHeading
          as="h2"
          textStyle={['bold32', 'bold32', 'bold48']}
          pt="24px"
        >
          A Bem Promotora
        </CustomHeading>

        <Text mt="26px" textStyle="regular20_24" color="grey.700">
          Mais do que prestar serviços financeiros com excelência, buscamos
          promover o bem-estar das pessoas acima de tudo, ajudando você a
          realizar seus sonhos!
        </Text>

        <CustomHeading as="h3" textStyle="bold20" mt="20px" color="grey.700">
          Isso é Bem o nosso jeito de ser. Isso é Bem Promotora.
        </CustomHeading>

        <Box mt="36px" mb="16px">
          <Link href={RoutesEnum.About}>
            <Button
              variant="link"
              rightIcon={
                <Icon
                  display="flex"
                  ml={3}
                  w="7px"
                  h="12px"
                  as={ArrowOutlineRightIcon}
                />
              }
              textStyle="bold14"
              color="primary.regular"
            >
              Saiba mais sobre a Bem{isMobile === true && ' Promotora'}
            </Button>
          </Link>
        </Box>
      </Box>
    </Flex>
  );
};
