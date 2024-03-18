// Action Types

export const Types = {
  UPDATE_TAB: 'consumption_profile/UPDATE_TAB',
  UPDATE_GENERAL: 'consumption_profile/general/UPDATE',
  UPDATE_GENERAL_SUCCESS: 'consumption_profile/general/UPDATE_SUCCESS',
  UPDATE_GENERAL_ERROR: 'consumption_profile/general/UPDATE_ERROR',
  TOASTER_GENERAL: 'consumption_profile/general/TOASTER',
  CREATE_GROUP: 'consumption_profile/group/CREATE',
  CREATE_GROUP_SUCCESS: 'consumption_profile/group/CREATE_SUCCESS',
  CREATE_GROUP_ERROR: 'consumption_profile/group/CREATE_ERROR',
  TOASTER_GROUP: 'consumption_profile/group/TOASTER',
  CREATE_SUBGROUP: 'consumption_profile/subgroup/CREATE',
  CREATE_SUBGROUP_SUCCESS: 'consumption_profile/subgroup/CREATE_SUCCESS',
  CREATE_SUBGROUP_ERROR: 'consumption_profile/subgroup/CREATE_ERROR',
  CREATE_USER: 'consumption_profile/user/CREATE',
  CREATE_USER_SUCCESS: 'consumption_profile/user/CREATE_SUCCESS',
  CREATE_USER_ERROR: 'consumption_profile/user/CREATE_ERROR',
};

// Reducer

const initialState = {
  selectedTab: 0,
  general: {
    general: {
      dataQuantity: '',
      dataBytes: '',
      smsMessages: '',
      dataRoamingQuantity: '',
      dataRoamingBytes: '',
      smsRoamingMessages: '',
    },
    errors: {},
    showToaster: false,
  },
  groups: {
    groups: [
      {
        id: 1,
        company: 'Teste QA TELCEL',
        group: 'QA',
        data: 'Def no Grupo/Empresa',
        roaming_data: 'Def no Grupo/Empresa',
        sms: 'Def no Grupo/Empresa',
        roaming_sms: 'Def no Grupo/Empresa',
      },
      {
        id: 2,
        company: 'Teste QA TELCEL',
        group: 'Sup',
        data: 'Ilimitado',
        roaming_data: 'Ilimitado',
        sms: 'Ilimitado',
        roaming_sms: 'Ilimitado',
      },
    ],
    group: {},
    errors: {},
    showToaster: false,
  },
  subgroups: {
    subgroups: [
      {
        id: 1,
        company: 'Teste QA TELCEL',
        subgroup: 'QA',
        data: 'Def no Grupo/Empresa',
        roaming_data: 'Def no Grupo/Empresa',
        sms: 'Def no Grupo/Empresa',
        roaming_sms: 'Def no Grupo/Empresa',
      },
      {
        id: 2,
        company: 'Teste QA TELCEL',
        subgroup: 'Sup',
        data: 'Ilimitado',
        roaming_data: 'Ilimitado',
        sms: 'Ilimitado',
        roaming_sms: 'Ilimitado',
      },
    ],
    group: {},
    errors: {},
    showToaster: false,
  },
  users: {
    users: [
      {
        id: 1,
        company: 'Teste QA TELCEL',
        user: 'Huawei emui',
        phone: '5551983443668',
        data: 'Def no Grupo/Empresa',
        roaming_data: 'Def no Grupo/Empresa',
        sms: 'Def no Grupo/Empresa',
        roaming_sms: 'Def no Grupo/Empresa',
      },
      {
        id: 2,
        company: 'Teste QA TELCEL',
        user: 'Huawei Y6',
        phone: '5551983443333',
        data: 'Ilimitado',
        roaming_data: 'Ilimitado',
        sms: 'Ilimitado',
        roaming_sms: 'Ilimitado',
      },
    ],
    user: {},
    errors: {},
    showToaster: false,
  },
};

const consumptionProfile = (state = initialState, action) => {
  switch (action.type) {
    case Types.UPDATE_TAB:
      return {
        ...state,
        selectedTab: action.payload,
      };
    case Types.CREATE_GROUP_SUCCESS:
      return {
        ...state,
        selectedTab: 1,
        groups: {
          ...state.groups,
          group: action.payload,
          showToaster: true,
        },
      };
    case Types.CREATE_GROUP_ERROR:
      return {
        ...state,
        groups: {
          ...state.groups,
          errors: action.payload,
        },
      };
    case Types.TOASTER_GROUP:
      return {
        ...state,
        groups: {
          ...state.groups,
          showToaster: action.payload,
        },
      };
    case Types.UPDATE_GENERAL_SUCCESS:
      return {
        ...state,
        selectedTab: 0,
        general: {
          general: action.payload,
          showToaster: true,
        },
      };
    case Types.UPDATE_GENERAL_ERROR:
      return {
        ...state,
        general: {
          ...state.general,
          errors: action.payload,
        },
      };
    case Types.CREATE_SUBGROUP_SUCCESS:
      return {
        ...state,
        selectedTab: 2,
        subgroups: {
          ...state.subgroups,
          subgroup: action.payload,
          showToaster: true,
        },
      };
    case Types.CREATE_SUBGROUP_ERROR:
      return {
        ...state,
        subgroups: {
          ...state.subgroups,
          errors: action.payload,
        },
      };
    case Types.CREATE_USER_SUCCESS:
      return {
        ...state,
        selectedTab: 3,
        users: {
          ...state.users,
          user: action.payload,
          showToaster: true,
        },
      };
    case Types.CREATE_USER_ERROR:
      return {
        ...state,
        users: {
          ...state.users,
          errors: action.payload,
        },
      };
    default:
      return state;
  }
};

export default consumptionProfile;

// Action Creators

export function updateSelectedTab(index) {
  return {
    type: Types.UPDATE_TAB,
    payload: index,
  };
}

export function showGroupsToaster(data) {
  return {
    type: Types.TOASTER_GROUP,
    payload: data,
  };
}

export function createGroup(data) {
  return {
    type: Types.CREATE_GROUP,
    payload: data,
  };
}

export function groupCreationSuccess(data) {
  return {
    type: Types.CREATE_GROUP_SUCCESS,
    payload: data,
    success: true,
  };
}

export function groupCreationError(errors) {
  return {
    type: Types.CREATE_GROUP_ERROR,
    payload: errors,
  };
}

export function updateGeneral(data) {
  return {
    type: Types.UPDATE_GENERAL,
    payload: data,
  };
}

export function generalUpdateSuccess(data) {
  return {
    type: Types.UPDATE_GENERAL_SUCCESS,
    payload: data,
    success: true,
  };
}

export function generalUpdateError(errors) {
  return {
    type: Types.UPDATE_GENERAL_ERROR,
    payload: errors,
  };
}

export function createSubgroup(data) {
  return {
    type: Types.CREATE_SUBGROUP,
    payload: data,
  };
}

export function subgroupCreationSuccess(data) {
  return {
    type: Types.CREATE_SUBGROUP_SUCCESS,
    payload: data,
    success: true,
  };
}

export function subgroupCreationError(errors) {
  return {
    type: Types.CREATE_SUBGROUP_ERROR,
    payload: errors,
  };
}

export function createUser(data) {
  return {
    type: Types.CREATE_USER,
    payload: data,
  };
}

export function userCreationSuccess(data) {
  return {
    type: Types.CREATE_USER_SUCCESS,
    payload: data,
    success: true,
  };
}

export function userCreationError(errors) {
  return {
    type: Types.CREATE_USER_ERROR,
    payload: errors,
  };
}

export function showToaster(data) {
  return {
    type: Types.TOASTER,
    payload: data,
  };
}

export function closeGeneralToaster() {
  return {
    type: Types.TOASTER_GENERAL,
    payload: false,
  };
}
