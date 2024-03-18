import { FC, useState } from 'react';

import { Button, Flex, FlexProps, Icon } from '@chakra-ui/react';

import { ArrowUpIcon } from '@pcf/design-system-icons';

export interface ExpandableSectionProps {
  title: string | FC | React.ReactElement;
  fullWidthTitle?: boolean;
  renderExpanded?: boolean;
  flexProps?: FlexProps;
  children?: React.ReactElement | FC | string;
}

export const ExpandableSection: FC<ExpandableSectionProps> = ({
  children,
  title,
  fullWidthTitle = false,
  renderExpanded = false,
  flexProps = {},
}) => {
  const [showMore, toggleShowMore] = useState(renderExpanded);

  return (
    <Flex direction="column" marginTop="24px" {...flexProps}>
      <Button
        variant="link"
        onClick={() => toggleShowMore(!showMore)}
        colorScheme="secondary"
        {...(fullWidthTitle
          ? {
              justifyContent: 'space-between',
              flexDirection: 'row-reverse',
              width: '100%',
            }
          : { width: 'fit-content' })}
      >
        <Icon
          as={ArrowUpIcon}
          width={3}
          height={3}
          transform={!showMore ? 'rotate(180deg)' : 'rotate(0deg)'}
          transition="transform .25s"
          marginRight="8px"
        />

        {title}
      </Button>

      <Flex
        marginTop={3}
        marginX={-3}
        padding={3}
        opacity={showMore ? 1 : 0}
        height={showMore ? 'fit-content' : 0}
        transition="all .25s"
        overflow="hidden"
      >
        {children}
      </Flex>
    </Flex>
  );
};
