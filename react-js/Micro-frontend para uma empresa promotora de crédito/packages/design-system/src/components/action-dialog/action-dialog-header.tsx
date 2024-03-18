import { FC } from 'react';

import {
  Icon,
  IconButton,
  useBreakpointValue,
  Text,
  Grid,
} from '@chakra-ui/react';

import { StatusCloseErrorIcon } from '@pcf/design-system-icons';

interface ActionDialogCloseButtonProps {
  onClose: () => void;
}

export const ActionDialogCloseButton: FC<ActionDialogCloseButtonProps> = ({
  onClose,
}) => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');

  return (
    <IconButton
      aria-label="close"
      variant="ghost"
      size="sm"
      isRound
      marginTop={-1}
      onClick={onClose}
      _hover={{
        background: 'none',
      }}
      _active={{
        background: 'none',
      }}
      icon={
        <Icon
          as={StatusCloseErrorIcon}
          fill={isDesktop ? 'secondary.regular' : 'grey.100'}
        />
      }
    />
  );
};

interface ActionDialogHeaderProps extends ActionDialogCloseButtonProps {
  title?: string;
  hasCancel?: boolean;
}

export const ActionDialogHeader: FC<ActionDialogHeaderProps> = ({
  title,
  onClose,
  hasCancel = true,
}) => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');

  return (
    <Grid gridTemplateColumns="32px 1fr 32px" paddingX="6" pb={2} width="100%">
      <span />

      <Text
        textStyle="bold20"
        textAlign="center"
        color={isDesktop ? 'secondary.mid-dark' : 'grey.100'}
        fontWeight="bold"
        paddingBottom="16px"
        fontSize="16px"
      >
        {title}
      </Text>

      {hasCancel ? <ActionDialogCloseButton onClose={onClose} /> : <span />}
    </Grid>
  );
};
