import React, { FC, useState } from 'react';

import {
  Button,
  Center,
  Flex,
  useBreakpointValue,
  Text,
} from '@chakra-ui/react';
import { pdfjs, Document, Page } from 'react-pdf';
import { useMount } from 'react-use';

import { SubPageHeader } from 'features/landing/components/sub-page-header';
import { CustomHeading, Loader } from '@pcf/design-system';
import { LandingTemplate } from 'features/landing/landing-template';

import CondicoesImg from './assets/condicoes.jpg';
import CondicoesImgMob from './assets/condicoes-mob.jpg';

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

export const Conditions: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });
  const [width, height] = useBreakpointValue(
    {
      base: [280, 396],
      sm: [400, 566],
      md: [600, 849],
      lg: [800, 1132],
      xl: [1000, 1415],
    },
    'sm',
  );

  const [isMounted, setMounted] = useState<boolean>(false);

  useMount(() => setMounted(true));

  const [numPages, setNumPages] = useState<number>(null);
  const [pageNumber, setPageNumber] = useState<number>(1);

  function onDocumentLoadSuccess({ numPages }): void {
    setNumPages(numPages);
  }

  return (
    <LandingTemplate>
      <SubPageHeader
        backgroundImage={
          isMobile !== undefined && (isMobile ? CondicoesImgMob : CondicoesImg)
        }
        backgroundImageAlt="contrato"
        position={['80%', '80%', 'right']}
      >
        <SubPageHeader.Title
          title="Condições gerais banrisul"
          width={['340px', '340px', '600px']}
        />
        <SubPageHeader.Subtitle
          subtitle="e rápido para o que você precisar"
          subtitleOrange="Empréstimo fácil"
        />
      </SubPageHeader>

      <Flex
        minHeight="500px"
        paddingX={0}
        paddingY={8}
        justifyContent="center"
        direction="column"
        alignItems="center"
      >
        <Flex color="secondary.mid-dark" alignItems="center" width={width}>
          <CustomHeading
            marginBottom={6}
            as="h3"
            textStyle={['bold32', 'bold32', 'bold40']}
            textAlign="center"
          >
            Condições gerais do contrato de concessão de empréstimo mediante
            consignação em folha de pagamento
          </CustomHeading>
        </Flex>

        <Flex
          background="secondary.regular"
          direction="column"
          width="100%"
          alignItems="center"
          paddingX={[6, 4, 6]}
          paddingY={[6, 6, '60px']}
          marginBottom={8}
        >
          {isMounted && (
            <Flex
              position="relative"
              width={width}
              height={height}
              background="white"
            >
              <Flex
                position="absolute"
                zIndex={1}
                bottom={2}
                backgroundColor="grey.800"
                opacity="0.8"
                borderRadius="8px"
                right="0"
                left="0"
                margin="auto"
                width="fit-content"
                minWidth="135px"
                alignItems="center"
              >
                <Button
                  margin={1}
                  size="sm"
                  variant="ghost"
                  onClick={() => setPageNumber((p) => p - 1)}
                  disabled={pageNumber === 1}
                >
                  &lt;
                </Button>

                <Text
                  color="white"
                  textStyle="bold14"
                  margin={1}
                  textAlign="center"
                  flex={1}
                >
                  {pageNumber} / {numPages}
                </Text>

                <Button
                  margin={1}
                  size="sm"
                  variant="ghost"
                  right={0}
                  onClick={() => setPageNumber((p) => p + 1)}
                  disabled={pageNumber === numPages}
                >
                  &gt;
                </Button>
              </Flex>

              <Document
                file="assets/documentos/condicoes-gerais.pdf"
                onLoadSuccess={onDocumentLoadSuccess}
                onLoadError={console.error}
                loading={
                  <Center height={height} width={width}>
                    <Loader />
                  </Center>
                }
              >
                <Page
                  pageNumber={pageNumber}
                  width={width}
                  loading={
                    <Center height={height} width={width}>
                      <Loader />
                    </Center>
                  }
                />
              </Document>
            </Flex>
          )}
          <Flex
            justifyContent={['center', 'center', 'flex-end']}
            marginTop={4}
            width={width}
          >
            <Button
              variant="link"
              textDecoration="underline"
              color="white"
              as="a"
              href="assets/documentos/condicoes-gerais.pdf"
              download="condicoes-gerais.pdf"
            >
              Para fazer o download clique aqui
            </Button>
          </Flex>
        </Flex>
      </Flex>
    </LandingTemplate>
  );
};
