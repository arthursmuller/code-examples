import { FC } from 'react';

import { Icon, Text, Button, ButtonProps } from '@chakra-ui/react';

import { PlusIcon } from '@pcf/design-system-icons';

export interface AddButtonProps extends ButtonProps {
  text: string;
}

export const AddButton: FC<AddButtonProps> = ({
  onClick,
  text,
  ...otherButtonProps
}) => {
  return (
    <Button
      mt={6}
      colorScheme="grey"
      flexDir="column"
      w="310px"
      h="145px"
      layerStyle="card"
      onClick={onClick}
      {...otherButtonProps}
    >
      <Icon as={PlusIcon} color="primary.regular" w="36px" h="36px" />
      <Text textStyle="regular16" mt={6} color="primary.regular">
        {text}
      </Text>
    </Button>
  );
};
