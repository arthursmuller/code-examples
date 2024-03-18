import { FC } from 'react';

import { Flex, Grid } from '@chakra-ui/react';

import { ButtonCard, useStepsContainerContext } from '@pcf/design-system';
import { CameraIcon, ImageIcon } from '@pcf/design-system-icons';

import { UploadPersonalPhotoModel } from './upload-personal-photo.model';

export const PhotoTypeStep: FC = () => {
  const { nextStep } = useStepsContainerContext<UploadPersonalPhotoModel>();

  return (
    <Flex direction="column" minH="50vh" justifyContent="space-between">
      <Grid templateColumns="repeat(1, 1fr)" gap={4}>
        <ButtonCard
          iconProps={{ color: 'secondary.regular', w: '33px', h: '27px' }}
          colorScheme="primary"
          title="Tirar uma nova foto"
          icon={CameraIcon}
          onClick={() => nextStep({ uploadType: 'webcam' })}
        />

        <ButtonCard
          iconProps={{ color: 'secondary.regular', w: '32px', h: '32px' }}
          colorScheme="primary"
          title="Fazer upload de uma foto"
          icon={ImageIcon}
          onClick={() => nextStep({ uploadType: 'upload' })}
        />
      </Grid>
    </Flex>
  );
};
