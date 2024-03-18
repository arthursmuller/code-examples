// Action Types

export const Types = {
  CREATE: 'geofence/CREATE',
  CREATE_TOASTER: 'geofence/CREATE_TOASTER',
  UPDATE: 'geofence/UPDATE',
  UPDATE_TOASTER: 'geofence/UPDATE_TOASTER',
};

// Reducer

const initialState = {
  geofence: { name: '', adress: '', radius: '' },
  showToasterAdd: false,
  showToasterEdit: false,
};

const geofence = (state = initialState, action) => {
  switch (action.type) {
    case Types.CREATE_TOASTER:
      return {
        ...state,
        showToasterAdd: action.payload,
      };
    case Types.CREATE:
      return {
        ...state,
        geofence: action.payload,
        showToasterAdd: true,
      };
    case Types.UPDATE_TOASTER:
      return {
        ...state,
        showToasterEdit: action.payload,
      };
    case Types.UPDATE:
      return {
        ...state,
        geofence: action.payload,
        showToasterEdit: true,
      };
    default:
      return state;
  }
};

export default geofence;

// Action Creators

export function closeCreateToaster() {
  return {
    type: Types.CREATE_TOASTER,
    payload: false,
  };
}

export function createGeofence(data) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function closeUpdateToaster() {
  return {
    type: Types.UPDATE_TOASTER,
    payload: false,
  };
}

export function updateGeofence(data) {
  return {
    type: Types.UPDATE,
    payload: data,
  };
}
