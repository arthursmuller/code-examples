import colors from './foundations/colors';

const styles = {
  global: () => {
    return {
      '*': {
        '::-webkit-scrollbar': {
          width: '7px',
          height: '7px',
        },
        '::-webkit-scrollbar-track': {
          borderRadius: '50px',
          padding: '2px',
          backgroundColor: colors.secondary.washed,
        },
        '::-webkit-scrollbar-thumb': {
          background: colors.secondary.light,
          borderRadius: '8px',
        },
      },

      body: {
        background: 'grey.100',
        color: 'grey.800',
        textRendering: 'optimizeLegibility !important',
        WebkitFontSmoothing: 'antialiased !important',
      },
      'body ,html, #root': {
        height: '100%',
        width: '100%',
      },

      'body, input, button': {
        fontFamily: `Inter, sans-serif`,
        border: 0,
      },

      '#chakra-toast-portal #chakra-toast-manager-top': {
        zIndex: `30 !important`,

        '.chakra-toast__inner': {
          maxWidth: '100% !important',
          width: '100%',
          margin: '0 !important',
        },
      },

      '.chakra-modal__overlay': {
        zIndex: `10 !important`,
      },

      button: {
        cursor: 'pointer',
      },
    };
  },
};

export default styles;
