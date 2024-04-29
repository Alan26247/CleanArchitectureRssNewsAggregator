import { configureStore } from '@reduxjs/toolkit'
import infoReducer from 'features/info/infoSlice'
import channelReducer from 'features/channel/channelSlice'
import spinnerReducer from 'features/spinner/spinnerSlice'

export const store = configureStore({
  reducer: {
    info: infoReducer,
    channel: channelReducer,
    spinner: spinnerReducer,
  },
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch