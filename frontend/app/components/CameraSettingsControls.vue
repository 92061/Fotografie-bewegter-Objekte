<template>
  <UCard>
    <template #header>
      <UIcon
        name="i-lucide-wrench"
        class="text-primary size-5"
      />
      Camera Settings
    </template>

    <UForm
      :state="state"
      :disabled="busy"
    >
      <UPageColumns class="column-1 md:columns-2 lg:columns-2">
        <UFormField label="Camera Mode">
            <template #description>
                <div class="flex flex-row gap-2">
                    <ULink to="https://www.raspberrypi.com/documentation/accessories/camera.html#hardware-specification">Hardware</ULink>
                    <ULink to="https://www.raspberrypi.com/documentation/computers/camera_software.html#mode">Software</ULink>
                </div>
            </template>
          <UInput
            v-model="state.mode"
            placeholder="Set this if you know what you are doing"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Output resolution">
          <div class="flex flex-row gap-2">
            <UInputNumber
              v-model="state.width"
              placeholder="Width"
            />
            <UInputNumber
              v-model="state.height"
              placeholder="Height"
            />
          </div>
        </UFormField>
        <UFormField label="Output orientation">
          <UCheckbox
            label="Flip horizontally"
            @update:model-value="value => state.hflip = value as boolean"
          />
          <UCheckbox
            label="Flip vertically"
            @update:model-value="value => state.vflip = value as boolean"
          />
          <UCheckbox
            label="Rotate 180 degrees"
            @update:model-value="value => state.rotate180 = value as boolean"
          />
        </UFormField>
        <UFormField label="Output Encoding">
          <USelect
            v-model="state.encoding"
            :items="['Jpeg', 'Png', 'Rgb', 'Bmp', 'Yuv420']"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Shutter Speed">
          <UInputNumber
            v-model="state.shutterSpeed"
            placeholder="Microseconds"
            :format-options="{ style: 'unit', unit: 'microsecond' }"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Gain">
          <UInputNumber
            v-model="state.gain"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Metering Mode">
          <USelect
            v-model="state.metering"
            :items="['Center', 'Spot', 'Average']"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Exposure">
          <USelect
            v-model="state.exposure"
            :items="['Sport', 'Normal', 'Long']"
            class="w-full"
          />
        </UFormField>
        <UFormField label="EV">
          <UInputNumber
            v-model="state.ev"
            placeholder="0"
            class="w-full"
            :min="-10.0"
            :max="10.0"
            :format-options="{
              style: 'decimal',
              signDisplay: 'always',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField label="Auto White Balance">
          <USelect
            v-model="state.awb"
            :items="['Auto', 'Incandescent', 'Tungsten', 'Fluorescent', 'Indoor', 'Daylight', 'Cloudy']"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Brightness">
          <UInputNumber
            v-model="state.brightness"
            placeholder="0"
            class="w-full"
            :min="-1.0"
            :max="1.0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              signDisplay: 'always',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField label="Contrast">
          <UInputNumber
            v-model="state.contrast"
            placeholder="1"
            class="w-full"
            :min="0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField label="Saturation">
          <UInputNumber
            v-model="state.saturation"
            placeholder="1"
            class="w-full"
            :min="0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField label="Sharpness">
          <UInputNumber
            v-model="state.saturation"
            placeholder="1"
            class="w-full"
            :min="0"
            :step="0.1"
            :format-options="{
              style: 'decimal',
              minimumFractionDigits: 1
            }"
          />
        </UFormField>
        <UFormField label="Denoise">
          <USelect
            v-model="state.denoise"
            :items="['Auto', 'Off', 'CdnOff', 'CdnFast', 'CdnHq']"
            class="w-full"
          />
        </UFormField>
        <UFormField label="Autofocus">
          <div class="flex flex-row gap-2">
            <USelect
              v-model="state.autofocusMode"
              placeholder="Mode"
              :items="['Default', 'Auto', 'Manual', 'Continous']"
              class="w-full"
            />
            <USelect
              v-model="state.autofocusRange"
              placeholder="Range"
              :items="['Normal', 'Macro', 'Full']"
              class="w-full"
            />
          </div>
        </UFormField>
        <UFormField label="Quality">
          <UInputNumber
            v-model="state.quality"
            placeholder="JPG Quality"
            class="w-full"
            :min="0"
            :max="100"
          />
        </UFormField>
      </UPageColumns>
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
      title: 'Settings updated!',
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
