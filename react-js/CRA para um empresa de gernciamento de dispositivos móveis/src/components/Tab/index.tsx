import { Tab as TabChakra, TabProps as TabPropsChakra } from '@chakra-ui/react';

type TabProps = TabPropsChakra;

const Tab = ({ children, ...rest }: TabProps) => {
  return (
    <TabChakra
      fontSize="xl"
      fontWeight="bold"
      color="gray.300"
      ml="10px"
      _selected={{
        color: 'blue.600',
        borderBottom: '4px solid #0a3b79',
        marginBottom: '-4px',
      }}
      {...rest}
    >
      {children}
    </TabChakra>
  );
};

export default Tab;
