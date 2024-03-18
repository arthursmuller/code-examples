import { FC, useState } from 'react';

import { Button, Flex, Icon, Text } from '@chakra-ui/react';

import {
  CustomHeading,
  FileFormat,
  FileInput,
  useStepsContainerContext,
} from '@pcf/design-system';
import { PasswordIcon } from '@pcf/design-system-icons';

import { UploadDocumentForm } from './models/upload-document-form.model';

export interface AttachFilesFormProps {
  maxFiles: number;
  info: string;
  onlyPictures?: boolean;
  callback?: (files: File[]) => void;
}

export const AttachFilesForm: FC<AttachFilesFormProps> = ({
  onlyPictures = false,
  ...otherProps
}) => {
  return (
    <Flex direction="column" alignItems="center">
      <CustomHeading
        as="h2"
        textStyle="bold24_32"
        textAlign="center"
        marginBottom="16px"
        color="secondary.mid-dark"
      >
        Anexar documento
      </CustomHeading>

      <FileInput
        {...otherProps}
        formats={
          onlyPictures
            ? [FileFormat.OnlyPictures]
            : [FileFormat.AnyImage, FileFormat.PDF]
        }
        marginBottom="48px"
      />

      <Flex>
        <Icon as={PasswordIcon} height="15px" width="15px" marginRight="8px" />

        <Text textStyle="regular12" textAlign="center" marginBottom="24px">
          Não se preocupe, seu documento está seguro e não será mostrado a
          ninguém.
        </Text>
      </Flex>
    </Flex>
  );
};

export interface AttachmFilesStepProps {
  maxFiles: number;
  info: string;
  onlyPictures?: boolean;
}

export const AttachFilesStep: FC<AttachmFilesStepProps> = (props) => {
  const { nextStep } = useStepsContainerContext<UploadDocumentForm>();
  const [upload, setUpload] = useState<File[]>();

  return (
    <Flex direction="column">
      <AttachFilesForm callback={setUpload} {...props} />

      <Button
        onClick={() => nextStep({ files: upload })}
        disabled={!upload}
        marginBottom="24px"
      >
        Prosseguir
      </Button>
    </Flex>
  );
};
