import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { BemImage } from 'features/components/images';

import { Title } from './title';
import { Subtitle } from './subtitle';

interface SubPageHeaderComposition {
  Title: typeof Title;
  Subtitle: typeof Subtitle;
}

export interface SubPageHeaderProps {
  backgroundImage: string | StaticImageData;
  backgroundImageAlt: string;
  position?: string[];
  customHeight?: string | string[];
  customPadding?: string | number | string[] | number[];
}

const SubPageHeader: FC<SubPageHeaderProps> & SubPageHeaderComposition = ({
  backgroundImage,
  backgroundImageAlt,
  position = [],
  customHeight = [],
  customPadding,
  children,
}) => (
  <Flex
    height={customHeight.length ? customHeight : '517px'}
    as="header"
    position="relative"
  >
    {backgroundImage && (
      <BemImage
        zIndex="-1"
        alt={backgroundImageAlt}
        position={position}
        priority
        {...(typeof backgroundImage === 'string'
          ? {
              srcPath: backgroundImage.split('.')[0],
              mobileMod: backgroundImage.split('.')[1],
            }
          : {
              src: backgroundImage,
            })}
      />
    )}
    <Flex
      flexDir="column"
      justifyContent="center"
      paddingLeft={customPadding || [6, 6, '74px', '129px']}
      width="100%"
    >
      {children}
    </Flex>
  </Flex>
);

SubPageHeader.Title = Title;
SubPageHeader.Subtitle = Subtitle;

export { SubPageHeader };
