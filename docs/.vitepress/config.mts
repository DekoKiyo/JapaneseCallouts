import { defineConfig } from 'vitepress'

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "Japanese Callouts Docs",
  titleTemplate: "JPC Docs",
  description: "Japanese style & customizable callouts pack",
  base: "/JapaneseCallouts/",
  cleanUrls: true,
  srcDir: './pages',
  head: [['link', { rel: 'icon', type: 'image/png', href: '/JapaneseCallouts/Icon.png' }]],
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: 'Home', link: '/' },
    ],

    sidebar: [
      {
        text: 'Information',
        items: [
          { text: 'Japanese Callouts', link: '/about' },
          { text: 'Recommendation Mods', link: '/recommendation' },
          { text: 'Installation', link: '/installation' },
          { text: 'Contributing', link: '/contributing' },
          { text: 'About Weighted Probability Calculation', link: '/weighted-probability-calculation' },
        ]
      },
      {
        text: 'Config',
        items: [
          { text: 'About Config Files', link: '/config' },
          { text: 'Vehicle', link: '/config/vehicle' },
          { text: 'Ped', link: '/config/ped' },
          { text: 'Weapon', link: '/config/weapon' },
          { text: 'Position', link: '/config/position' },
        ]
      },
      {
        text: 'Callouts',
        items: [
          { text: 'About Callouts', link: '/callouts' },
          { text: 'Bank Heist', link: '/callouts/bank-heist' },
          { text: 'Drunk Guys', link: '/callouts/drunk-guys' },
          { text: 'Road Rage', link: '/callouts/road-rage' },
          { text: 'Stolen Vehicle', link: '/callouts/stolen-vehicle' },
          { text: 'Store Robbery', link: '/callouts/store-robbery' },
        ]
      },
    ],

    socialLinks: [
      { icon: 'github', link: 'https://github.com/DekoKiyo/JapaneseCallouts' },
      { icon: 'discord', link: 'https://discord.gg/aBer7YvDPA' },
      { icon: 'twitter', link: 'https://twitter.com/DekoKiyomori' },
    ],

    footer: {
      message: "Japanese Callouts Official Docs - Powered by VitePress",
      copyright: "Copyright 2024 DekoKiyo, All rights reserved."
    }
  }
})
