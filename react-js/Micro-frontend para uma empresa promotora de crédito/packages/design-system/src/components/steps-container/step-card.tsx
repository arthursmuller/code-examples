import { FC } from 'react';

import { Flex, Text } from '@chakra-ui/react';

import { rightToLeft } from '../../animations/horizontal-fade-in';
import { CustomHeading } from '../custom-heading';

type TextAlign = 'start' | 'center';

export interface StepCardProps {
  title?: string;
  subTitle?: string;
  information?: string;
  textAlign?: TextAlign | TextAlign[];
  customTop?: React.ReactNode;
}

export const StepCard: FC<StepCardProps> = ({
  title,
  subTitle,
  information,
  children,
  textAlign = 'center',
  customTop,
}) => {
  return (
    <Flex
      flexDir="column"
      layerStyle="card"
      p={!customTop ? '30px 24px 32px 24px' : '48px 24px 32px 24px'}
      mx="6"
      sx={{
        animation: `250ms ${rightToLeft} ease-in-out`,
      }}
    >
      {customTop && customTop}
      {title && (
        <CustomHeading
          textStyle={!customTop ? 'bold40_48' : 'bold32_40'}
          color="secondary.mid-dark"
          textAlign={textAlign}
          mx={textAlign === 'center' ? { xl: '30px' } : {}}
          mb="15px"
        >
          {title}
        </CustomHeading>
      )}

      {subTitle && (
        <Text as="p" textStyle="bold20" textAlign={textAlign}>
          {subTitle}
        </Text>
      )}

      {information && (
        <Text
          as="p"
          textStyle="regular16"
          mt="2px"
          textAlign={textAlign}
          mx={['10px', '10px', '50px', '50px']}
        >
          {information}
        </Text>
      )}

      {children}
    </Flex>
  );
};
