import axios from 'axios'

import { apiUrl } from './env.config'
import qs from 'qs'

export const initHeaders = {
  Accept: 'application/ld+json',
  'Content-Type': 'application/json; charset=utf-8',
  'Cache-Control': 'no-cache',
  'Access-Control-Allow-Origin': 'http://localhost:3000',
  'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, DELETE, PUT',
  'Access-Control-Allow-Headers': 'Content-Type',
}

axios.defaults.withCredentials = true

export const httpCore = axios.create({
  baseURL: apiUrl,
  paramsSerializer: (params) => qs.stringify(params, { arrayFormat: 'repeat' }),
  timeout: 60 * 1000,
  headers: initHeaders,
})

export const httpCoreMultipart = axios.create({
  baseURL: apiUrl,
  timeout: 180 * 1000,
  headers: {
    'Content-Type': 'multipart/form-data',
    Accept: '*/*',
  },
})

//@ts-ignore
httpCore.interceptors.request.use((config) => {
  if (!config.headers) return undefined
  return config
})
httpCore.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.code === "ERR_NETWORK") {
      // store.dispatch({
      //   type: 'snack/showSnack',
      //   payload: {
      //     messageSnack: 'Ошибка загрузки данных',
      //     typeSnack: 'error',
      //   },
      // })
    }

    return Promise.reject(error)
  }
)

// @ts-ignore
httpCoreMultipart.interceptors.request.use((config) => {
  if (!config.headers) return undefined
  return config
})

httpCoreMultipart.interceptors.response.use(
  (response) => response,
  (error) => {
    return Promise.reject(error)
  }
)
