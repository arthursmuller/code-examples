import { Box } from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { useHistory } from 'react-router-dom';

import FormContainer from '../../../components/FormContainer';
import PageTitle from '../../../components/PageTitle';
import { QRCodeCanvas } from '../../../components/QRCodeCanvas';
import Text from '../../../components/Text';
import { useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';

const QRCodeDeviceOwner = () => {
  const { form, textToGenerate } = useAppSelector((state) => state.qrcode);
  const history = useHistory();

  return (
    <>
      <PageTitle
        title={<FormattedMessage id="qrcode.show.title" />}
        description={<FormattedMessage id="qrcode.show.description" />}
      />
      <FormContainer
        labelSecundary={<FormattedMessage id="global.back" />}
        handleSecundary={() => history.push(routes.qrcode.generate)}
      >
        <Box d="flex" flexDirection="row">
          <Box w="500px" h="500px">
            <QRCodeCanvas text={textToGenerate}/>
          </Box>
          <Box d="flex" flexDirection="column" ml="3%">
            <Box>
              <Text fontWeight="400" m="0" fontSize="sm">
                <FormattedMessage id="qrcode.show.url" />
              </Text>
              <Text
                fontWeight="400"
                m="10px 0px 0px 0px"
                color="black.500"
                fontSize="sm"
              >
                {form.url}
              </Text>
            </Box>
            <Box mt="30px">
              <Text fontWeight="400" m="0" fontSize="sm">
                <FormattedMessage id="qrcode.show.packageName" />
              </Text>
              <Text
                fontWeight="400"
                m="10px 0px 0px 0px"
                color="black.500"
                fontSize="sm"
              >
                {form.packageName}
              </Text>
            </Box>
            <Box mt="30px">
              <Text fontWeight="400" m="0" fontSize="sm">
                <FormattedMessage id="qrcode.show.language" />
              </Text>
              <Text
                fontWeight="400"
                m="10px 0px 0px 0px"
                color="black.500"
                fontSize="sm"
              >
                {form.language}
              </Text>
            </Box>
            <Box mt="30px">
              <Text fontWeight="400" m="0" fontSize="sm">
                <FormattedMessage id="qrcode.show.timezone" />
              </Text>
              <Text   
                fontWeight="400"
                m="10px 0px 0px 0px"
                color="black.500"
                fontSize="sm"
              >
                {form.timezone}
              </Text>
            </Box>
            {form.ssid && (
              <>
                <Box mt="30px">
                  <Text fontWeight="400" m="0" fontSize="sm">
                    <FormattedMessage id="qrcode.show.ssid" />
                  </Text>
                  <Text
                    fontWeight="400"
                    m="10px 0px 0px 0px"
                    color="black.500"
                    fontSize="sm"
                  >
                    {form.ssid}
                  </Text>
                </Box>
                <Box mt="30px">
                  <Text fontWeight="400" m="0" fontSize="sm">
                    <FormattedMessage id="qrcode.show.ssidPassword" />
                  </Text>
                  <Text
                    fontWeight="400"
                    m="10px 0px 0px 0px"
                    color="black.500"
                    fontSize="sm"
                  >
                    {form.ssidPassword}
                  </Text>
                </Box>
              </>
            )}
            {form.enableAllApps && (
              <Box mt="30px">
                <Text fontWeight="400" m="0" color="black.500" fontSize="sm">
                  <FormattedMessage id="qrcode.show.enabledApplication" />
                </Text>
              </Box>
            )}
          </Box>
        </Box>
      </FormContainer>
    </>
  );
};

export default QRCodeDeviceOwner;
