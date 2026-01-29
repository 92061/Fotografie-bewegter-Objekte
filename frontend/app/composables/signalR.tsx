import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr'

const signalRConnection = ref<HubConnection>();

async function start(signalRConnection : HubConnection) {
    await signalRConnection.start()
    console.log('SignalR Connected.')
}

export function useSignalR() {
    
    if (!signalRConnection.value){
        const signalRUrl = useRuntimeConfig().public.openFetch.api.baseURL + '/notifications'
        
        signalRConnection.value = new HubConnectionBuilder()
            .withUrl(signalRUrl, {
                withCredentials: false
            })
            .configureLogging(LogLevel.Information)
            .build()

        start(signalRConnection.value);
    }

    
  const addCallback = (topic: string, callback: () => void) => {
    signalRConnection.value?.on(topic, callback)
  }
  
  return { addCallback }
}