import { FC } from 'react';

import { PageLayout } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

export interface ConfigLayoutMobileProps {
  title: string;
}

export const ConfigLayoutMobile: FC<ConfigLayoutMobileProps> = ({
  children,
  title,
}) => {
  const navigateUp = useNavigatePathUp();

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>{title}</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>{children}</PageLayout.Content>
    </PageLayout>
  );
};

export { ConfigLayoutMobile as default };
