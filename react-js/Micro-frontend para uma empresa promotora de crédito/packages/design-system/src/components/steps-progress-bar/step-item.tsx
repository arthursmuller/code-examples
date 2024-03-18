import { FC } from 'react';

import {
  Flex,
  Icon,
  Text,
  Center,
  useTheme,
  CSSWithMultiValues,
  RecursivePseudo,
  RecursiveCSSSelector,
  FlexProps,
} from '@chakra-ui/react';

import { stepsSizeConfig, stepItemConfig } from './step-item.config';
import { StepStatus } from './step-status.enum';

export type StepStatusSize = 'xs' | 'sm' | 'md';

export interface StepItemData {
  label: string;
  description?: string;
  status: StepStatus;
}
export interface StepItemProps extends StepItemData {
  icon?: React.FC;
  isHorizontal?: boolean;
  size: StepStatusSize;
  showLabels?: boolean;
}

interface ContentProp {
  content?: string;
}

export const StepItem: FC<StepItemProps> = ({
  label,
  status,
  isHorizontal = false,
  size,
  description,
  showLabels = true,
}) => {
  const { space } = useTheme();
  const { bg, iconConfig, color } = stepItemConfig[status];
  const {
    outCircle,
    innerCircle,
    barThickness,
    barTop,
    innerCircleThickness,
    barLeft,
  } = stepsSizeConfig[size];

  let stepItemProps: FlexProps = {};

  let barStyles:
    | CSSWithMultiValues
    | (CSSWithMultiValues & RecursivePseudo<CSSWithMultiValues>)
    | ((CSSWithMultiValues & RecursiveCSSSelector<CSSWithMultiValues>) &
        ContentProp) = {
    content: "''",
    bg,
    position: 'absolute',
  };

  let firstChildStyle:
    | CSSWithMultiValues
    | (CSSWithMultiValues & RecursivePseudo<CSSWithMultiValues>) = {
    alignItems: 'flex-start',
    _after: {
      ...barStyles,
    },
  };

  let lastChildStyle:
    | CSSWithMultiValues
    | (CSSWithMultiValues & RecursivePseudo<CSSWithMultiValues>) = {
    alignItems: 'flex-end',
    _after: {
      ...barStyles,
    },
  };

  if (isHorizontal) {
    stepItemProps = {
      w: '100%',
    };

    barStyles = {
      ...barStyles,
      h: barThickness,
      w: '100%',
      top: barTop,
    };

    firstChildStyle = {
      ...firstChildStyle,
      w: `calc(50% + ${space[outCircle]})`,

      _after: {
        ...firstChildStyle['_after'], // eslint-disable-line
        w: `calc(100% - ${space[outCircle]})`,
        left: `calc(${space[outCircle]} + ${innerCircleThickness})`,
      },
    };

    lastChildStyle = {
      ...lastChildStyle,
      w: `calc(50% + ${space[outCircle]})`,

      _after: {
        ...lastChildStyle['_after'], // eslint-disable-line
        w: `calc(100% - ${space[outCircle]})`,
        right: `calc(${space[outCircle]} + ${innerCircleThickness})`,
      },
    };
  } else {
    stepItemProps = {
      h: 'auto',
      minH: '100%',
      flexGrow: 1,
    };

    barStyles = {
      ...barStyles,
      h: '100%',
      w: barThickness,
      left: barLeft,
    };

    firstChildStyle = {
      ...firstChildStyle,
      _after: {
        ...firstChildStyle['_after'], // eslint-disable-line
        top: '1px',
      },
      flexGrow: 0.4,
      justifyContent: 'flex-start',
    };

    lastChildStyle = {
      ...lastChildStyle,
      _after: {
        ...lastChildStyle['_after'], // eslint-disable-line
        top: '0',
        height: '70%',
      },
      flexGrow: 0.5,
      justifyContent: 'flex-end',
    };
  }

  if (!showLabels) {
    firstChildStyle = {
      ...firstChildStyle,
      paddingLeft: '10px',
    };

    lastChildStyle = {
      ...lastChildStyle,
      paddingRight: '10px',
    };
  }

  return (
    <Flex
      flexDir="column"
      justifyContent="center"
      position="relative"
      alignItems="center"
      _after={barStyles}
      _first={firstChildStyle}
      _last={lastChildStyle}
      {...stepItemProps}
    >
      <Flex
        flexDir={isHorizontal ? 'column' : 'row'}
        justifyContent="center"
        alignItems={isHorizontal ? 'center' : 'flex-start'}
        h="auto"
        minHeight="100%"
        zIndex={1}
      >
        <Center
          bg={bg}
          h={outCircle}
          w={outCircle}
          borderRadius="full"
          flexShrink={0}
        >
          <Center
            borderRadius="full"
            border={`${innerCircleThickness} solid white`}
            h={innerCircle}
            w={innerCircle}
          >
            {iconConfig?.iconComponent && (
              <Icon
                as={iconConfig?.iconComponent}
                color="white"
                w={iconConfig.sizes[size].w}
                h={iconConfig.sizes[size].h}
              />
            )}
          </Center>
        </Center>

        <Flex
          flexDir="column"
          mt={isHorizontal ? 1 : 0}
          ml={isHorizontal ? 0 : 2}
        >
          {showLabels && (
            <Text
              textStyle={isHorizontal ? 'regular12' : 'regular16_20'}
              color={color}
            >
              {label}
            </Text>
          )}

          {description && (
            <Text textStyle="regular14" color="grey.800" mt={2}>
              {description}
            </Text>
          )}
        </Flex>
      </Flex>
    </Flex>
  );
};
