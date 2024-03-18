import { FC } from 'react';

import { Button, ButtonProps } from '@chakra-ui/react';

import { Container } from './burger-button.styles';

export interface BurgerButtonProps extends ButtonProps {
  expanded?: boolean;
}

export const BurgerButton: FC<BurgerButtonProps> = ({
  expanded,
  onClick,
  color,
  ...buttonProps
}: BurgerButtonProps) => {
  return (
    <Button
      height="45px"
      variant="icon"
      onClick={onClick}
      {...buttonProps}
      paddingX={0}
      _focus={{
        outline: 'none',
      }}
    >
      <Container expanded={expanded || false} color={color as string}>
        <div className="one" />
        <div className="two" />
        <div className="three" />
      </Container>
    </Button>
  );
};
