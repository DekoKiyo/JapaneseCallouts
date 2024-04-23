import { defineConfig } from 'vitepress'
import { common } from './common'
import * as enUS from './en-US'
import * as jaJP from './ja-JP'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  ...common,
  locales: {
    'root': { label: 'English', ...enUS.config },
    'ja-JP': { label: '日本語', ...jaJP.config }
  }
})
