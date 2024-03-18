// Action Types

export const Types = {
  FILTER_UPDATE: 'reportDate/FILTER_UPDATE',
};

// Reducer

const initialState = {
  filter: {
    date: null,
    user_id: '',
  },
  records: [
    {
      title: '18/01/2021',
      items: [
        {
          date: '18:58:19 UTC',
          label: 'google.com',
        },
        {
          date: '18:58:19 UTC',
          label: 'google.com',
        },
        {
          date: '18:58:19 UTC',
          label: 'facebook.com',
        },
      ],
    },
    {
      title: '18/01/2021',
      items: [
        {
          date: '18:58:19 UTC',
          label: 'google.com',
          markColor: '#e69395',
        },
        {
          date: '18:58:19 UTC',
          label: 'google.com',
        },
        {
          date: '18:58:19 UTC',
          label: 'facebook.com',
        },
      ],
    },
  ],
};

const reportDate = (state = initialState, action) => {
  switch (action.type) {
    case Types.FILTER_UPDATE:
      return {
        ...state,
        filter: action.payload,
      };
    default:
      return state;
  }
};

export default reportDate;

// Action Creator

export function filterUpdate(data) {
  return {
    type: Types.FILTER_UPDATE,
    payload: data,
  };
}
