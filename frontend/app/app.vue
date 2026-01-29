<template>
  <UApp>
    <UHeader :title="$t('title')">
      <template #right>
        <UButton
          icon="i-lucide-text-search"
          color="neutral"
          variant="ghost"
          :to="`${useRuntimeConfig().public.openFetch.api.baseURL}/swagger`"
          target="_blank"
          :ui="{
            leadingIcon: 'text-primary'
          }"
          label="Swagger"
        />
          <UColorModeSelect />
          <ULocaleSelect
              :model-value="locale"
              :locales="[en, de]"
              @update:model-value="setLocale($event)"
          />
      </template>
    </UHeader>

    <UMain>
      <UPage>
        <UContainer>
          <UPageBody>
            <NuxtPage />
          </UPageBody>
        </UContainer>
      </UPage>
    </UMain>

    <UFooter>
      <template #left>
        <p class="text-sm text-muted">
            {{ $t('builtWithNuxtUi') }} • © {{ new Date().getFullYear() }}
        </p>
      </template>
    </UFooter>
  </UApp>
</template>

<script setup>
import { en, de } from '@nuxt/ui/locale'

const { locale, setLocale } = useI18n()

useHead({
  meta: [{ name: 'viewport', content: 'width=device-width, initial-scale=1' }],
  link: [{ rel: 'icon', href: '/favicon.ico' }],
  htmlAttrs: {
    lang: 'en'
  }
})

const title = "Photography of moving objects"

useSeoMeta({
  title,
  ogTitle: title,
})
</script>
