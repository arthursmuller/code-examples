import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import { RadioCardsGroup, RadioCardVariant } from '@pcf/design-system';
import { useClienteLogado } from '@pcf/core';

import { BaseLayoutStep } from './base-step-layout';

export const Emails: FC = () => {
  const { data, isLoading } = useClienteLogado();
  const emails = data.email ? [data.email] : [];

  return (
    <BaseLayoutStep isLoading={isLoading}>
      <RadioCardsGroup
        name="emails"
        defaultValue={data.email || undefined}
        onChange={() => {}}
        isRadio
      >
        {emails.map((email) => (
          <RadioCardVariant
            key={email}
            value={`${email}`}
            header={<Text color="white">E-mail</Text>}
            content={<Text>{data.email}</Text>}
            footer={null}
          />
        ))}
      </RadioCardsGroup>

      <BaseLayoutStep.Footer />
    </BaseLayoutStep>
  );
};
