import { ChevronDownIcon, ChevronRightIcon } from '@chakra-ui/icons';
import {
  Box,
  Accordion,
  AccordionItem,
  AccordionButton,
  AccordionPanel,
} from '@chakra-ui/react';
import React from 'react';

import Card from '../../../components/Card';

const AccordionCard = () => {
  return (
    <>
      <Accordion allowMultiple>
        <Card w="100%" p="0">
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      ¿Por cuánto tiempo estará disponible la información de
                      consumo en el portal?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      ¿Puedo tener una lista completa de los dispositivos de mi
                      empresa? ¿Qué información puedo obtener de ellos
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Sí. En el menú dispositivos. La información que pude obtener
                  es la siguiente: Número del teléfono; IMEI: es el número de
                  identificación global y único del dispositivo; ICCID: es el
                  número del chip (SIM); Marca y modelo del dispositivo; Versión
                  del Sistema Operativo; Versión del App Control Móvil Telcel;
                  Historial de almacenamiento; Estado del dispositivo, GPS y
                  Complience; Última hora de actualización.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      ¿Cómo puedo ver el consumo de un usuario en específico?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      ¿Cuál la diferencia entre usuario y dispositivo?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      He perdido mi contraseña/login de acceso al portal, ¿cómo
                      puedo rescatarla?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      ¿Cómo hago para saber el IMEI del aparato en el mismo
                      celular?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      En el portal ¿cómo saber si el aparato está vinculado a
                      determinado usuario?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      Si el usuario borra el historial de navegación ¿esta
                      información será mostrada en el portal?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      ¿Cuál el tiempo de actualización de la información acerca
                      del celular en el portal?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
          <AccordionItem>
            {({ isExpanded }) => (
              <>
                <h2>
                  <AccordionButton h="60px">
                    {isExpanded ? (
                      <ChevronDownIcon color="gray.600" boxSize={8} />
                    ) : (
                      <ChevronRightIcon color="gray.600" boxSize={8} />
                    )}
                    <Box
                      ml="1%"
                      flex="1"
                      fontSize="sm"
                      color="gray.500"
                      textAlign="left"
                    >
                      He bloqueado una aplicación, pero ella sigue accediendo a
                      internet, ¿qué puede ser?
                    </Box>
                  </AccordionButton>
                </h2>
                <AccordionPanel fontSize="sm">
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Ut enim ad minim veniam, quis nostrud exercitation ullamco
                  laboris nisi ut aliquip ex ea commodo consequat.
                </AccordionPanel>
              </>
            )}
          </AccordionItem>
        </Card>
      </Accordion>
    </>
  );
};

export default AccordionCard;
