import { FC, useState, useRef } from 'react';

import { Flex, Text, Box, Button, Icon } from '@chakra-ui/react';
import { AnimatePresence, motion } from 'framer-motion';
import { useGeolocation } from 'react-use';

import {
  CustomHeading,
  FormItem,
  getFormattedPhone,
  BemErrorBoundary,
  CreateAsyncSelect,
  Loader,
} from '@pcf/design-system';
import { useLojas, useUnidadesFederativas } from '@pcf/core';
import { ArrowOutlineRightIcon } from '@pcf/design-system-icons';

import BemMarker from './bem-marker.svg';
import { GoogleMap, GoogleMapRef } from './google-map';

export const MapContent: FC = () => {
  const [uf, setUf] = useState<number>();
  const { data: lojas, isLoading: isLoadingLojas } = useLojas(
    { idUf: uf },
    { skipAlerts: true },
  );
  const mapRef = useRef<GoogleMapRef>(null);
  const {
    loading: loadingGeolocation,
    latitude,
    longitude,
  } = useGeolocation({ timeout: 3000 });

  return (
    <Flex
      direction="column"
      paddingX={[6, 6, 6, '120px']}
      paddingTop={['54px', '54px', '72px']}
      paddingBottom={['64px', '64px', '80px']}
      marginTop={[14, 14, 0]}
      backgroundColor="secondary.mid-dark"
    >
      <CustomHeading
        color="white"
        marginBottom={[6, 6, 6, 8]}
        textStyle="bold32_40"
      >
        Encontre uma de nossas lojas
      </CustomHeading>

      <Flex direction={['column', 'column', 'column', 'row']} flexShrink={0}>
        <Flex
          marginRight={[0, 0, 0, 6]}
          direction="column"
          width={['100%', '100%', '100%', '376px']}
          maxHeight={['400px', '400px', '400px', '520px']}
          flexShrink={0}
        >
          <Flex flexShrink={0}>
            <FormItem overrideColor="white" label="Selecione o seu estado">
              <CreateAsyncSelect
                useQueryHook={useUnidadesFederativas}
                configSelectOption={{
                  valueKey: 'id',
                  nameKey: 'nome',
                }}
                onChange={setUf}
                defaultValue={uf}
              />
            </FormItem>
          </Flex>

          <Flex
            direction="column"
            marginBottom={[6, 6, 6, 0]}
            paddingRight={2}
            marginRight={-2}
            overflow="auto"
          >
            <AnimatePresence>
              {lojas?.map((loja, i) => (
                <Flex
                  flexShrink={0}
                  direction="column"
                  layerStyle="card"
                  marginBottom={2}
                  key={`${loja.id}`}
                  as={motion.div}
                  initial={{ opacity: 0 }}
                  animate={{
                    opacity: 1,
                    transition: { duration: (i + 1) / 8 },
                  }}
                  exit={{ opacity: 0, transition: { duration: (i + 1) / 16 } }}
                >
                  <Text color="grey.700">
                    <Box as="span" fontWeight="bold" color="grey.800">
                      {loja.municipio?.descricao}
                    </Box>{' '}
                    | {loja.bairro} ({loja.logradouro})
                  </Text>

                  <Text marginTop={2} marginBottom={6}>
                    {loja.nome}
                  </Text>

                  <Flex alignItems="center">
                    <Text flex={1}>{loja.municipio?.uf?.nome}</Text>
                    <Button
                      variant="link"
                      onClick={() =>
                        mapRef.current?.focusOn(loja.latitude, loja.longitude)
                      }
                      rightIcon={
                        <Icon
                          display="flex"
                          ml={3}
                          w="7px"
                          h="12px"
                          color="grey.700"
                          as={ArrowOutlineRightIcon}
                        />
                      }
                    >
                      Ver no mapa
                    </Button>
                  </Flex>
                </Flex>
              ))}
            </AnimatePresence>
          </Flex>
        </Flex>
        <Flex flexGrow={1} height={['400px', '400px', '400px', '520px']}>
          {loadingGeolocation || isLoadingLojas ? (
            <Loader w="100%" />
          ) : (
            <GoogleMap
              clientLatitude={latitude}
              clientLongitude={longitude}
              points={
                !lojas?.length
                  ? []
                  : lojas?.map((loja) => ({
                      latitude: loja.latitude,
                      longitude: loja.longitude,
                      title: loja.nome,
                      subtitle: `${loja.municipio?.descricao} | ${loja.bairro}`,
                      image: BemMarker,
                      info: `${loja.tipoLogradouro?.descricao} ${
                        loja.logradouro
                      }${
                        loja.numero ? `, ${loja.numero}` : ''
                      }<br/>Telefone: ${loja.telefones?.reduce(
                        (acc, cur) =>
                          `${acc ? ' / ' : ''}${getFormattedPhone(
                            cur.telefone,
                          )}`,
                        '',
                      )}
              `,
                    }))
              }
              ref={mapRef}
            />
          )}
        </Flex>
      </Flex>
    </Flex>
  );
};

export const Map: FC = () => (
  <BemErrorBoundary>
    <MapContent />
  </BemErrorBoundary>
);
