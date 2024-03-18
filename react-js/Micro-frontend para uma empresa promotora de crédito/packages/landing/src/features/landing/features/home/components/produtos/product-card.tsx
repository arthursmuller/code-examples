import { HTMLAttributes, FC } from 'react';

import { Flex, Text, Button, Link as ChakraLink } from '@chakra-ui/react';
import Link from 'next/link';

import { CustomHeading } from '@pcf/design-system';

interface ProductCardProps extends HTMLAttributes<HTMLElement> {
  title: string;
  text: string;
  route: string;
  isExternal: boolean;
}

const ProductCard: FC<ProductCardProps> = ({
  title,
  text,
  route,
}: ProductCardProps) => (
  <Flex
    layerStyle="cardProduct"
    flexDirection="column"
    justifyContent="center"
    maxWidth={['80vw', '85vw', '400px']}
    height="100%"
    mb="20px"
    mr="16px"
    ml="16px"
  >
    <CustomHeading
      as="h2"
      textStyle="bold24"
      color="primary.regular"
      textAlign="center"
    >
      {title}
    </CustomHeading>

    <Text
      as="p"
      textStyle="regular20"
      color="grey.700"
      mt="26px"
      textAlign="center"
      dangerouslySetInnerHTML={{ __html: text }}
      flex={1}
    />

    <Flex mt="24px" justifyContent="center">
      <Button colorScheme="secondary">
        <Link href={route}>Saiba mais</Link>
      </Button>
    </Flex>
  </Flex>
);

export default ProductCard;
