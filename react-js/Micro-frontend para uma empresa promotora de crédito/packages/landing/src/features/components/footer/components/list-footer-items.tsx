import { FC } from 'react';

import NextLink from 'next/link';
import {
  UnorderedList,
  ListItem,
  Flex,
  Text,
  Link,
  Icon,
} from '@chakra-ui/react';

import { ExternallinkIcon } from '@pcf/design-system-icons';

export interface FooterItem {
  id: number;
  text?: string;
  link?: string;
  isInternalLink?: boolean;
  content?: React.ReactElement;
}

export interface ListFooterItem {
  title?: string;
  icon?: React.ReactElement;
  items: FooterItem[];
}

export interface ListFooterItemsProps {
  listFooterItem: ListFooterItem;
}

export const ListFooterItems: FC<ListFooterItemsProps> = ({
  listFooterItem,
}) => {
  return (
    <Flex
      flexDir="column"
      alignItems={['center', 'center', 'center', 'flex-start']}
    >
      {listFooterItem?.icon}

      {listFooterItem?.title && (
        <Text
          mt={[4, 4, 4, 1]}
          as="p"
          textStyle="bold16"
          color="primary.light"
          textAlign={['center', 'center', 'center', 'start']}
        >
          {listFooterItem.title}
        </Text>
      )}

      <UnorderedList
        listStyleType="none"
        ml={0}
        textAlign={['center', 'center', 'center', 'start']}
      >
        {listFooterItem.items.map((item) => (
          <ListItem mt={[3, 3, 3, 2]} key={item.id}>
            {item?.link && (
              <>
                {item.isInternalLink ? (
                  <NextLink href={item.link} passHref>
                    {/* missing href warning link chakra-ui */}
                    {/* eslint-disable-next-line */}
                    <Link color="white">{item?.text}</Link>
                  </NextLink>
                ) : (
                  <Link color="white" href={item.link} isExternal>
                    {item?.text}
                    <Icon
                      ml={3}
                      as={ExternallinkIcon}
                      boxSize={3}
                      color="white"
                    />
                  </Link>
                )}
              </>
            )}

            {item.content && item.content}
          </ListItem>
        ))}
      </UnorderedList>
    </Flex>
  );
};
