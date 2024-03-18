import { Box, Button, Divider } from '@chakra-ui/react';
import React from 'react';

import FaqImage_one from '../../../assets/Images/help/faq1.png';
import FaqImage_two from '../../../assets/Images/help/faq2.png';
import FaqImage_three from '../../../assets/Images/help/faq3.png';
import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import Expand from '../../../components/Icons/Expand';
import Search from '../../../components/Icons/Search';
import Input from '../../../components/Input';
import Text from '../../../components/Text';
import AccordionCard from './Accordion';

const FAQ = () => {
  return (
    <>
      <Box>
        <Heading>Preguntas frecuentes</Heading>
        <Text width="90%">
          Lorem ipsum dolor sit amet consectetur adipiscing elit
        </Text>
      </Box>
      <Box d="flex" flexDirection="column" w="90%">
        <Input
          inputProps={{
            placeholder: 'Como podemos lhe ajudar?',
            backgroundColor: 'white',
            _placeholder: { color: 'gray.300' },
            display: 'flex',
          }}
          leftElement={<Search boxSize={8} color="gray.600" />}
        />
        <Text fontSize="md" as="i">
          Filtre sua dúvida nas categorias abaixo, ou pesquise no campo de busca
          acima.
        </Text>
        <Box d="flex" flexDirection="row">
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
            mr="24px"
          >
            Dispositivos
          </Button>
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
            mr="24px"
          >
            Device owner
          </Button>
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
            mr="24px"
          >
            Android
          </Button>
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
          >
            Geolocalização
          </Button>
        </Box>
        <Box d="flex" flexDirection="row" mt="24px">
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
            mr="24px"
          >
            Modo Quiosque
          </Button>
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
            mr="24px"
          >
            Ações na aplicação
          </Button>
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
            mr="24px"
          >
            Configurações
          </Button>
          <Button
            h="75px"
            w="100%"
            bg="white"
            color="blue.500"
            fontWeight="normal"
            _focus={{
              bg: 'gray.500',
              color: 'white',
            }}
          >
            Todas
          </Button>
        </Box>
        <Text m="2% 0% 1% 0%" fontWeight="600">
          Dispositivos
        </Text>
        <Box mb="2%">
          <Divider borderColor="gray.600" orientation="horizontal" />
        </Box>
        <AccordionCard />
        <Box mt="2%" d="flex" alignSelf="center">
          <Button
            leftIcon={<Expand boxSize={6} />}
            color="blue.500"
            fontWeight="normal"
            variant="link"
          >
            Carregar mais
          </Button>
        </Box>
        <Box m="3% 10% 0% 10%">
          <Box d="flex" flexDirection="row">
            <Card w="100%" mr="12px" p="1% 1%">
              <Box d="flex" flexDirection="row" justifyContent="space-between">
                <Box
                  d="flex"
                  flexDirection="column"
                  justifyContent="space-between"
                >
                  <Text m="0%" fontSize="4xl" color="black.500">
                    Manuais para Downloads
                  </Text>
                  <Text m="2% 0% 0% 0%">
                    Confira nossos documentos y materiales de apoyo
                  </Text>
                  <Text m="2% 0% 0% 0%" color="blue.500">
                    Saiba mais
                  </Text>
                </Box>
                <Box d="contents">
                  <Box
                    w="207px"
                    h="182px"
                    backgroundImage={`url('${FaqImage_one}')`}
                    backgroundRepeat="no-repeat"
                    backgroundSize="contain"
                    backgroundPosition="100%"
                  />
                </Box>
              </Box>
            </Card>
            <Card w="100%" ml="12px" p="1% 1%">
              <Box d="flex" flexDirection="row" justifyContent="space-between">
                <Box
                  d="flex"
                  flexDirection="column"
                  justifyContent="space-between"
                >
                  <Text m="0%" fontSize="4xl" color="black.500">
                    Videos tutoriais
                  </Text>
                  <Text m="2% 0% 0% 0%">
                    Confira nossos vídeos explicativos.
                  </Text>
                  <Text m="2% 0% 0% 0%" color="blue.500">
                    Saiba mais
                  </Text>
                </Box>
                <Box d="contents">
                  <Box
                    w="207px"
                    h="182px"
                    backgroundImage={`url('${FaqImage_two}')`}
                    backgroundRepeat="no-repeat"
                    backgroundSize="contain"
                    backgroundPosition="100%"
                  />
                </Box>
              </Box>
            </Card>
          </Box>
          <Card w="100%" mt="2%">
            <Box d="flex" flexDirection="row" justifyContent="space-between">
              <Box
                d="flex"
                flexDirection="column"
                justifyContent="space-between"
              >
                <Text m="0%" fontSize="4xl" color="black.500">
                  Ainda tem dúvida?
                </Text>
                <Text m="2% 0% 0% 0%">
                  Nossa equipe de suporte técnico pode lhe ajudar.
                </Text>
                <Text m="2% 0% 0% 0%" color="blue.500">
                  Solicitud de Soporte
                </Text>
              </Box>
              <Box d="contents">
                <Box
                  w="207px"
                  h="182px"
                  backgroundImage={`url('${FaqImage_three}')`}
                  backgroundRepeat="no-repeat"
                  backgroundSize="contain"
                  backgroundPosition="100%"
                />
              </Box>
            </Box>
          </Card>
        </Box>
      </Box>
    </>
  );
};

export default FAQ;
