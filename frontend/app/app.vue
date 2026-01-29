<template>
  <UApp>
    <UHeader>
      <ULink
        :to="`${useRuntimeConfig().public.openFetch.api.baseURL}/swagger`"
        target="_blank"
      >Swagger</ULink>
    </UHeader>

    <UMain>
      <UPageBody>
        <NuxtLayout>
          <NuxtPage />
        </NuxtLayout>
      </UPageBody>
    </UMain>

    <USeparator icon="i-simple-icons-nuxtdotjs" />

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
