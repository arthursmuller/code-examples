// Action Types

export const Types = {
  FILTER_UPDATE: 'audit/FILTER_UPDATE',
};

// Reducer

const initialState = {
  filter: {
    start_date: null,
    end_date: null,
    user: null,
    text_search: '',
  },
  audits: [
    {
      activity: 'Lorem ipsum dolor sit amet',
      device: 'Alcatel v142',
      user: '5551982248385',
      date: '05/01/2021 16:41:10 UTC',
    },
    {
      activity: 'Lorem ipsum dolor sit amet',
      device: 'Alcatel v142',
      user: '5551982248385',
      date: '05/01/2021 16:41:20 UTC',
    },
  ],
};

const audit = (state = initialState, action) => {
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

export default audit;

// Action Creators

export function filterUpdate(data) {
  return {
    type: Types.FILTER_UPDATE,
    payload: data,
  };
}
