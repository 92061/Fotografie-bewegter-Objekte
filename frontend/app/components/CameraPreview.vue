<template>
  <UPageCard
    icon="i-lucide-image"
  >
    <div class="relative">
      <NuxtImg
        :src="`${useRuntimeConfig().public.openFetch.api.baseURL}/Camera/LatestPhoto?${key}`"
      />
      <UButton
        icon="i-lucide-refresh-cw"
        size="sm"
        color="neutral"
        variant="soft"
        class="absolute top-0 m-2 right-0"
        @click="refresh"
      />
    </div>
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

const refresh = () => {
  key.value = Date.now()
  console.debug(key.value)
}

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
    refresh()
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
