import { FC } from 'react';

import { Box, Flex, Stack, Text, Icon, Center, Link } from '@chakra-ui/react';
import NextLink from 'next/link';

import { BemImage } from 'features/components/images';
import { CustomHeading } from '@pcf/design-system';
import { TabProfileInativaIcon } from '@pcf/design-system-icons';

import HackerImage from '../assets/hacker-man-laptop@2x.jpg';

export const DPOOfficerSection: FC = () => {
  return (
    <Flex justifyContent="center">
      <Stack
        direction={['column', 'column', 'row']}
        spacing={['20px', '30px', '30px']}
        position="relative"
        align={['center', 'center']}
        justify={['center', 'center']}
        justifyContent="center"
        px="38px"
      >
        <Box
          flexShrink={0}
          sx={{
            width: ['100%', '285px', '285px'],
            height: '250px',
            position: 'relative',
          }}
        >
          <BemImage
            width={['100%', '285px', '285px']}
            height="250px"
            src={HackerImage}
            alt="Mãos digitando no notebook"
          />
        </Box>

        <Flex flexDirection="column">
          <CustomHeading textStyle="bold32" color="secondary.regular">
            Quem faz o tratamento dos seus dados - DPO
          </CustomHeading>

          <Text textStyle="regular16" color="grey.700" lineHeight="24px" mt={2}>
            DPO: Data Protection Officer / Encarregado dos Dados
          </Text>

          <Flex mt="48px" justifyContent={['center', 'center', 'flex-start']}>
            <Center
              borderRadius="full"
              boxSize="32px"
              bg="linear-gradient(90deg, #5889FA 0%, #2B3EA1 100%);"
              flexShrink={0}
            >
              <Icon as={TabProfileInativaIcon} width="16px" heigth="16px" />
            </Center>

            <Box ml={4}>
              <NextLink passHref href="mailto:dpo@bempromotora.com.br">
                {/* eslint-disable-next-line */}
                <Link textStyle="bold24" color="primary.regular">
                  Juliano Mirapalheta Sangoi
                </Link>
              </NextLink>

              <Text color="grey.700">dpo@bempromotora.com.br</Text>
            </Box>
          </Flex>
        </Flex>
      </Stack>
    </Flex>
  );
};
