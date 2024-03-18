import { FC } from 'react';

import { Avatar, AvatarBadge, IconButton, Icon } from '@chakra-ui/react';

import { CameraIcon } from '@pcf/design-system-icons';

import { useUploadPersonalPhoto } from '../features/perfil/documentos/components/upload-personal-photo';
import { useDocumentos } from '../features/perfil/documentos';

export const EditableAvatar: FC = () => {
  const onUpload = useUploadPersonalPhoto();
  const { anexosFotoPessoal } = useDocumentos();

  return (
    <Avatar
      w={['96px', '96px', '104px', '104px']}
      h={['96px', '96px', '104px', '104px']}
      src={anexosFotoPessoal[0]?.linkAnexo}
    >
      <AvatarBadge
        as={IconButton}
        right="6px"
        bottom="8px"
        boxSize="32px"
        bg="grey.600"
        _hover={{
          background: 'grey.600',
          borderColor: 'white',
        }}
        borderColor="grey.600"
        isRound
        onClick={onUpload}
        variant="ghost"
        size="xs"
        aria-label="Editar foto de perfil"
        icon={<Icon color="white" as={CameraIcon} w="18px" h="18px" />}
      />
    </Avatar>
  );
};
