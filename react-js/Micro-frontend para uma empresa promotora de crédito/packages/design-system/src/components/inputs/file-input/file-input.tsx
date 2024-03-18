import { FC } from 'react';

import { Button, Center, Flex, FlexProps, Text } from '@chakra-ui/react';
import { DropzoneOptions, useDropzone } from 'react-dropzone';

import { FileFormat } from './file-formats.enum';

export interface FileInputProps {
  info: string;
  formats?: FileFormat[];
  maxFiles?: number;
  disabled?: boolean;
  callback?: (files: File[]) => void;
}

interface FileInputContainerProps extends FileInputProps, FlexProps {}

export const FileInput: FC<FileInputContainerProps> = ({
  formats,
  info,
  disabled,
  callback,
  maxFiles,
  ...flexProps
}) => {
  const handleDrop = (files: File[]): void => {
    callback && callback(files);
  };

  const {
    acceptedFiles: selectedFiles,
    getRootProps,
    getInputProps,
  } = useDropzone({
    disabled,
    accept: formats?.join(', '),
    maxFiles,
    multiple: maxFiles > 1,
    onDrop: handleDrop,
  } as DropzoneOptions);

  return (
    <Flex direction="column" alignItems="center" {...flexProps}>
      <Center
        border="1px dashed"
        borderColor="grey.400"
        borderRadius="24px"
        marginBottom="4px"
        flexDirection="column"
        paddingX="52px"
        paddingTop="32px"
        paddingBottom="16px"
        animation=".25s width"
        {...getRootProps({ className: 'dropzone' })}
      >
        <Text textStyle="regular16" marginBottom="4px" textAlign="center">
          {info}
        </Text>

        <Text textStyle="regular16" marginBottom="4px">
          ou
        </Text>

        <Button
          variant="link"
          textStyle="regular16"
          marginBottom="16px"
          textDecoration="underline"
          color="primary.regular"
        >
          Procure-o no seu computador
        </Button>

        {!!selectedFiles?.length && (
          <>
            <Text textStyle="regular16" marginBottom="4px">
              {maxFiles === 1 ? 'Selecionado:' : 'Selecionados'}
            </Text>

            {selectedFiles?.map((file: File, index: number) => (
              <Flex color="primary.regular" key={`${index + 1}-${file.name}`}>
                {file.name} - {file.size / 1000} kbytes{' '}
              </Flex>
            ))}
          </>
        )}

        <input {...getInputProps()} />
      </Center>

      <Text textStyle="regular12" marginBottom="16px">
        Formatos aceitos: {formats?.join(', ') || 'qualquer formato'}
      </Text>
    </Flex>
  );
};
