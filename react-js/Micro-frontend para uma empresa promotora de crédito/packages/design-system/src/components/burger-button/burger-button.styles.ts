import styled from '@emotion/styled';
import { css } from '@emotion/react';

import colors from '../../bem-chakra-theme/foundations/colors';

export const Container = styled.div<{ expanded: boolean; color?: string }>`
  width: 24px;

  .one,
  .two,
  .three {
    background-color: ${({ color }) =>
      css`
        ${color || colors.grey[100]}
      `};
    height: 4px;
    width: 100%;
    margin: 4px auto;

    transition-duration: 0.3s;
  }

  ${({ expanded }) =>
    expanded &&
    css`
      .one {
        transform: rotate(45deg) translate(4.5px, 4.5px);
      }

      .two {
        opacity: 0;
      }

      .three {
        transform: rotate(-45deg) translate(7px, -7px);
      }
    `}
`;
