import { FC } from 'react';

import { BemErrorBoundary } from '../error/bem-error-boundary';
import {
  FullLayoutCard,
  FullLayoutCardProps,
} from '../full-layout-card/full-layout-card';

export interface PageLayoutContentProps extends FullLayoutCardProps {
  hasErrorBoundary?: boolean;
}

export const PageLayoutContent: FC<PageLayoutContentProps> = ({
  children,
  hasErrorBoundary = false,
  ...props
}) => {
  const content = hasErrorBoundary ? (
    <BemErrorBoundary>{children}</BemErrorBoundary>
  ) : (
    children
  );

  return <FullLayoutCard {...props}>{content}</FullLayoutCard>;
};
