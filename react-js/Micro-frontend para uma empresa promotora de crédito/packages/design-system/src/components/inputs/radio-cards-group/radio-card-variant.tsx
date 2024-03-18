import { FC } from 'react';

import { Checkbox, Flex } from '@chakra-ui/react';

import { RadioCardLogic, RadioCardLogicProps } from './radio-card-logic';

export interface RadioCardVariantProps extends RadioCardLogicProps {
  footer?: React.ReactElement;
  content: React.ReactElement;
  header: React.ReactElement;
  asDisplay?: boolean;
}

export const RadioCardVariant: FC<RadioCardVariantProps> = ({
  header,
  content,
  footer,
  asDisplay,
  ...props
}) => {
  return (
    <RadioCardLogic
      {...props}
      render={({ isChecked }) => (
        <Flex direction="column" width="100%" height="100%" borderRadius="8px">
          <Flex
            borderTopRadius="8px"
            className="card-header"
            padding="16px"
            backgroundColor={isChecked ? 'secondary.regular' : 'grey.600'}
          >
            {!asDisplay && (
              <Checkbox
                isChecked={isChecked}
                pointerEvents="none"
                size="sm"
                color={isChecked ? 'inherit' : 'grey.800'}
                marginRight="8px"
              />
            )}

            {header}
          </Flex>

          <Flex
            className="card-content"
            padding="16px"
            flex={1}
            direction="column"
          >
            {content}
          </Flex>

          <Flex
            className="card-footer"
            backgroundColor="grey.200"
            direction="column"
          >
            {footer}
          </Flex>
        </Flex>
      )}
    />
  );
};
