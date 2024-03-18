import { v4 as uuidv4 } from 'uuid';

import { useFeatureFlags } from 'app';
import { menuItems } from 'features/main/consts/menu-items';
import { SearchItemModel } from '@pcf/design-system';
import { useRefinanciamentoEnabled } from 'features/main/features/simulacoes/features/refinanciamento';

interface useAvaivalableMainItemsData {
  availableMainItems: SearchItemModel[];
  isLoading: boolean;
}

export function useAvaivalableMainItems(): useAvaivalableMainItemsData {
  const { flags } = useFeatureFlags();
  const { canAccess } = useRefinanciamentoEnabled();

  const availableMainItems = menuItems
    .filter((item) => {
      if (item.flagKey) {
        const hasAccess = flags && flags[item.flagKey];
        return hasAccess;
      }

      return true;
    })
    .map((item) => {
      return {
        id: uuidv4(),
        title: item.label,
        description: `Ir para ${item.label}`,
        category: 'Ação',
        icon: item.icon,
        route: item.route,
      };
    });

  return {
    availableMainItems: canAccess
      ? availableMainItems
      : availableMainItems.filter(
          (item) => item.title !== 'Refinanciar Consignado',
        ),
    isLoading: false,
  };
}
