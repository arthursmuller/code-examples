import {
  Box,
  FormControl,
  FormLabel,
  Button,
  Divider,
  Switch,
  Wrap,
  WrapItem,
} from '@chakra-ui/react';
import React, { useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';

import Card from '../../../components/Card';
import Input from '../../../components/Input';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import {
  updateGeneralCategory,
  closeGeneralToaster,
} from '../../../store/site';

const GeneralCard = () => {
  const dispatch = useDispatch();
  const { block_categories } = useSelector((state) => state.site);
  const { showToaster: toaster, general } = block_categories;
  const [categories, setCategories] = useState(general);

  const handleSwitchChange = ({ target }) => {
    setCategories({ ...categories, [target.name]: target.checked });
  };

  const submit = () => {
    dispatch(updateGeneralCategory(categories));
  };

  const switches1 = React.useMemo(() => [
    'education',
    'government',
    'entertainment',
    'search_portal',
    'news',
    'sports',
    'business',
  ]);
  const switches2 = React.useMemo(() => [
    'health',
    'games',
    'tech',
    'trips',
    'shopping',
    'job',
    'email',
  ]);
  const switches3 = React.useMemo(() => [
    'forums',
    'social_media',
    'chat',
    'buy_files',
    'gambling',
    'proxies',
    'violence',
  ]);
  const switches4 = React.useMemo(() => [
    'rudeness',
    'adult_content',
    'alcohol',
    'drugs',
    'tobacco',
  ]);
  return (
    <>
      <Toaster
        w="100%"
        ml="-1%"
        open={general.showToaster}
        onClose={() => dispatch(closeGeneralToaster())}
      >
        <FormattedMessage id="sites.block.general.success" />
      </Toaster>
      <Card w="100%" ml="-1%" mt="2%">
        <Box d="flex" flexDirection="column">
          <FormControl w="376px">
            <FormLabel fontSize="sm" color="gray.300">
              <FormattedMessage id="sites.block.general.company" />
            </FormLabel>
            <Input
              inputProps={{
                backgroundColor: 'white',
                disabled: true,
                placeholder: 'VOSTOK ONE',
                _disabled: { bg: 'gray.400' },
                _placeholder: { color: 'gray.500' },
              }}
            />
          </FormControl>
          <Text m="3% 0% 0% 0%" color="gray.300">
            <FormattedMessage id="sites.block.general.notice" />
          </Text>
          <Text m="3% 0% 0% 0%" fontWeight="600" fontSize="md">
            <FormattedMessage id="sites.block.general.block_content" />
          </Text>
          <Box mt="2%">
            <Divider
              borderColor="gray.600"
              orientation="horizontal"
              mb="1.5%"
            />
          </Box>
          <Box>
            <Wrap spacing="1%" justifyContent="space-around">
              <WrapItem flexDirection="column" w="300px">
                {switches1.map(function (item) {
                  return (
                    <FormControl
                      key={item}
                      d="flex"
                      alignItems="center"
                      w="100%"
                      mb="5%"
                    >
                      <Switch
                        m="0px 10px 0px 0px"
                        colorScheme="red"
                        size="lg"
                        isChecked={categories[item]}
                        name={item}
                        onChange={handleSwitchChange}
                      />
                      <FormLabel fontSize="sm" color="gray.500">
                        <FormattedMessage id={`sites.block.general.${item}`} />
                      </FormLabel>
                    </FormControl>
                  );
                })}
              </WrapItem>
              <WrapItem flexDirection="column" w="300px">
                {switches2.map(function (item, i) {
                  return (
                    <FormControl
                      key={i}
                      d="flex"
                      alignItems="center"
                      w="100%"
                      mb="5%"
                    >
                      <Switch
                        m="0px 10px 0px 0px"
                        colorScheme="red"
                        size="lg"
                        isChecked={categories[item]}
                        name={item}
                        onChange={handleSwitchChange}
                      />
                      <FormLabel fontSize="sm" color="gray.500">
                        <FormattedMessage id={`sites.block.general.${item}`} />
                      </FormLabel>
                    </FormControl>
                  );
                })}
              </WrapItem>
              <WrapItem flexDirection="column" w="300px">
                {switches3.map(function (item, i) {
                  return (
                    <FormControl
                      key={i}
                      d="flex"
                      alignItems="center"
                      w="100%"
                      mb="5%"
                    >
                      <Switch
                        m="0px 10px 0px 0px"
                        colorScheme="red"
                        size="lg"
                        isChecked={categories[item]}
                        name={item}
                        onChange={handleSwitchChange}
                      />
                      <FormLabel fontSize="sm" color="gray.500">
                        <FormattedMessage id={`sites.block.general.${item}`} />
                      </FormLabel>
                    </FormControl>
                  );
                })}
              </WrapItem>
              <WrapItem flexDirection="column" w="300px">
                {switches4.map(function (item, i) {
                  return (
                    <FormControl
                      key={i}
                      d="flex"
                      alignItems="center"
                      w="100%"
                      mb="5%"
                    >
                      <Switch
                        m="0px 10px 0px 0px"
                        colorScheme="red"
                        size="lg"
                        isChecked={categories[item]}
                        name={item}
                        onChange={handleSwitchChange}
                      />
                      <FormLabel fontSize="sm" color="gray.500">
                        <FormattedMessage id={`sites.block.general.${item}`} />
                      </FormLabel>
                    </FormControl>
                  );
                })}
              </WrapItem>
            </Wrap>
          </Box>
        </Box>
        <Box mt="3%">
          <Divider borderColor="gray.600" orientation="horizontal" mb="1.5%" />
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
          <Button variant="ghost" color="blue.500">
            <FormattedMessage id="global.cancel" />
          </Button>
        </Box>
      </Card>
    </>
  );
};

export default GeneralCard;
