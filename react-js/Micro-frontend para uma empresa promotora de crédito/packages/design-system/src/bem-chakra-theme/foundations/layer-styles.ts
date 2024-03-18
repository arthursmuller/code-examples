const card = {
  bg: 'white',
  borderRadius: '8px',
  padding: '16px 24px',
  boxShadow: 'soft',
};

const layerStyles = {
  card,
  cardProduct: {
    ...card,
    padding: '40px 20px',
  },
  'card-without-border-radius-right': {
    ...card,
    borderBottomRightRadius: 0,
    borderTopRightRadius: 0,
  },
  'card-without-border-radius-left': {
    ...card,
    borderBottomLeftRadius: 0,
    borderTopLeftRadius: 0,
  },
  'card-underneath-bottom': {
    ...card,
    borderRadius: '25px 25px 0 0',
  },
  nav: {
    color: 'white',
    textDecoration: 'none',
    fontWeight: '700',
    lineHeight: '24px',
    fontSize: ['sm', 'md'],
    borderRadius: '4px',
    padding: '12px 24px',
    transition: 'background .2s',
    '&:hover': {
      bg: 'rgba(0,0,0,0.1)',
      textDecoration: 'none',
    },
  },
  navActive: {
    color: 'white',
    textDecoration: 'none',
    fontWeight: '700',
    fontSize: ['sm', 'md'],
    borderRadius: '4px',
    lineHeight: '24px',
    borderBottom: '2px solid',
    borderBottomRightRadius: 0,
    borderBottomLeftRadius: 0,
    padding: '12px 24px',
    '&:hover': {
      bg: 'rgba(0,0,0,0.1)',
      textDecoration: 'none',
    },
  },
  link: {
    color: 'white',
    fontWeight: '500',
    letterSpacing: '0.04em',
    fontSize: 12,
    lineHeight: '16px',
  },
};

export default layerStyles;
