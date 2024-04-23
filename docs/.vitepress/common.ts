import { defineConfig } from 'vitepress'

export const common = defineConfig({
  base: "/JapaneseCallouts/",
  cleanUrls: true,
  srcDir: './pages',
  head: [['link', { rel: 'icon', type: 'image/png', href: '/JapaneseCallouts/Icon.png' }]],
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    logo: '/Icon.png',

    socialLinks: [
      { icon: 'discord', link: 'https://discord.gg/aBer7YvDPA' },
      { icon: 'twitter', link: 'https://twitter.com/DekoKiyomori' },
      { icon: 'github', link: 'https://github.com/DekoKiyo/JapaneseCallouts' },
    ],

    docFooter: {
      next: false,
      prev: false
    }
  },
})