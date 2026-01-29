import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

class SignalR {
  public constructor() {
    this.start()
  }

  signalRUrl = useRuntimeConfig().public.openFetch.api.baseURL + '/notifications'

  signalRConnection = new HubConnectionBuilder()
    .withUrl(this.signalRUrl, {
      withCredentials: false
    })
    .configureLogging(LogLevel.Information)
    .build()

  private async start() {
    await this.signalRConnection.start()
    console.log('SignalR Connected.')
  }

  addCallback(topic: string, callback: () => void) {
    this.signalRConnection.on(topic, callback)
  }
}

const signalR = new SignalR()

export function useSignalR() {
  return signalR
}
