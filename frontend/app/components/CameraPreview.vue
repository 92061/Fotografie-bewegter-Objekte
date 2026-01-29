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
      <div class="absolute top-0 m-2 right-0 flex flex-row gap-2">
        <UButton
          icon="i-lucide-refresh-cw"
          size="sm"
          color="neutral"
          variant="soft"

          @click="refresh"
        />

        <UModal :ui="{ content: 'w-7/8 max-w-full h-7/8 max-h-full' }">
          <UButton
            icon="i-lucide-expand"
            size="sm"
            color="neutral"
            variant="soft"
          />

          <template #content>
            <NuxtImg
              :src="`${useRuntimeConfig().public.openFetch.api.baseURL}/Camera/LatestPhoto?${key}`"
            />
          </template>
        </UModal>
      </div>
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
  .withUrl(signalRUrl, {
    withCredentials: false
  })
  .configureLogging(LogLevel.Information)
  .build()

async function start() {
  await signalRConnection.start()
  console.log('SignalR Connected.')
}
signalRConnection.on('picture', refresh)

start()
</script>
