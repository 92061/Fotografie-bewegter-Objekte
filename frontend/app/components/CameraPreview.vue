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
const key = ref(0)
const refresh = () => {
  key.value = Date.now()
}

useSignalR().addCallback('picture', refresh)
</script>
