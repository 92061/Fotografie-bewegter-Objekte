<template>
  <UPageCard title="Flash" icon="i-lucide-zap">
      <USelect v-model="pinNumber" :items="gpioPins" :disabled="busy" :loading="status !== 'success'" />
  </UPageCard>
</template>

<script setup lang="ts">
import type { SelectMenuItem } from "@nuxt/ui/components/SelectMenu.vue";
const { $api } = useNuxtApp();

const { data, status, refresh } = await useApi("/Flash/Flash/PinNumber");
watch(data, () => {
    if (data.value)
        pinNumber.value = data.value
});

let busy = ref(false);
let pinNumber = ref(0);

const gpioPins : SelectMenuItem[] = [...Array(27).keys()].map(i => {
    return {
        label: `GPIO ${i}`,
        type: "item",
        value: i
    }
});

watch(pinNumber, async (newValue) => {
    busy.value = true;
    try{
        await $api("/Flash/Flash/PinNumber", {
            method: "PATCH",
            body: newValue
        });
        await refresh();
    }catch {
        
    }finally {
        busy.value = false;
    }
});
</script>
