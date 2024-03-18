import { useEffect, useLayoutEffect } from 'react';

import { useHistory } from 'react-router-dom';

import { useNavigatePathUp } from 'hooks';
import { usePageContext } from 'components/page/page.context';

export type RouteOpt = {
  title: string;
  route: string;
  disabled?: boolean;
};

export const useSubRouteMenu: <T extends RouteOpt>(
  title: string,
  opts?: T[],
) => void = <T extends RouteOpt>(title: string, opts?: T[]) => {
  const navigateUp = useNavigatePathUp();
  const history = useHistory();

  const { setMenuBarConfig } = usePageContext();

  const setMenu = (): void =>
    setMenuBarConfig &&
    setMenuBarConfig({
      menuBarColor: 'secondary.regular',
      contextMenuBarColor: 'secondary.regular',
      contextMenuTitle:
        opts?.find((opt) => opt.route === history.location.pathname)?.title ||
        title,
      contextMenuCallback: navigateUp,
      fixedMenuScrollOffset: 175,
    });

  useLayoutEffect(setMenu, []);
  useEffect(setMenu, [history?.location?.pathname]); //eslint-disable-line
};
