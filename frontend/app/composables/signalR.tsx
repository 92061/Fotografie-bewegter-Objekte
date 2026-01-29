import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

export function useSignalR() {
  const signalRUrl = useRuntimeConfig().public.openFetch.api.baseURL + '/notifications'

  const signalRConnection = new HubConnectionBuilder()
      .withUrl(signalRUrl, {
        withCredentials: false
      })
      .configureLogging(LogLevel.Information)
      .build()

  async function start() {
    await signalRConnection.start()
    console.log('SignalR Connected.')
  }

  const addCallback = (topic: string, callback: () => void) => {
    signalRConnection.on(topic, callback)
  }
  
  start();
  
  return { addCallback }
}
