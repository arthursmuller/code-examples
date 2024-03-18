import { FC } from 'react';

import {
  Button,
  Center,
  Flex,
  Icon,
  Text,
  useBreakpointValue,
} from '@chakra-ui/react';

import { PasswordIcon } from '@pcf/design-system-icons';
import { useStepsContainerContext, CustomHeading } from '@pcf/design-system';

import { CameraPopover } from './camera-popover';
import { UploadDocumentForm } from './models/upload-document-form.model';

export interface DocumentGuidelinesProps {
  icon: FC;
  title: string;
  subtitle?: string;
  info?: string;
}

export const DocumentGuidelines: FC<DocumentGuidelinesProps> = ({
  icon,
  title,
  subtitle,
  info,
}) => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');

  return (
    <Flex direction="column" alignItems="center">
      <Flex
        direction={isDesktop ? 'row' : 'column'}
        alignItems="center"
        marginBottom={isDesktop ? '24px' : '16px'}
      >
        <Center
          borderRadius="full"
          border="1px solid"
          borderColor="secondary.mid-dark"
          padding="8px"
          width="fit-content"
          marginRight={isDesktop ? '24px' : 0}
          marginBottom={isDesktop ? 0 : '8px'}
        >
          <Icon
            as={icon}
            height="40px"
            width="40px"
            color="secondary.mid-dark"
          />
        </Center>

        <CustomHeading
          as="h2"
          textStyle="bold24_32"
          color="secondary.mid-dark"
          textAlign="center"
        >
          {title}
        </CustomHeading>
      </Flex>

      {subtitle && (
        <Text
          textStyle="regular24"
          textAlign="center"
          marginBottom={isDesktop ? '40px' : '24px'}
        >
          {subtitle}
        </Text>
      )}

      {info && (
        <Text
          textStyle="regular14"
          textAlign="center"
          marginBottom={isDesktop ? '40px' : '24px'}
        >
          {info}
        </Text>
      )}

      {!isDesktop && (
        <>
          <Icon
            as={PasswordIcon}
            height="36px"
            width="36px"
            paddingBottom="8px"
          />

          <Text textStyle="regular12" textAlign="center" marginBottom="24px">
            Não se preocupe, seu documento está seguro e não será mostrado a
            ninguém.
          </Text>
        </>
      )}
    </Flex>
  );
};

export const DocumentGuidelinesStep: FC<DocumentGuidelinesProps> = (props) => {
  const { nextStep } = useStepsContainerContext();

  return (
    <Flex direction="column">
      <DocumentGuidelines {...props} />

      <Button onClick={() => nextStep()}>Continuar</Button>
    </Flex>
  );
};

export interface DocumentGuidelinesWithCameraStepProps
  extends DocumentGuidelinesProps {
  cameraInputTitle: string;
  onSubmit?: (picture: File) => void;
  guideline?: string;
}

export const DocumentGuidelinesWithCameraStep: FC<DocumentGuidelinesWithCameraStepProps> =
  ({ cameraInputTitle, onSubmit, guideline, ...props }) => {
    const { nextStep, finish } = useStepsContainerContext<UploadDocumentForm>();

    return (
      <Flex direction="column">
        <DocumentGuidelines {...props} />

        <CameraPopover
          onSubmit={
            onSubmit || ((picture: File) => nextStep({ files: [picture] }))
          }
          title={cameraInputTitle}
          camera="back"
          guideline={
            guideline ||
            'Posicione a FRENTE INTEIRA do seu documento dentro da moldura'
          }
        />

        <Button variant="link" onClick={finish}>
          Terminar depois
        </Button>
      </Flex>
    );
  };
