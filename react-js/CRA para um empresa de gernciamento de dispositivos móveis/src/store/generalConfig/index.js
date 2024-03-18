// Action Types

export const Types = {
  UPDATE_TAB: 'application/UPDATE_TAB',
  UPDATE_GENERAL: 'general_config/UPDATE',
  UPDATE_GENERAL_TOASTER: 'general_config/UPDATE_TOASTER',
  CREATE_GROUP: 'general_config/CREATE_GROUP',
  GROUP_TOASTER: 'general_config/GROUP_TOASTER',
  UPDATE_GROUP: 'general_config/UPDATE_GROUP',
  CREATE_SUBGROUP: 'general_config/CREATE_SUBGROUP',
  SUBGROUP_TOASTER: 'general_config/SUBGROUP_TOASTER',
  UPDATE_SUBGROUP: 'general_config/UPDATE_SUBGROUP',
  CREATE_USER: 'general_config/CREATE_USER',
  USER_TOASTER: 'general_config/USER_TOASTER',
  UPDATE_USER: 'general_config/UPDATE_USER',
};

// Reducer

const initialState = {
  selectedTab: 0,
  configGeneral: {
    company: '',
    cycle_start: '',
    sync_every: '',
    week_start: '',
    week_end: '',
    work_hour_start: '',
    work_hour_end: '',
    lock_outside_work_hours: false,
    gps_lock_outside_work_hours: false,
    gps_hour_start: '',
    gps_hour_end: '',
    gps_precision: '',
    locate_every: '',
    block_apps: false,
    block_sites: false,
    get_usage_time: false,
    block_url: false,
    total_block_apps: false,
    hotspot: false,
    warning_email: false,
    allow_safe_start: false,
    allow_add_user: false,
  },
  configGroup: {
    group: '',
    week_start: '',
    week_end: '',
    work_hour_start: '',
    work_hour_end: '',
    lock_outside_work_hours: '',
    track_gps: '',
    gps_hour_start: '',
    gps_hour_end: '',
    gps_precision: '',
    locate_every: '',
    block_apps: '',
    block_sites: '',
    get_usage_time: '',
    block_url: '',
    total_block_apps: '',
    hotspot: '',
    warning_email: '',
    allow_safe_start: '',
    allow_add_user: '',
    allow_sd_card: '',
  },
  configSubgroup: {
    subgroup: '',
    week_start: '',
    week_end: '',
    work_hour_start: '',
    work_hour_end: '',
    lock_outside_work_hours: '',
    track_gps: '',
    gps_hour_start: '',
    gps_hour_end: '',
    gps_precision: '',
    locate_every: '',
    block_apps: '',
    block_sites: '',
    get_usage_time: '',
    block_url: '',
    total_block_apps: '',
    hotspot: '',
    warning_email: '',
    allow_safe_start: '',
    allow_add_user: '',
    allow_sd_card: '',
  },
  configUser: {
    user: '',
    week_start: '',
    week_end: '',
    work_hour_start: '',
    work_hour_end: '',
    lock_outside_work_hours: '',
    track_gps: '',
    gps_hour_start: '',
    gps_hour_end: '',
    gps_precision: '',
    locate_every: '',
    block_apps: '',
    block_sites: '',
    get_usage_time: '',
    block_url: '',
    total_block_apps: '',
    hotspot: '',
    warning_email: '',
    allow_safe_start: '',
    allow_add_user: '',
    allow_sd_card: '',
  },
  showToasterGeneralEdit: false,
  showToasterGroupCreate: false,
  showToasterGroupEdit: false,
  showToasterSubgroupCreate: false,
  showToasterSubgroupEdit: false,
  showToasterUserCreate: false,
  showToasterUserEdit: false,
};

const generalConfig = (state = initialState, action) => {
  switch (action.type) {
    case Types.UPDATE_TAB:
      return {
        ...state,
        selectedTab: action.payload,
      };
    case Types.UPDATE_GENERAL:
      return {
        ...state,
        selectedTab: 0,
        configGeneral: action.payload,
        showToasterGeneralEdit: true,
      };
    case Types.UPDATE_GENERAL_TOASTER:
      return {
        ...state,
        showToasterGeneralEdit: action.payload,
      };
    case Types.CREATE_GROUP:
      return {
        ...state,
        selectedTab: 1,
        configGroup: action.payload,
        showToasterGroupCreate: true,
      };
    case Types.GROUP_TOASTER:
      return {
        ...state,
        showToasterGroupCreate: action.payload,
        showToasterGroupEdit: action.payload,
      };
    case Types.UPDATE_GROUP:
      return {
        ...state,
        selectedTab: 1,
        configGroup: action.payload,
        showToasterGroupEdit: true,
      };
    case Types.CREATE_SUBGROUP:
      return {
        ...state,
        selectedTab: 2,
        configSubgroup: action.payload,
        showToasterSubgroupCreate: true,
      };
    case Types.SUBGROUP_TOASTER:
      return {
        ...state,
        showToasterSubgroupCreate: action.payload,
        showToasterSubgroupEdit: action.payload,
      };
    case Types.UPDATE_SUBGROUP:
      return {
        ...state,
        selectedTab: 2,
        configSubgroup: action.payload,
        showToasterSubgroupEdit: true,
      };
    case Types.CREATE_USER:
      return {
        ...state,
        selectedTab: 3,
        configUser: action.payload,
        showToasterUserCreate: true,
      };
    case Types.USER_TOASTER:
      return {
        ...state,
        showToasterUserCreate: action.payload,
        showToasterUserEdit: action.payload,
      };
    case Types.UPDATE_USER:
      return {
        ...state,
        selectedTab: 3,
        configUser: action.payload,
        showToasterUserEdit: true,
      };
    default:
      return state;
  }
};

export default generalConfig;

// Action Creators

export function updateSelectedTab(index) {
  return {
    type: Types.UPDATE_TAB,
    payload: index,
  };
}

export function updateGeneral(data) {
  return {
    type: Types.UPDATE_GENERAL,
    payload: data,
  };
}

export function closeGeneralToaster() {
  return {
    type: Types.UPDATE_GENERAL_TOASTER,
    payload: false,
  };
}

export function createGroup(data) {
  return {
    type: Types.CREATE_GROUP,
    payload: data,
  };
}

export function updateGroup(data) {
  return {
    type: Types.UPDATE_GROUP,
    payload: data,
  };
}

export function closeGroupToaster() {
  return {
    type: Types.GROUP_TOASTER,
    payload: false,
  };
}

export function createSubgroup(data) {
  return {
    type: Types.CREATE_SUBGROUP,
    payload: data,
  };
}

export function updateSubgroup(data) {
  return {
    type: Types.UPDATE_SUBGROUP,
    payload: data,
  };
}

export function closeSubgroupToaster() {
  return {
    type: Types.SUBGROUP_TOASTER,
    payload: false,
  };
}

export function createUser(data) {
  return {
    type: Types.CREATE_USER,
    payload: data,
  };
}

export function updateUser(data) {
  return {
    type: Types.UPDATE_USER,
    payload: data,
  };
}

export function closeUserToaster() {
  return {
    type: Types.USER_TOASTER,
    payload: false,
  };
}
