// Action Types

export const Types = {
  UPDATE_TAB: 'sites/UPDATE_TAB',
  UPDATE_GENERAL_CATEGORY: 'sites/UPDATE_GENERAL_CATEGORY',
  UPDATE_GENERAL_TOASTER: 'sites/UPDATE_GENERAL_TOASTER',
  UPDATE_GROUPS_CATEGORY: 'sites/UPDATE_GROUPS_CATEGORY',
  UPDATE_GROUPS_TOASTER: 'sites/UPDATE_GROUPS_TOASTER',
  UPDATE_SUBGROUPS_CATEGORY: 'sites/UPDATE_SUBGROUPS_CATEGORY',
  UPDATE_SUBGROUPS_TOASTER: 'sites/UPDATE_SUBGROUPS_TOASTER',
  UPDATE_USERS_CATEGORY: 'sites/UPDATE_USERS_CATEGORY',
  UPDATE_USERS_TOASTER: 'sites/UPDATE_USERS_TOASTER',

  KIOSK_UPDATE_GENERAL: 'sites/kiosk/UPDATE_GENERAL',
  KIOSK_UPDATE_GENERAL_TOASTER: 'sites/kiosk/UPDATE_GENERAL_TOASTER',
  KIOSK_UPDATE_GROUPS: 'sites/kiosk/UPDATE_GROUPS',
  KIOSK_UPDATE_GROUPS_TOASTER: 'sites/kiosk/UPDATE_GROUPS_TOASTER',
  KIOSK_UPDATE_SUBGROUPS: 'sites/kiosk/UPDATE_SUBGROUPS',
  KIOSK_UPDATE_SUBGROUPS_TOASTER: 'sites/kiosk/UPDATE_SUBGROUPS_TOASTER',
  KIOSK_UPDATE_USERS: 'sites/kiosk/UPDATE_USERS',
  KIOSK_UPDATE_USERS_TOASTER: 'sites/kiosk/UPDATE_USERS_TOASTER',
};

// Reducer

const categories = {
  education: true,
  government: false,
  entertainment: false,
  search_portal: false,
  news: true,
  sports: false,
  business: false,
  health: false,
  games: false,
  tech: false,
  trips: false,
  shopping: true,
  job: false,
  email: false,
  forums: false,
  social_media: false,
  chat: false,
  buy_files: false,
  gambling: false,
  proxies: false,
  violence: false,
  rudeness: false,
  adult_content: false,
  alcohol: false,
  drugs: false,
  tobacco: false,
};

const kioskActions = {
  accessibility: false,
  actions_moto: false,
  software_update: false,
  administrador: false,
  schedule: false,
  settings: false,
  app_update: false,
  notepad: false,
  bubble_bash: false,
  calculator: false,
  calendar: false,
  capture: false,
  catalog: false,
  update_center: false,
  subscription_center: false,
  chrome: false,
};

const initialState = {
  selectedTab: 0,
  block_categories: {
    general: {
      showToaster: false,
      ...categories,
    },
    groups: {
      group: '',
      showToaster: false,
      ...categories,
    },
    subgroups: {
      showToaster: false,
      ...categories,
    },
    users: {
      showToaster: false,
      ...categories,
    },
  },
  kiosk_mode: {
    general: {
      showToaster: false,
      ...kioskActions,
    },
    groups: {
      showToaster: false,
      ...kioskActions,
    },
    subgroups: {
      showToaster: false,
      ...kioskActions,
    },
    users: {
      showToaster: false,
      ...kioskActions,
    },
  },
};

const site = (state = initialState, action) => {
  switch (action.type) {
    case Types.UPDATE_TAB:
      return {
        ...state,
        selectedTab: action.payload,
      };
    case Types.UPDATE_GENERAL_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        block_categories: {
          ...state.block_categories,
          general: {
            ...state.block_categories.general,
            showToaster: action.payload,
          },
        },
      };
    case Types.UPDATE_GENERAL_CATEGORY:
      return {
        ...state,
        block_categories: {
          ...state.block_categories,
          general: {
            ...state.block_categories.general,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.UPDATE_GROUPS_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        block_categories: {
          ...state.block_categories,
          groups: {
            ...state.block_categories.groups,
            showToaster: action.payload,
          },
        },
      };
    case Types.UPDATE_GROUPS_CATEGORY:
      return {
        ...state,
        block_categories: {
          ...state.block_categories,
          groups: {
            ...state.block_categories.groups,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.UPDATE_SUBGROUPS_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        block_categories: {
          ...state.block_categories,
          subgroups: {
            ...state.block_categories.subgroups,
            showToaster: action.payload,
          },
        },
      };
    case Types.UPDATE_SUBGROUPS_CATEGORY:
      return {
        ...state,
        block_categories: {
          ...state.block_categories,
          subgroups: {
            ...state.block_categories.subgroups,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.UPDATE_USERS_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        block_categories: {
          ...state.block_categories,
          users: {
            ...state.block_categories.users,
            showToaster: action.payload,
          },
        },
      };
    case Types.UPDATE_USERS_CATEGORY:
      return {
        ...state,
        block_categories: {
          ...state.block_categories,
          users: {
            ...state.block_categories.users,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.KIOSK_UPDATE_GENERAL_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        kiosk_mode: {
          ...state.kiosk_mode,
          general: {
            ...state.kiosk_mode.general,
            showToaster: action.payload,
          },
        },
      };
    case Types.KIOSK_UPDATE_GENERAL:
      return {
        ...state,
        kiosk_mode: {
          ...state.kiosk_mode,
          general: {
            ...state.kiosk_mode.general,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.KIOSK_UPDATE_GROUPS_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        kiosk_mode: {
          ...state.kiosk_mode,
          groups: {
            ...state.kiosk_mode.groups,
            showToaster: action.payload,
          },
        },
      };
    case Types.KIOSK_UPDATE_GROUPS:
      return {
        ...state,
        kiosk_mode: {
          ...state.kiosk_mode,
          groups: {
            ...state.kiosk_mode.groups,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.KIOSK_UPDATE_SUBGROUPS_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        kiosk_mode: {
          ...state.kiosk_mode,
          subgroups: {
            ...state.kiosk_mode.subgroups,
            showToaster: action.payload,
          },
        },
      };
    case Types.KIOSK_UPDATE_SUBGROUPS:
      return {
        ...state,
        kiosk_mode: {
          ...state.kiosk_mode,
          subgroups: {
            ...state.kiosk_mode.subgroups,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    case Types.KIOSK_UPDATE_USERS_TOASTER:
      return {
        ...state,
        selectedTab: action.payload,
        kiosk_mode: {
          ...state.kiosk_mode,
          users: {
            ...state.kiosk_mode.users,
            showToaster: action.payload,
          },
        },
      };
    case Types.KIOSK_UPDATE_USERS:
      return {
        ...state,
        kiosk_mode: {
          ...state.kiosk_mode,
          users: {
            ...state.kiosk_mode.users,
            ...action.payload,
            showToaster: true,
          },
        },
      };
    default:
      return state;
  }
};

export default site;

// Action Creators

export function updateSelectedTab(index) {
  return {
    type: Types.UPDATE_TAB,
    payload: index,
  };
}

export function closeGeneralToaster() {
  return {
    type: Types.UPDATE_GENERAL_TOASTER,
    payload: false,
  };
}

export function updateGeneralCategory(data) {
  return {
    type: Types.UPDATE_GENERAL_CATEGORY,
    payload: data,
  };
}

export function closeGroupToaster() {
  return {
    type: Types.UPDATE_GROUPS_TOASTER,
    payload: false,
  };
}

export function updateGroupCategory(data) {
  return {
    type: Types.UPDATE_GROUPS_CATEGORY,
    payload: data,
  };
}

export function closeSubgroupToaster() {
  return {
    type: Types.UPDATE_SUBGROUPS_TOASTER,
    payload: false,
  };
}

export function updateSubgroupCategory(data) {
  return {
    type: Types.UPDATE_SUBGROUPS_CATEGORY,
    payload: data,
  };
}

export function closeUsersToaster() {
  return {
    type: Types.UPDATE_USERS_TOASTER,
    payload: false,
  };
}

export function updateUsersCategory(data) {
  return {
    type: Types.UPDATE_USERS_CATEGORY,
    payload: data,
  };
}

export function kioskCloseGeneralToaster() {
  return {
    type: Types.KIOSK_UPDATE_GENERAL_TOASTER,
    payload: false,
  };
}

export function kioskUpdateGeneral(data) {
  return {
    type: Types.KIOSK_UPDATE_GENERAL,
    payload: data,
  };
}

export function kioskCloseGroupToaster() {
  return {
    type: Types.KIOSK_UPDATE_GROUPS_TOASTER,
    payload: false,
  };
}

export function kioskUpdateGroup(data) {
  return {
    type: Types.KIOSK_UPDATE_GROUPS,
    payload: data,
  };
}

export function kioskCloseSubgroupToaster() {
  return {
    type: Types.KIOSK_UPDATE_SUBGROUPS_TOASTER,
    payload: false,
  };
}

export function kioskUpdateSubgroup(data) {
  return {
    type: Types.KIOSK_UPDATE_SUBGROUPS,
    payload: data,
  };
}

export function kioskCloseUsersToaster() {
  return {
    type: Types.KIOSK_UPDATE_USERS_TOASTER,
    payload: false,
  };
}

export function kioskUpdateUsers(data) {
  return {
    type: Types.KIOSK_UPDATE_USERS,
    payload: data,
  };
}
