import { FC, ReactElement } from 'react';

import {
  ButtonProps,
  ComponentWithAs,
  Divider,
  Drawer as ChakraDrawer,
  DrawerBody,
  DrawerContent,
  DrawerHeader,
  DrawerOverlay,
  Flex,
  Icon,
  IconButton,
  IconProps,
  useBreakpointValue,
  useDisclosure,
} from '@chakra-ui/react';
import { use100vh } from 'react-div-100vh';

import { CustomHeading } from '../custom-heading';

export interface TitleProps {
  onClick: () => void;
  title: string;
  icon: FC;
  color?: string;
  iconProps?: Partial<IconProps>;
}

interface DrawerCompound {
  Title: FC<TitleProps>;
  Body: typeof DrawerBody;
}

const DrawerTitle: FC<TitleProps> = ({
  onClick,
  icon,
  title,
  color = 'primary.regular',
  iconProps,
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const buttonExtraProps = isMobile
    ? { borderRadius: 'full', colorScheme: 'secondary' }
    : { variant: 'ghost' };

  return (
    <>
      <DrawerHeader boxShadow={isMobile ? 'card' : 'none'} zIndex={1}>
        <Flex align="center">
          <IconButton
            aria-label="close"
            size="sm"
            isRound
            onClick={onClick}
            icon={<Icon as={icon} fill={color} {...(iconProps as any)} />}
            marginRight={4}
            {...buttonExtraProps}
          />

          <CustomHeading textStyle="bold24" color={color} flex={1}>
            {title}
          </CustomHeading>
        </Flex>
      </DrawerHeader>

      {!isMobile && (
        <Divider
          borderColor="grey.500"
          borderBottomWidth="1.5px"
          marginX={6}
          width="calc(100% - 48px)"
        />
      )}
    </>
  );
};

export interface DrawerProps {
  buttonRender: ComponentWithAs<'button', ButtonProps>;
}

export interface DrawerWithContentProps extends DrawerProps {
  content: ({ onClose }: { onClose: () => void }) => ReactElement;
}

const Drawer: FC<DrawerWithContentProps> & DrawerCompound = ({
  buttonRender: ButtonRender,
  content: Content,
}) => {
  const { isOpen, onOpen, onClose } = useDisclosure();

  const height = use100vh();

  return (
    <>
      <ButtonRender onClick={onOpen} />

      <ChakraDrawer
        isOpen={isOpen}
        placement="right"
        onClose={onClose}
        size="md"
      >
        <DrawerOverlay>
          <DrawerContent
            backgroundColor="grey.100"
            display="flex"
            flexDirection="column"
            height={`${height ? `${height}px` : '100%'}`}
          >
            <Content onClose={onClose} />
          </DrawerContent>
        </DrawerOverlay>
      </ChakraDrawer>
    </>
  );
};

Drawer.Title = DrawerTitle;
Drawer.Body = DrawerBody;

export { Drawer };
