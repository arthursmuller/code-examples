import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import { use100vh } from 'react-div-100vh';

import { CustomHeading, StepsActionDialog, useModal } from '@pcf/design-system';

import { CepSearchDisplay } from './cep-search';
import { CepSearchForm } from './cep-search-form';
import { CepSearchResults } from './cep-search-results';

const Content: FC = ({ children }) => {
  const height = use100vh();

  return (
    <Flex direction="column" maxHeight={`calc(${height}px * 0.75)`}>
      <CustomHeading
        textStyle="bold24"
        color="secondary.mid-dark"
        marginBottom={[2, 8]}
        marginTop={[0, 2]}
        textAlign="center"
      >
        Consulta de CEP
      </CustomHeading>
      {children}
    </Flex>
  );
};
export const CepSearchMobile: FC<CepSearchDisplay> = ({
  buttonRender: ButtonRender,
  data,
  onSubmit,
}) => {
  const { showModal } = useModal();

  const openDialog = (): void =>
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <StepsActionDialog>
          <Content>
            <CepSearchForm initialData={data} />
          </Content>
          <Content>
            <CepSearchResults onSubmit={onSubmit} />
          </Content>
        </StepsActionDialog>
      ),
    });

  return <ButtonRender onClick={openDialog} />;
};

export { CepSearchMobile as default };
