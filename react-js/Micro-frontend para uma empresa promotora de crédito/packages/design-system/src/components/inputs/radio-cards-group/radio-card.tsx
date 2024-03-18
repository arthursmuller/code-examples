import { FC, ReactElement } from 'react';

import {
  Flex,
  Text,
  Checkbox,
  Divider,
  SystemProps,
  Radio,
} from '@chakra-ui/react';

import { RadioCardLogic, RadioCardLogicProps } from './radio-card-logic';

export interface RadioCardProps extends RadioCardLogicProps {
  title?: string;
  subtitle?: string;
  checkboxLabel?: string;
  checkedCheckboxLabel?: string;
  containerDirection?: SystemProps['flexDirection'];
  information?: string;

  customContent?: FC<{ isChecked?: boolean }> | ReactElement;
  customFooter?: FC | ReactElement;
}

const defaultContainerDirection: SystemProps['flexDirection'] = [
  'row-reverse',
  'row-reverse',
  'row',
  'row',
];

export const RadioCard: FC<RadioCardProps> = ({
  title,
  subtitle,
  checkboxLabel,
  checkedCheckboxLabel,
  information,
  containerDirection = defaultContainerDirection,
  customContent,
  customFooter,
  ...props
}) => {
  return (
    <RadioCardLogic
      {...props}
      render={({ isChecked, isRadio }) => {
        return (
          <Flex
            direction="column"
            padding="16px"
            paddingBottom={!customFooter ? '16px' : '8px'}
            width="100%"
            color={isChecked ? 'white' : 'secondary.regular'}
            height="100%"
            backgroundColor={isChecked ? 'secondary.regular' : 'inherit'}
            borderRadius="8px"
          >
            {subtitle && (
              <Text
                textStyle="bold12"
                color={isChecked ? 'inherit' : 'primary.regular'}
                mb="8px"
                ml={[0, 0, '41px']}
              >
                {subtitle}
              </Text>
            )}

            <Flex direction={containerDirection}>
              <Flex align="start" pt={['0', '0', '4px']}>
                {!isRadio ? (
                  <Checkbox
                    isChecked={isChecked}
                    pointerEvents="none"
                    size="sm"
                    color={isChecked ? 'inherit' : 'grey.800'}
                  >
                    {isChecked ? checkedCheckboxLabel : checkboxLabel}
                  </Checkbox>
                ) : (
                  <Radio
                    isChecked={isChecked}
                    pointerEvents="none"
                    size="md"
                    color={isChecked ? 'inherit' : 'grey.800'}
                  >
                    {isChecked ? checkedCheckboxLabel : checkboxLabel}
                  </Radio>
                )}
              </Flex>

              {!customContent ? (
                <Flex direction="column" pl={[0, 0, '16px']} flexGrow={1}>
                  <Text as="h3" textStyle="bold20" color="inherit">
                    {title}
                  </Text>

                  <Text textStyle="regular14" pt="8px" color="inherit">
                    {information}
                  </Text>
                </Flex>
              ) : (
                (typeof customContent === 'function' &&
                  customContent({ isChecked })) ||
                customContent
              )}
            </Flex>

            {customFooter && (
              <>
                <Divider
                  color="secondary.washed"
                  my="8px"
                  ml={[0, 0, '-16px']}
                  w={['100%', '100%', 'calc(100% + 32px)']}
                />
                {customFooter}
              </>
            )}
          </Flex>
        );
      }}
    />
  );
};
