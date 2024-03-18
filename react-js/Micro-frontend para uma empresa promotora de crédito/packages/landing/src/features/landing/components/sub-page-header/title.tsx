import { FC, ReactElement } from 'react';

import { CustomHeading } from '@pcf/design-system';

interface TitleProps {
  title: string | ReactElement;
  width?: string | string[];
}

export const Title: FC<TitleProps> = ({
  title,
  width = ['270px', '270px', '400px'],
}) => (
  <CustomHeading
    as="h1"
    mt={['115px', '30px', '30px']}
    w={width}
    textStyle="headline2"
    color="white"
    whiteSpace="pre-line"
  >
    {title}
  </CustomHeading>
);
