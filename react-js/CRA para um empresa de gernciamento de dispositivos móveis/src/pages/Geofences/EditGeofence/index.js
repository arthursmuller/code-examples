import { Box, FormControl, FormLabel, Divider, Button } from '@chakra-ui/react';
import { GoogleMap, useJsApiLoader } from '@react-google-maps/api';
import React, { useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';

import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import Input from '../../../components/Input';
import Text from '../../../components/Text';
import { updateGeofence } from '../../../store/geofence';
import { history } from '../../../store/history';

const EditGeofence = () => {
  const geofence = useSelector((state) => state.geofence);
  const dispatch = useDispatch();
  const [form, setForm] = useState({
    name: '',
    adress: '',
    radius: '',
  });

  const handleInputChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const submit = () => {
    dispatch(updateGeofence(form));
    history.push('/manage-geofences');
  };

  const { isLoaded } = useJsApiLoader({
    id: 'google-map-script',
    googleMapsApiKey: 'AIzaSyBTkTC5K1wAHF6ZXzN_sKweYskVLQItMCw',
  });
  const containerStyle = {
    width: '100%',
    height: '485px',
  };
  const center = { lat: 39.5, lng: -98.35 };
  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="geofence.edit_title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="geofence.edit_description" />
        </Text>
      </Box>
      <Card>
        <Box d="flex" flexDirection="row">
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="geofence.edit.form_name" />
            </FormLabel>
            <Input
              inputProps={{
                name: 'name',
                value: form.name,
                onChange: handleInputChange,
              }}
            />
          </FormControl>
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="global.adress" />
            </FormLabel>
            <Input
              inputProps={{
                name: 'adress',
                value: form.adress,
                onChange: handleInputChange,
              }}
            />
          </FormControl>
          <FormControl w="276px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="geofence.edit.form_radius" />
            </FormLabel>
            <Input
              inputProps={{
                name: 'radius',
                value: form.radius,
                onChange: handleInputChange,
              }}
            />
          </FormControl>
        </Box>
        <Box mt="2%">
          {isLoaded && (
            <GoogleMap
              mapContainerStyle={containerStyle}
              center={center}
              zoom={3}
            />
          )}
        </Box>
        <Box mt="2%">
          <Divider orientation="horizontal" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Button
            bg="blue.500"
            color="white"
            h="45px"
            w="176px"
            onClick={submit}
          >
            <FormattedMessage id="global.update" />
          </Button>
          <Divider
            orientation="vertical"
            h="22px"
            ml="1%"
            borderColor="gray.600"
          />
          <Link to="/manage-geofences">
            <Button variant="ghost" color="blue.500">
              <FormattedMessage id="global.cancel" />
            </Button>
          </Link>
        </Box>
      </Card>
    </>
  );
};

export default EditGeofence;
