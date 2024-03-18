import { FC, ReactElement } from 'react';

import {
  ErrorBoundary,
  ErrorBoundaryProps,
  FallbackProps,
} from 'react-error-boundary';
import { useQueryErrorResetBoundary } from 'react-query';

import { BemErrorFallback } from '../bem-error-fallback';

export type BemErrorBoundaryProps = Partial<ErrorBoundaryProps> & {
  fallbackRender?: (props: FallbackProps) => ReactElement<any, any> | null; //eslint-disable-line
  errorMessage?: string | ((error: Error) => string);
};

export const BemErrorBoundary: FC<BemErrorBoundaryProps> = ({
  children,
  fallbackRender,
  errorMessage,
  ...rest
}) => {
  const reactQueryErrorBoundary = useQueryErrorResetBoundary();

  return (
    <ErrorBoundary
      fallbackRender={
        fallbackRender ||
        ((props) => {
          const isAlert = typeof props.error === 'string';

          let description = '';

          if (isAlert) {
            description = props.error as any;
          } else {
            description =
              typeof errorMessage === 'function'
                ? errorMessage(props.error)
                : errorMessage;
          }

          return (
            <BemErrorFallback
              isAlert={isAlert}
              {...props}
              description={description}
            />
          );
        })
      }
      onReset={() => reactQueryErrorBoundary.reset()}
      {...rest}
    >
      {children}
    </ErrorBoundary>
  );
};
