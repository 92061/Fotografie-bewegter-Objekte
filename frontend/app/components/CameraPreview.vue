<template>
  <UPageCard
    title="Camera"
  >
    <template #leading>
      <UIcon
        name="i-lucide-image"
        :class="['size-5 shrink-0 text-primary', snap ? 'animate-wiggle' : '']"
      />
    </template>
    <div :class="['relative rounded-md', newPicture ? 'animate-highlight' : '']">
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
    <CameraControls />
  </UPageCard>
</template>

<script setup lang="ts">
const key = ref(0)
const refresh = () => {
  key.value = Date.now()
}

const snap = ref(false)
useSignalR().addCallback('snap', () => {
  snap.value = true
  setTimeout(() => snap.value = false, 250)
})
const newPicture = ref(false)
useSignalR().addCallback('picture', () => {
  refresh()
  newPicture.value = true
  setTimeout(() => newPicture.value = false, 250)
})
</script>
