import { Box, Text } from '@chakra-ui/react';
import { useMemo } from 'react';
import { useIntl } from 'react-intl';

import { sanitizeFilter } from '../../../helper/filter';
import { useAppSelector } from '../../../hooks/useRedux';
import CardHeader from '../../CardHeader';

interface ApplicationHeaderProps {
  intlKey: string;
  showDescription: boolean;
}

const ApplicationHeader = ({
  intlKey,
  showDescription,
}: ApplicationHeaderProps) => {
  const { application, filter } = useAppSelector((state) => state.application);
  const intl = useIntl();

  const filters = {
    deviceUser: {
      translation: intl.formatMessage({
        id: `${intlKey}.filter.device_user`,
      }),
      parseValue: (value) => `${value.name ?? ''} ${value.phoneNumber ?? ''}`,
    },
    group: {
      translation: intl.formatMessage({
        id: `${intlKey}.filter.group`,
      }),
      parseValue: (value) => `${value.name ?? ''}`,
    },
    subgroup: {
      translation: intl.formatMessage({
        id: `${intlKey}.filter.subgroup`,
      }),
      parseValue: (value) => `${value.name ?? ''}`,
    },
  };

  const appliedFilters = useMemo(() => {
    const filterElements = Object.entries(sanitizeFilter(filter)).map(
      ([key, value]) => (
        <span key={key}>
          {filters[key].translation} <em>{filters[key].parseValue(value)}</em>
        </span>
      )
    );

    return filterElements.reduce((accumulator, current, index) => {
      if (index === 0) return accumulator;
      return (
        <>
          {accumulator}, {current}
        </>
      );
    }, []);
  }, [filter]);

  return (
    <CardHeader
      description={
        !!showDescription && (
          <Box d="flex" flexDirection="column">
            <Text color="gray.500" fontSize="xs" fontWeight="normal" mt="8px">
              Filtrado por: {appliedFilters}
            </Text>
          </Box>
        )
      }
      iconUrl={application?.urlIcon}
      title={application?.name}
    />
  );
};

export default ApplicationHeader;
