import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "Japanese Callouts Docs",
  description: "Japanese style & customizable callouts pack",
  base: "/JapaneseCallouts/",
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: 'Home', link: '/' },
      { text: 'Examples', link: '/markdown-examples' }
    ],

    sidebar: [
      {
        text: 'Examples',
        items: [
          { text: 'Markdown Examples', link: '/markdown-examples' },
          { text: 'Runtime API Examples', link: '/api-examples' }
        ]
      }
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/DekoKiyo/JapaneseCallouts' },
      { icon: 'discord', link: 'https://discord.gg/aBer7YvDPA' },
      { icon: 'twitter', link: 'https://twitter.com/DekoKiyomori' },
    ]
  }
})
