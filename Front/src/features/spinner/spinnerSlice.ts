import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export interface SpinnerState {
  isVisible: boolean,
}

const initialState: SpinnerState = {
  isVisible: false,
}

export const spinnerSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    setSpinner: (state, action: PayloadAction<boolean>) => {
      state.isVisible = action.payload;
    },
  },
})

export const { setSpinner } = spinnerSlice.actions

export default spinnerSlice.reducer