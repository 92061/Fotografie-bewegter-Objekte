import type { HubConnection } from '@microsoft/signalr'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

const signalRConnection = ref<HubConnection>()

async function start(signalRConnection: HubConnection) {
  const toast = useToast()
  const { t } = useI18n()
  await signalRConnection.start()
  toast.add({
    icon: 'i-lucide-wifi',
    title: t('signalR.connected'),
    color: 'success'
  })
}

export function useSignalR() {
  const toast = useToast()
  const { t } = useI18n()
  if (!signalRConnection.value) {
    const signalRUrl = useRuntimeConfig().public.openFetch.api.baseURL + '/notifications'

    signalRConnection.value = new HubConnectionBuilder()
      .withUrl(signalRUrl, {
        withCredentials: false
      })
      .configureLogging(LogLevel.Information)
      .withAutomaticReconnect()
      .build()

    signalRConnection.value.onreconnecting(() => toast.add({
      icon: 'i-lucide-wifi-sync',
      title: t('signalR.reconnecting'),
      color: 'warning'
    }))
    signalRConnection.value.onreconnected(() => toast.add({
      icon: 'i-lucide-wifi',
      title: t('signalR.reconnected'),
      color: 'success'
    }))

    start(signalRConnection.value)
  }

  const addCallback = (topic: string, callback: () => void) => {
    signalRConnection.value?.on(topic, callback)
  }

  return { addCallback }
}
