import { Box } from '@chakra-ui/react';

import { BemImage } from 'features/components/images';
import MockupSvg from 'features/landing/assets/mockup@2x.png';

const Mockup: React.FC = () => {
  return (
    <Box
      display={['none', 'none', 'none', 'none', 'flex']}
      position="relative"
      width="342px"
    >
      <Box
        sx={{
          zIndex: 9,
          position: 'absolute',
          top: '-230px',
          width: '342px',
          height: '532px',
          transform: 'rotate(10deg)',
          display: ['none', 'none', 'none', 'none', 'flex'],
        }}
      >
        <BemImage
          width="342px"
          height="532px"
          src={MockupSvg}
          alt="Celular com site da Bem Promotora exibido na tela"
        />
      </Box>
    </Box>
  );
};

export default Mockup;
