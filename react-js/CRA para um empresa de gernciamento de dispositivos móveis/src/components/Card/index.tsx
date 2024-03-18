import { Box as BoxChakra, BoxProps } from '@chakra-ui/react';

interface CardProps extends BoxProps {
  children: React.ReactNode;
}

const Card = ({ children, ...rest }: CardProps) => (
  <BoxChakra
    bg="white"
    width="90%"
    d="flex"
    flexDirection="column"
    borderRadius="10px"
    p="1.6% 1.3%"
    {...rest}
    minWidth="0"
  >
    {children}
  </BoxChakra>
);

export default Card;
