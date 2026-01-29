import type { HubConnection } from '@microsoft/signalr'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

const signalRConnection = ref<HubConnection>()

async function start(signalRConnection: HubConnection) {
  await signalRConnection.start()
  console.log('SignalR Connected.')
}

export function useSignalR() {
  if (!signalRConnection.value) {
    const signalRUrl = useRuntimeConfig().public.openFetch.api.baseURL + '/notifications'

    signalRConnection.value = new HubConnectionBuilder()
      .withUrl(signalRUrl, {
        withCredentials: false
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build()

    const toast = useToast()
    signalRConnection.value.onreconnecting(() => toast.add({
      icon: 'i-lucide-wifi-sync',
      title: 'Reconnecting SignalR...',
      color: 'warning'
    }))
    signalRConnection.value.onreconnected(() => toast.add({
      icon: 'i-lucide-wifi',
      title: 'SignalR reconnected!',
      color: 'success'
    }))

    start(signalRConnection.value)
  }

  const addCallback = (topic: string, callback: () => void) => {
    signalRConnection.value?.on(topic, callback)
  }

  return { addCallback }
}
