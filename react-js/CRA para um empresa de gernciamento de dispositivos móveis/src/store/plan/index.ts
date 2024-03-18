import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { PlanType } from '../../types/plan';

// Action Types

export const Types = {
  LIST: 'plan/LIST',
};

// Reducer

interface PlansState {
  plans: PlanType[];
  errors: Error | unknown;
}

const initialState: PlansState = {
  plans: [],
  errors: {},
};

const plansSlice = createSlice({
  name: 'plans',
  initialState,
  reducers: {
    planError: (plan, action: PayloadAction<Error | unknown>) => {
      plan.errors = action.payload;
    },
    planListSuccess: (plan, action: PayloadAction<PlanType[]>) => {
      plan.plans = action.payload;
      plan.errors = initialState.errors;
    },
  },
});

export default plansSlice.reducer;

// Action Creators

export const {
  planError,

  planListSuccess,
} = plansSlice.actions;

export function listPlans() {
  return {
    type: Types.LIST,
  };
}
