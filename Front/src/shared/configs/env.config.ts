const getEnvironmentVariables = () => {
  return {
    apiUrl: process.env.REACT_APP_API_URL,
  } as const
}

export const {
  apiUrl,
} = getEnvironmentVariables()
