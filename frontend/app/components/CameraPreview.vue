<template>
  <UPageCard>
    <NuxtImg
      :key="key"
      :src="`${useRuntimeConfig().public.openFetch.api.baseURL}/Camera/LatestPhoto`"
    />
    <UButton
      :disabled="ws.readyState === ws.OPEN"
      @click="connect"
    >
      Connect
    </UButton>
  </UPageCard>
</template>

<script setup lang="ts">
const toast = useToast()

const key = ref(Date.now())

const webSocketAddress
  = useRuntimeConfig().public.openFetch.api.baseURL.replace('http://', 'ws://').replace('https://', 'wss://') + '/ws'

const ws = ref(new WebSocket(webSocketAddress))

const connect = () => {
  ws.value = new WebSocket(webSocketAddress)

  ws.value.onopen = () => {
    toast.add({
      icon: 'i-lucide-image',
      title: 'Connected to Websocket!',
      color: 'success'
    })
  }

  ws.value.onmessage = () => {
    console.debug('Websocket update!')
    key.value = Date.now()
  }

  ws.value.onerror = () => {
    toast.add({
      icon: 'i-lucide-image',
      title: 'Error',
      color: 'error'
    })
  }

  ws.value.onclose = () => {
    toast.add({
      icon: 'i-lucide-image',
      title: 'Disconnected from Websocket!',
      color: 'warning'
    })
  }
}
</script>
