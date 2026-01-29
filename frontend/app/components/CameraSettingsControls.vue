<template>
  <UCard>
    <template #header>
      <UIcon
        name="i-lucide-wrench"
        class="text-primary size-5"
      />
      {{ $t('camera.settings.title') }}
    </template>

    <UForm
      :state="state"
      :disabled="busy"
    >
      <UPageColumns class="column-1 md:columns-2 lg:columns-2 *:mb-2">
        <UFormField :label="$t('camera.settings.resolution')">
          <div class="flex flex-row gap-2">
            <UInputNumber
              v-model="state.width"
              :placeholder="$t('camera.settings.width')"
            />
            <UInputNumber
              v-model="state.height"
              :placeholder="$t('camera.settings.height')"
            />
          </div>
        </UFormField>
        <UFormField :label="$t('camera.settings.orientation')">
          <UCheckbox
            :label="$t('camera.settings.hflip')"
            @update:model-value="value => state.hflip = value as boolean"
          />
          <UCheckbox
            :label="$t('camera.settings.vflip')"
            @update:model-value="value => state.vflip = value as boolean"
          />
          <UCheckbox
            :label="$t('camera.settings.rotate180')"
            @update:model-value="value => state.rotate180 = value as boolean"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.encoding')">
          <USelect
            v-model="state.encoding"
            :items="['Jpeg', 'Png', 'Rgb', 'Bmp', 'Yuv420']"
            placeholder="Jpeg"
            class="w-full"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.quality')">
          <UInputNumber
            v-model="state.quality"
            :placeholder="'JPEG ' + $t('camera.settings.quality')"
            class="w-full"
            :min="0"
            :max="100"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.shutter')">
          <UInputNumber
            v-model="state.shutterSpeed"
            :placeholder="$t('camera.settings.microseconds')"
            :format-options="{ style: 'unit', unit: 'microsecond' }"
            class="w-full"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.gain')">
          <UInputNumber
            v-model="state.gain"
            class="w-full"
            placeholder="0.0"
            :steps="0.1"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.metering')">
          <USelect
            v-model="state.metering"
            :items="['Center', 'Spot', 'Average']"
            placeholder="Center"
            class="w-full"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.exposure')">
          <USelect
            v-model="state.exposure"
            :items="['Sport', 'Normal', 'Long']"
            placeholder="Normal"
            class="w-full"
          />
        </UFormField>
        <UFormField label="EV">
          <UInputNumber
            v-model="state.ev"
            placeholder="0.0"
            class="w-full"
            :min="-10.0"
            :max="10.0"
            :format-options="{
              style: 'decimal',
              signDisplay: 'exceptZero',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.awb')">
          <USelect
            v-model="state.awb"
            :items="['Auto', 'Incandescent', 'Tungsten', 'Fluorescent', 'Indoor', 'Daylight', 'Cloudy']"
            placeholder="Auto"
            class="w-full"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.brightness')">
          <UInputNumber
            v-model="state.brightness"
            placeholder="0.0"
            class="w-full"
            :min="-1.0"
            :max="1.0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              signDisplay: 'exceptZero',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.contrast')">
          <UInputNumber
            v-model="state.contrast"
            placeholder="1.0"
            class="w-full"
            :min="0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.saturation')">
          <UInputNumber
            v-model="state.saturation"
            placeholder="1.0"
            class="w-full"
            :min="0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.sharpness')">
          <UInputNumber
            v-model="state.saturation"
            placeholder="1.0"
            class="w-full"
            :min="0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.denoise')">
          <USelect
            v-model="state.denoise"
            :items="['Auto', 'Off', 'CdnOff', 'CdnFast', 'CdnHq']"
            placeholder="Auto"
            class="w-full"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.afMode')">
          <USelect
            v-model="state.autofocusMode"
            placeholder="Default"
            :items="['Default', 'Auto', 'Manual', 'Continous']"
            class="w-full"
          />
        </UFormField>
        <UFormField :label="$t('camera.settings.afRange')">
          <USelect
            v-model="state.autofocusRange"
            placeholder="Normal"
            :items="['Normal', 'Macro', 'Full']"
            class="w-full"
          />
        </UFormField>
      </UPageColumns>
      <UFormField :label="$t('camera.settings.mode')">
        <template #description>
          <div class="flex flex-row gap-2">
            <ULink to="https://www.raspberrypi.com/documentation/accessories/camera.html#hardware-specification">{{ $t('camera.settings.hardware') }}</ULink>
            <ULink to="https://www.raspberrypi.com/documentation/computers/camera_software.html#mode">{{ $t('camera.settings.software') }}</ULink>
          </div>
        </template>
        <UInput
          v-model="state.mode"
          :placeholder="$t('camera.settings.modeWarning')"
          class="w-full"
        />
      </UFormField>
    </UForm>

    <template #footer>
      <div class="flex w-full justify-end">
        <UButton
          :loading="busy"
          @click="updateSettings"
        >
          Update
        </UButton>
      </div>
    </template>
  </UCard>
</template>

<script setup lang="ts">
import type { components } from '#open-fetch-schemas/api'

type CameraSettings = components['schemas']['CameraSettings']

const { $api } = useNuxtApp()
const toast = useToast()
const { t } = useI18n()

const state = reactive<CameraSettings>({})

const busy = ref(false)
const updateSettings = async () => {
  busy.value = true
  try {
    await $api('/Camera/Settings', {
      method: 'POST',
      body: state
    })
    toast.add({
      icon: 'i-lucide-wrench',
      title: t('camera.settings.toasts.settingsUpdated'),
      color: 'success'
    })
  } catch (e) {
    toast.add({
      icon: 'i-lucide-wrench',
      title: 'Error',
      description: (e as Error).message,
      color: 'error'
    })
  } finally {
    busy.value = false
  }
}
</script>
