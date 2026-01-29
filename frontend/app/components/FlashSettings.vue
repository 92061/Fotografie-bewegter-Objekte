<template>
  <UPageCard
    title="Flash"
    icon="i-lucide-zap"
  >
    <UFormField label="GPIO Pin Number">
      <USelect
        v-model="pinNumber"
        :items="gpioPins"
        :disabled="busy"
        :loading="statusPin !== 'success'"
        class="w-full"
      />
    </UFormField>

    <UFormField label="Delay after trigger">
      <UInputNumber
        v-model="delayMs"
        :disabled="busy"
        :loading="statusDelay !== 'success'"
        :min="0"
        :format-options="{
          style: 'unit',
          unit: 'millisecond'
        }"
        class="w-full"
      />
    </UFormField>
    <UButton
      :loading="busy"
      @click="testFlash"
    >
      Flash!
    </UButton>
  </UPageCard>
</template>

<script setup lang="ts">
import type { SelectMenuItem } from '@nuxt/ui/components/SelectMenu.vue'

const { $api } = useNuxtApp()
const toast = useToast()

/**
 * Test flash
 */
const testFlash = async () => {
  busy.value = true
  try {
    await $api('/Flash/Flash', {
      method: 'POST'
    })
    toast.add({
      icon: 'i-lucide-zap',
      title: 'Flash!',
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-zap',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
}

/**
 * Busy configuring Flash
 */
const busy = ref(false)

/**
 * PinNumber data
 */
const {
  data: pin,
  status: statusPin,
  refresh: refreshPin
} = await useApi('/Flash/PinNumber')
const pinNumber = ref(pin.value)

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
    await $api('/Flash/PinNumber', {
      method: 'PATCH',
      body: newValue
    })
    await refreshPin()
    toast.add({
      icon: 'i-lucide-zap',
      title: 'Set Pin-Number!',
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-zap',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
})

/**
 * Flash delay data
 */
const {
  data: delay,
  status: statusDelay,
  refresh: refreshDelay
} = await useApi('/Flash/Delay')
const delayMs = ref(delay.value)
watch(delayMs, async (newValue) => {
  busy.value = true
  try {
    await $api('/Flash/Delay', {
      method: 'PATCH',
      body: newValue
    })
    await refreshDelay()

    toast.add({
      icon: 'i-lucide-zap',
      title: 'Set Delay!',
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-zap',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
})
</script>
