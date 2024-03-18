import { Story } from '@storybook/react';

import { FileFormat } from './file-formats.enum';
import { FileInput, FileInputProps } from './file-input';

export default {
  title: 'Inputs/File Input',
  component: FileInput,
};

export const Area: Story<FileInputProps> = (args) => <FileInput {...args} />;
Area.args = {
  info: 'Selecione arquivos para subir aqui',
};

export const Formats = Area.bind({});
Formats.args = {
  info: 'Só aceito imagens (a menos que eu esteja sendo exibido em um Macbook...)',
  formats: [FileFormat.AnyImage],
};
