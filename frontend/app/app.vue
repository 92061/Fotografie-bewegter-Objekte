<template>
  <UApp>
    <UHeader title="Photography">
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
          Built with Nuxt UI • © {{ new Date().getFullYear() }}
        </p>
      </template>
    </UFooter>
  </UApp>
</template>

<script setup>
useHead({
  meta: [{ name: 'viewport', content: 'width=device-width, initial-scale=1' }],
  link: [{ rel: 'icon', href: '/favicon.ico' }],
  htmlAttrs: {
    lang: 'en'
  }
})

const title = 'Photography'
const description = 'Frontend of Photography'

useSeoMeta({
  title,
  description,
  ogTitle: title,
  ogDescription: description
})
</script>
