import { FC } from 'react';

import { Button, Flex, Grid, Text } from '@chakra-ui/react';

import { LightIcon, RgBrilhoIcon, RgFrontIcon } from '@pcf/design-system-icons';
import {
  useStepsContainerContext,
  CustomHeading,
  GuidelineInfo,
} from '@pcf/design-system';

export const UploadGuidelines: FC = () => (
  <>
    <CustomHeading
      as="h2"
      textStyle="bold24_32"
      color="secondary.mid-dark"
      textAlign="center"
      marginBottom="8px"
    >
      Dicas para cadastrar seu documento
    </CustomHeading>

    <Text as="p" textStyle="regular14" textAlign="center" marginBottom="24px">
      Use a via original do seu documento. A qualidade da foto pode agilizar a
      sua aprovação!
    </Text>

    <Grid gridTemplateColumns="1fr 1fr" gap="16px" marginBottom="24px">
      <GuidelineInfo
        information="Seus documentos devem aparecer inteiros."
        icon={RgFrontIcon}
        align="center"
      />
      <GuidelineInfo
        information="Evite tirar a foto sobre fundos estampados."
        icon={RgFrontIcon}
        align="center"
        customBg="repeating-linear-gradient(0deg, #E8F1FF, #E8F1FF 10px,  #FFFFFF 10px,  #FFFFFF 20px)"
      />
      <GuidelineInfo
        information="Tire a foto em um ambiente bem iluminado."
        icon={LightIcon}
        align="center"
      />
      <GuidelineInfo
        information="Evite reflexos nos seus documentos."
        icon={RgBrilhoIcon}
        align="center"
      />
    </Grid>
  </>
);

export const UploadGuidelinesStep: FC = () => {
  const { nextStep } = useStepsContainerContext();

  return (
    <Flex direction="column">
      <UploadGuidelines />

      <Button onClick={() => nextStep()}>Entendi!</Button>
    </Flex>
  );
};
