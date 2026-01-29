<template>
  <UPageCard
    :title="$t('trigger.title')"
  >
    <template #leading>
      <UIcon
        name="i-lucide-siren"
        :class="['size-5 shrink-0 text-primary', triggered ? 'animate-wiggle' : '']"
      />
    </template>
    <UFormField>
      <template #label>
        <ULink to="https://pinout.xyz/">{{ $t('trigger.pin') }}</ULink>
      </template>
      <USelect
        v-model="pinNumber"
        :items="gpioPins"
        :disabled="busy"
        :loading="statusPin !== 'success'"
        class="w-full"
      />
    </UFormField>
  </UPageCard>
</template>

<script setup lang="ts">
import type { SelectMenuItem } from '@nuxt/ui/components/SelectMenu.vue'

const { $api } = useNuxtApp()
const toast = useToast()
const { t } = useI18n()

const triggered = ref(false)
useSignalR().addCallback('trigger', () => {
  triggered.value = true
  setTimeout(() => triggered.value = false, 250)
})

/**
 * Busy configuring Trigger
 */
const busy = ref(false)

/**
 * PinNumber data
 */
const {
  data: pin,
  status: statusPin,
  refresh: refreshPin
} = await useApi('/Trigger/PinNumber')
const pinNumber = ref(pin.value)

const gpioPins: SelectMenuItem[] = [...Array(27).keys()].map((i) => {
  return {
    label: `GPIO ${i}`,
    type: 'item',
    value: i
  }
})
watch(pinNumber, async (newValue) => {
  if (!newValue)
    return
  busy.value = true
  try {
    await $api('/Trigger/PinNumber', {
      method: 'PATCH',
      body: newValue
    })
    await refreshPin()
    toast.add({
      icon: 'i-lucide-siren',
      title: t('trigger.toasts.pinSet'),
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
