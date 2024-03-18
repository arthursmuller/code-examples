import { CheckIcon, StatusCloseErrorIcon } from '@pcf/design-system-icons';

import { StepStatus } from './step-status.enum';

export const stepsSizeConfig = {
  xs: {
    outCircle: 4,
    innerCircle: 2,
    innerCircleThickness: '1px',
    barThickness: 1,
    barTop: '6px',
    barLeft: '6px',
  },
  sm: {
    outCircle: 6,
    innerCircle: 4,
    innerCircleThickness: '1px',
    barThickness: 2,
    barTop: '9px',
    barLeft: '9px',
  },
  md: {
    outCircle: 10,
    innerCircle: 6,
    innerCircleThickness: '2px',
    barThickness: 4,
    barTop: '13px',
    barLeft: '12.5px',
  },
};

export const stepItemConfig = {
  [StepStatus.inactive]: {
    bg: 'grey.400',
    color: 'grey.600',
    iconConfig: {
      iconComponent: null,
      sizes: {
        xs: {
          w: 0,
          h: 0,
        },
        sm: {
          w: 0,
          h: 0,
        },
        md: {
          w: 0,
          h: 0,
        },
      },
    },
  },
  [StepStatus.active]: {
    bg: 'secondary.regular',
    color: 'secondary.mid-dark',
    iconConfig: {
      iconComponent: CheckIcon,
      sizes: {
        xs: {
          w: '4.88px',
          h: '3.05px',
        },
        sm: {
          w: '8.88px',
          h: '6.05px',
        },
        md: {
          w: '22px',
          h: '22px',
        },
      },
    },
  },
  [StepStatus.success]: {
    bg: 'success.regular',
    color: 'success.regular',
    iconConfig: {
      iconComponent: CheckIcon,
      sizes: {
        xs: {
          w: '4.88px',
          h: '3.05px',
        },
        sm: {
          w: '8.88px',
          h: '6.05px',
        },
        md: {
          w: '22px',
          h: '22px',
        },
      },
    },
  },
  [StepStatus.error]: {
    bg: 'error.regular',
    color: 'error.dark',
    iconConfig: {
      iconComponent: StatusCloseErrorIcon,
      sizes: {
        xs: {
          w: '4.99px',
          h: '4.99px',
        },
        sm: {
          w: '6.99px',
          h: '6.99px',
        },
        md: {
          w: '10.49px',
          h: '10.49px',
        },
      },
    },
  },
};
