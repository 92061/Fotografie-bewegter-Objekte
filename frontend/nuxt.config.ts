// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  modules: ['@nuxt/eslint', '@nuxt/ui', 'nuxt-open-fetch', '@nuxt/image', '@nuxtjs/i18n'],

  devtools: {
    enabled: true
  },

  devServer: {
    host: '127.0.0.1'
  },

  css: ['~/assets/css/main.css'],

  ssr: false,
  routeRules: {
    '/': { prerender: true }
  },

  compatibilityDate: '2025-01-15',

  eslint: {
    config: {
      stylistic: {
        commaDangle: 'never',
        braceStyle: '1tbs'
      }
    }
  },

  i18n: {
    defaultLocale: 'en',
    locales: [
      { code: 'en', name: 'English', file: 'en.json' },
      { code: 'de', name: 'Deutsch', file: 'de.json' }
    ]
  },

  openFetch: {
    clients: {
      api: {
        baseURL: '/',
        schema: '../API/API.json'
      }
    }
  }
})
