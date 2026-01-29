// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  modules: ['@nuxt/eslint', '@nuxt/ui', 'nuxt-open-fetch', '@nuxt/image'],

  devtools: {
    enabled: true
  },

  css: ['~/assets/css/main.css'],

  routeRules: {
    '/': { prerender: true }
  },

  devServer: {
    host: '127.0.0.1'
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

  openFetch: {
    clients: {
      api: {
        baseURL: 'http://192.168.0.133:5000',
        schema: 'http://192.168.0.133:5000/swagger/v1/swagger.json'
      }
    }
  }
})
