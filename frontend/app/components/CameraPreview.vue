<template>
  <UPageCard
    orientation="horizontal"
    title="Camera"
    icon="i-lucide-image"
  >
    <template #description>
      <CameraSettings />
    </template>
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
  </UPageCard>
</template>

<script setup lang="ts">
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'

const key = ref(0)
const refresh = () => {
  key.value = Date.now()
}

const signalRUrl = useRuntimeConfig().public.openFetch.api.baseURL + '/notifications'

const signalRConnection = new HubConnectionBuilder()
  .withUrl(signalRUrl)
  .configureLogging(LogLevel.Information)
  .build()

async function start() {
  await signalRConnection.start()
  console.log('SignalR Connected.')
}
signalRConnection.on('picture', refresh)

start()
</script>
