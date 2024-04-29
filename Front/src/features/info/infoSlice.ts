import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export interface InfoMessage {
  isSuccess: boolean,
  text: string,
}

export interface InfoState {
  isVisible: boolean,
  isSuccess: boolean,
  text: string,
}

const initialState: InfoState = {
  isVisible: false,
  isSuccess: false, 
  text: '',
}

export const infoSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    outInfo: (state, action: PayloadAction<InfoMessage>) => {
      state.isVisible = true;
      state.isSuccess = action.payload.isSuccess;
      state.text = action.payload.text;
    },
    closeInfo: (state) => {
      state.isVisible = false;
    },
  },
})

export const { outInfo, closeInfo } = infoSlice.actions

export default infoSlice.reducer