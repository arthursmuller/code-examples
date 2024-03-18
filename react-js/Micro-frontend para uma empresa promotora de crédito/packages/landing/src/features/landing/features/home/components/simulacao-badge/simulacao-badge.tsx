import { FC, useRef } from 'react';

import {
  Box,
  Flex,
  SystemStyleObject,
  CloseButton,
  Button,
} from '@chakra-ui/react';
import { useWindowSize, useToggle, useScroll } from 'react-use';
import { keyframes } from '@emotion/react';

import { useLandingContext } from 'features/landing/landing.context';
import { zIndexes, CustomHeading } from '@pcf/design-system';
import { usePageContext } from 'features/components/page/page.context';

const moveLeftToRight = keyframes`
 0% {
    opacity: 0;
    transform:translateX(-271px);
  }
 100% {
    opacity: 1;
    transform:translateX(0);
 }
`;

const moveBottomToUp = keyframes`
 0% {
    opacity: 0;
    transform:translateY(112px);
  }
 100% {
    opacity: 1;
    transform:translateY(0);
 }
`;

const getDesktopStyles = (isToFadeIn: boolean): SystemStyleObject => ({
  opacity: isToFadeIn ? 1 : 0,
  position: 'fixed',
  zIndex: zIndexes.fixedElements,
  left: 0,
  bottom: '140px',

  width: '271px',
  animation: isToFadeIn ? `.5s ${moveLeftToRight} ease-in` : '',

  bg: 'white',
  boxShadow: 'card',

  borderRadius: '16px',
  borderTopLeftRadius: 0,
  borderBottomLeftRadius: 0,

  p: '13px 13px 16px 30px',
});

const getMobileStyles = (isToFadeIn: boolean): SystemStyleObject => ({
  display: isToFadeIn ? '' : 'none',

  position: 'fixed',
  zIndex: zIndexes.fixedElements,
  bottom: 4,

  animation: isToFadeIn ? `.5s ${moveBottomToUp} ease-in` : '',

  mx: 'auto',
  width: 'fit-content',
  left: 0,
  right: 0,

  bg: 'white',
  boxShadow: '0px 17px 52px rgb(0 0 0 / 98%)',

  '@media screen and (max-width: 360px)': {
    h4: {
      fontSize: '0.80em',
    },
  },

  borderRadius: '8px',

  p: '15px 24px',
});

export const FixedBagdeSimulacao: FC = () => {
  const { onShowSimulador, showSimulador } = useLandingContext();
  const [open, toogle] = useToggle(true);
  const { pageRef } = usePageContext();
  const { width } = useWindowSize();
  const scrollPos = useScroll(pageRef ?? useRef(null)); // TODO: MOCK

  const showFixedInfoY = scrollPos.y > 500;
  const isToFadeIn = showFixedInfoY && !showSimulador && open;
  const showMobile = width < 500;

  return (
    <Box
      pointerEvents={isToFadeIn ? 'inherit' : 'none'}
      sx={
        showMobile ? getMobileStyles(isToFadeIn) : getDesktopStyles(isToFadeIn)
      }
    >
      <Flex flexDirection="row" justifyContent="flex-start">
        <CustomHeading as="h4" textStyle="bold16_20" color="primary.regular">
          Venha realizar seu sonho com a gente!
        </CustomHeading>

        <CloseButton onClick={toogle} size="sm" color="black" />
      </Flex>

      <Box mt="11px" width={['100%', '180px']}>
        <Button isFullWidth onClick={onShowSimulador}>
          Fazer Simulação
        </Button>
      </Box>
    </Box>
  );
};
