import { ElementType, Suspense } from 'react';

import { useBreakpointValue, PropsOf } from '@chakra-ui/react';

import { Loader } from '../components/loader';

export interface LazyByDevice<AsType> {
  desktop?: AsType;
  mobile?: AsType;
}

type LazyByDeviceFC = <Tag extends ElementType>(
  props: LazyByDevice<Tag> & PropsOf<Tag>,
) => JSX.Element;

export const LazyByDevice: LazyByDeviceFC = ({
  mobile: Mobile,
  desktop: Desktop,
  ...props
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return isMobile !== undefined ? (
    <Suspense fallback={<Loader fullHeight />}>
      {isMobile && Mobile && <Mobile {...props} />}
      {!isMobile && Desktop && <Desktop {...props} />}
    </Suspense>
  ) : (
    <Loader fullHeight />
  );
};
