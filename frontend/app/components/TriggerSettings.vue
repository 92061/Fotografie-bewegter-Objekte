<template>
  <UPageCard
    title="Trigger"
    icon="i-lucide-siren"
  >
    <USelect
      v-model="pinNumber"
      :items="gpioPins"
      :disabled="busy"
      :loading="statusPin !== 'success'"
    />
  </UPageCard>
</template>

<script setup lang="ts">
import type { SelectMenuItem } from '@nuxt/ui/components/SelectMenu.vue'

const { $api } = useNuxtApp()
const toast = useToast()

/**
 * Busy configuring Trigger
 */
const busy = ref(false)

/**
 * PinNumber data
 */
const pinNumber = ref(0)
const {
  data: pin,
  status: statusPin,
  refresh: refreshPin
} = await useApi('/Trigger/PinNumber')
watch(pin, (data) => {
  if (data) pinNumber.value = data
})

const gpioPins: SelectMenuItem[] = [...Array(27).keys()].map((i) => {
  return {
    label: `GPIO ${i}`,
    type: 'item',
    value: i
  }
})
watch(pinNumber, async (newValue) => {
  busy.value = true
  try {
    await $api('/Trigger/PinNumber', {
      method: 'PATCH',
      body: newValue
    })
    await refreshPin()
    toast.add({
      icon: 'i-lucide-siren',
      title: 'Set Pin-Number!',
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-siren',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
})
</script>
