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

    <UPageColumns class="*:mb-0 column-1 md:columns-2 lg:columns-2">
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
      <UFormField :label="$t('trigger.flank')">
        <USelect
          v-model="flank"
          :items="['Rising', 'Falling']"
          :disabled="busy"
          :loading="statusFlank !== 'success'"
          class="w-full"
        />
      </UFormField>
    </UPageColumns>
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
  data: pinData,
  status: statusPin,
  refresh: refreshPin
} = await useApi('/Trigger/PinNumber')
const pinNumber = ref<number | undefined>(pinData.value)

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

const {
  data: flankData,
  status: statusFlank,
  refresh: refreshFlank
} = await useApi('/Trigger/Flank')
const flank = ref(flankData.value)
watch(flank, async (newValue) => {
  if (!newValue)
    return
  busy.value = true
  try {
    await $api('/Trigger/Flank', {
      method: 'PATCH',
      body: `"${newValue}"` // Bruh what is this
    })
    await refreshFlank()
    toast.add({
      icon: 'i-lucide-siren',
      title: t('trigger.toasts.flankSet'),
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
