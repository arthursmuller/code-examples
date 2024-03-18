import { Box } from '@chakra-ui/react';
import QRCode from 'qrcode';
import { useEffect, useRef, useState } from 'react';
import { FormattedMessage } from 'react-intl';

export const QRCodeCanvas = ({ text }) => {
  const [failure, setFailure] = useState(false);
  const canvasRef = useRef();
  useEffect(() => {
    QRCode.toCanvas(canvasRef.current, text, { width: 500 }, () => {
      setFailure(!text);
    });
  }, [text]);

  return (
    <Box>
      <canvas ref={canvasRef} id="canvas" />
      {failure ? <FormattedMessage id="qrcode.failure_generate" /> : null}
    </Box>
  );
};
