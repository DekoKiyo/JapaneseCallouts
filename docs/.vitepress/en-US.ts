import { defineConfig, type DefaultTheme } from 'vitepress'

export const config = defineConfig({
  lang: 'en-US',
  title: "Japanese Callouts Docs",
  titleTemplate: "JPC Docs",
  description: "Japanese style & customizable callouts pack",
  themeConfig: {
    nav: nav(),

    sidebar: {
      '/docs/': { base: '/docs/', items: sidebarDocs() },
      '/localization/': { base: '/localization/', items: sidebarLocalization() },
    },

    socialLinks: [
      { icon: 'discord', link: 'https://discord.gg/aBer7YvDPA' },
      { icon: 'twitter', link: 'https://twitter.com/DekoKiyomori' },
      { icon: 'github', link: 'https://github.com/DekoKiyo/JapaneseCallouts' },
    ],

    footer: {
      message: 'Japanese Callouts Official Docs - Powered by <a href="https://vitepress.dev">VitePress</a>',
      copyright: 'Copyright 2024 DekoKiyo, All rights reserved.'
    }
  },
})

function nav(): DefaultTheme.NavItem[] {
  return [
    {
      text: 'Home',
      link: '/',
    },
    {
      text: 'Docs',
      link: '/docs/about',
      activeMatch: '/docs/'
    },
    {
      text: 'Localization',
      link: '/localization/about-localization',
      activeMatch: '/localization/'
    },
  ]
}

function sidebarDocs(): DefaultTheme.NavItem[] {
  return [
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
  ]
}

function sidebarLocalization(): DefaultTheme.NavItem[] {
  return [
    {
      text: 'Information',
      items: [
        { text: 'About Localization', link: '/about-localization' },
        { text: 'Docs Localization', link: '/docs-localization' },
      ]
    },
  ]
}