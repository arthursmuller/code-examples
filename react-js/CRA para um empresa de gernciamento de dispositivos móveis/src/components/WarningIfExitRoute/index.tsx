import { useEffect } from 'react';
import { Prompt } from 'react-router-dom';

interface WarningIfExitRouteProps {
  preventExit: boolean,
  message: string,
}

export default function WarningIfExitRoute({
  preventExit,
  message,
}: WarningIfExitRouteProps) {
  useEffect(() => {
    const beforeunload = (e) => {
      if (preventExit) {
        e.preventDefault();
        e.returnValue = message;
        return message;
      }
    };

    window.addEventListener('beforeunload', beforeunload);

    return () => {
      window.removeEventListener('beforeunload', beforeunload);
    };
  }, [preventExit]);

  return <Prompt when={preventExit} message={message} />;
}
