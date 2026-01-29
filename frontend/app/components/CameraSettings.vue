<template>
  <div class="flex flex-col gap-2">
    <UFormField label="Delay after trigger">
      <UInputNumber
        v-model="delayMs"
        :disabled="busy"
        :loading="statusDelay !== 'success'"
        :min="0"
        class="w-full"
      />
    </UFormField>

    <UButton @click="testCamera">
      Cheese!
    </UButton>
  </div>
</template>

<script setup lang="ts">
const { $api } = useNuxtApp()
const toast = useToast()

/**
 * Test Camera
 */
const testCamera = async () => {
  busy.value = true
  try {
    await $api('/Camera/TakePicture', {
      method: 'POST'
    })
    toast.add({
      icon: 'i-lucide-camera',
      title: 'Picture taken!',
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-camera',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
}

/**
 * Busy configuring Camera
 */
const busy = ref(false)

/**
 * Camera delay data
 */
const delayMs = ref(0)
const {
  data: delay,
  status: statusDelay,
  refresh: refreshDelay
} = await useApi('/Camera/Delay')
watch(delay, (data) => {
  if (data) delayMs.value = data
})
watch(delayMs, async (newValue) => {
  busy.value = true
  try {
    await $api('/Camera/Delay', {
      method: 'PATCH',
      body: newValue
    })
    await refreshDelay()

    toast.add({
      icon: 'i-lucide-camera',
      title: 'Set Delay!',
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-camera',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
})
</script>
