import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export interface ChannelState {
  channelId: number | null,
  channelTitle: string | null,
}

const initialState: ChannelState = {
  channelId: null,
  channelTitle: 'Все каналы',
}

export const channelSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    changeChannel: (state, action: PayloadAction<ChannelState>) => {
      state.channelId = action.payload.channelId;
      state.channelTitle = action.payload.channelTitle;
    },
  },
})

export const { changeChannel } = channelSlice.actions

export default channelSlice.reducer