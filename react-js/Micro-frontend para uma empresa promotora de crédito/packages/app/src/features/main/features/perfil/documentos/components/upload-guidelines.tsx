import { FC } from 'react';

import { Divider, Flex, FlexProps, Grid } from '@chakra-ui/react';

import { CustomHeading, GuidelineInfo } from '@pcf/design-system';
import {
  BoneIcon,
  LightIcon,
  GlassesIcon,
  RgBrilhoIcon,
  RgFrontIcon,
  SelfieIcon,
} from '@pcf/design-system-icons';

export const DocumentsGuidelines: FC<FlexProps> = (props) => {
  return (
    <Flex {...props} marginBottom={3}>
      <Flex direction="column" overflowY="auto" paddingRight={2}>
        <Flex
          direction="column"
          height="fit-content"
          paddingX={10}
          paddingY={10}
          color="grey.100"
          maxW="400px"
          border="1px solid"
          borderColor="grey.100"
          borderRadius="24px"
        >
          <CustomHeading
            as="h3"
            textStyle="regular16_20"
            marginBottom="8px"
            color="grey.100"
          >
            Dicas para você tirar uma boa selfie
          </CustomHeading>

          <Grid gridTemplateColumns="1fr 1fr" gap="16px">
            <GuidelineInfo
              information="Procure um ambiente bem iluminado."
              icon={LightIcon}
              colorScheme="white"
              align="start"
            />
            <GuidelineInfo
              information="Posicione o celular na altura dos olhos."
              icon={SelfieIcon}
              colorScheme="white"
              align="start"
            />
            <GuidelineInfo
              information="Retire qualquer tipo de chapéu ou boné."
              icon={BoneIcon}
              colorScheme="white"
              align="start"
            />
            <GuidelineInfo
              information="Retire seus óculos de grau ou de sol."
              icon={GlassesIcon}
              colorScheme="white"
              align="start"
            />
          </Grid>

          <Divider borderColor="secondary.regular" marginY="24px" />

          <CustomHeading
            as="h3"
            textStyle="regular16_20"
            marginBottom="8px"
            color="grey.100"
          >
            Dicas para você tirar fotos dos seus documentos
          </CustomHeading>

          <Grid gridTemplateColumns="1fr 1fr" gap="16px">
            <GuidelineInfo
              information="Os documentos devem aparecer inteiros na foto."
              icon={RgFrontIcon}
              colorScheme="white"
              align="start"
            />
            <GuidelineInfo
              information="Evite tirar foto sobre fundos estampados e com reflexos."
              icon={RgBrilhoIcon}
              colorScheme="white"
              align="start"
            />
          </Grid>
        </Flex>
      </Flex>
    </Flex>
  );
};
