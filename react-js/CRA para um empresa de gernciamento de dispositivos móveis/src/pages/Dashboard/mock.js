export function blockedWebsites() {
  return [
    {
      website: 'www.instagram.com.br',
      attempts: 48,
      color: '#de6163',
    },
    {
      website: 'www.youtube.com.br',
      attempts: 12,
    },
    {
      website: 'https://glassboxdigital.com/',
      attempts: 8,
    },
    {
      website: 'https://produto.mercadolivre.com.br/',
      attempts: 24,
      color: '#de6163',
    },
    {
      website: 'https://www.facebook.com.br',
      attempts: 73,
      color: '#de6163',
    },
    {
      website: 'https://www.twitter.com.br',
      attempts: 2,
    },
  ];
}

export function lastActivities() {
  return [
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
    {
      description: 'Lorem ipsum dolor sit amet',
      time: '23/01/2021 18:26:30',
    },
  ];
}

export function dataConsumptionPieChart() {
  const returnValue = {
    total: 1234,
    used: 122,
    colorTotal: '#D9F7F1',
    colorUsed: '#86E2D5',
    unit: 'MB',
  };

  return returnValue;
}

export function smsConsumptionPieChart() {
  const returnValue = {
    total: 4555,
    used: 345,
    colorTotal: '#EAE9FF',
    colorUsed: '#c8c7fd',
  };

  return returnValue;
}

export function applicationConsumption() {
  const returnValue = {
    total: 1200,
    data: [
      {
        label: 'Chrome',
        used: 700,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: 'Youtube',
        used: 130,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: 'Google Play Store',
        used: 100,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: 'Facebook',
        used: 56,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: 'Instagram',
        used: 276,
        color: '#98e6de',
        unit: 'MB',
      },
    ],
  };

  return returnValue;
}

export function userConsumption() {
  const returnValue = {
    total: 3000,
    data: [
      {
        label: '55 54 982278745',
        used: 2000,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: '55 51 999378874',
        used: 500,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: '55 51 978441013',
        used: 200,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: '55 54 975440415',
        used: 67,
        color: '#98e6de',
        unit: 'MB',
      },
      {
        label: '55 53 982378874',
        used: 2,
        color: '#98e6de',
        unit: 'MB',
      },
    ],
  };

  return returnValue;
}

export function applicationConsumptionTime() {
  const returnValue = {
    total: 730,
    data: [
      {
        label: 'Chrome',
        used: 10,
        color: '#adc9fa',
        unit: 'h',
      },
      {
        label: 'Youtube',
        used: 50,
        color: '#adc9fa',
        unit: 'h',
      },
      {
        label: 'Google Play Store',
        used: 100,
        color: '#adc9fa',
        unit: 'h',
      },
      {
        label: 'Facebook',
        used: 56,
        color: '#adc9fa',
        unit: 'h',
      },
      {
        label: 'Instagram',
        used: 276,
        color: '#adc9fa',
        unit: 'h',
      },
    ],
  };

  return returnValue;
}

export function visitedWebsites() {
  const returnValue = {
    total: 730,
    data: [
      {
        label: 'www.instagram.com.br',
        used: 10,
        color: '#c8c7fd',
        unit: 'MB',
      },
      {
        label: 'www.youtube.com.br',
        used: 50,
        color: '#c8c7fd',
        unit: 'MB',
      },
      {
        label: 'https://glassboxdigital.com/',
        used: 100,
        color: '#c8c7fd',
        unit: 'MB',
      },
      {
        label: 'https://produto.mercadolivre.com.br/',
        used: 56,
        color: '#c8c7fd',
        unit: 'MB',
      },
      {
        label: 'https://www.facebook,com.br',
        used: 276,
        color: '#c8c7fd',
        unit: 'MB',
      },
    ],
  };

  return returnValue;
}

export function inventoryPieChart() {
  const returnValue = {
    total: 456,
    data: [
      {
        id: 'Samsung',
        label: 'Samsung',
        qty: 45,
        color: '#98e6de',
      },
      {
        id: 'Xiaomi',
        label: 'Xiaomi',
        qty: 70,
        color: '#e9b8e8',
      },
      {
        id: 'LGE',
        label: 'LGE',
        qty: 140,
        color: '#fddfa2',
      },
      {
        id: 'Apple',
        label: 'Apple',
        qty: 234,
        color: '#c8c7fd',
      },
    ],
  };

  return returnValue;
}

export function consumption() {
  //
  // all values in percentage
  return {
    keys: ['dataUsed', 'smsUsed'],
    legendColors: ['#98e6de', '#c8c7fd'],
    data: [
      {
        period: '08/2020',
        dataUsed: 67,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 22,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
      },
      {
        period: '09/2020',
        dataUsed: 12,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 3,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
      },
      {
        period: '10/2020',
        dataUsed: 23,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 56,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
      },
      {
        period: '11/2020',
        dataUsed: 12,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 45,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
      },
      {
        period: '12/2020',
        dataUsed: 2,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 12,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
      },
      {
        period: '01/2021',
        dataUsed: 4,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 13,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
      },
      {
        period: '02/2021',
        dataUsed: 4,
        dataUsedLabel: 'Dados',
        dataUsedColor: '#98e6de',
        dataUsedUnit: 'MB',
        smsUsed: 13,
        smsUsedLabel: 'SMS',
        smsUsedColor: '#c8c7fd',
        smsUsedUnit: '',
      },
    ],
  };
}

export function users() {
  //
  // all values in percentage
  return {
    keys: ['newUsers', 'totalUsers'],
    legendColors: ['#98e6de', '#c8c7fd'],
    data: [
      {
        period: '08/2020',
        newUsers: 67,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 22,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
      {
        period: '09/2020',
        newUsers: 12,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 3,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
      {
        period: '10/2020',
        newUsers: 23,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 56,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
      {
        period: '11/2020',
        newUsers: 12,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 45,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
      {
        period: '12/2020',
        newUsers: 2,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 12,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
      {
        period: '01/2021',
        newUsers: 4,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 13,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
      {
        period: '02/2021',
        newUsers: 4,
        newUsersLabel: 'Novos usuaios',
        newUsersColor: '#98e6de',
        totalUsers: 13,
        totalUsersLabel: 'Total de usuarios',
        totalUsersColor: '#c8c7fd',
      },
    ],
  };
}

export function mapMarkers() {
  return [
    {
      lat: 37.772,
      lng: -122.214,
    },
    {
      lat: 47.772,
      lng: -121.214,
    },
    {
      lat: 48.772,
      lng: -123.214,
    },
    {
      lat: 50.772,
      lng: -126.214,
    },
    {
      lat: 50.772,
      lng: -100.214,
    },
    {
      lat: 34.772,
      lng: -91.214,
    },
    {
      lat: 44.772,
      lng: -90.214,
    },
    {
      lat: 34.772,
      lng: -80.214,
    },
    {
      lat: 54.772,
      lng: -98.214,
    },
  ];
}
