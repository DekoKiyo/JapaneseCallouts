import { defineConfig, type DefaultTheme } from 'vitepress'

export const config = defineConfig({
  lang: 'ja-JP',
  title: "Japanese Callouts Docs",
  titleTemplate: "JPC Docs",
  description: "日本風でカスタマイズ可能なコールアウトパック",
  themeConfig: {
    nav: nav(),

    sidebar: {
      '/ja-JP/docs/': { base: '/ja-JP/docs/', items: sidebarDocs() },
      '/ja-JP/localization/': { base: '/ja-JP/localization/', items: sidebarLocalization() },
    },

    socialLinks: [
      { icon: 'discord', link: 'https://discord.gg/aBer7YvDPA' },
      { icon: 'twitter', link: 'https://twitter.com/DekoKiyomori' },
      { icon: 'github', link: 'https://github.com/DekoKiyo/JapaneseCallouts' },
    ],

    footer: {
      message: 'Japanese Callouts Official Docs - 提供: <a href="https://vitepress.dev">VitePress</a>',
      copyright: 'Copyright 2024 DekoKiyo, All rights reserved.'
    }
  },
})

function nav(): DefaultTheme.NavItem[] {
  return [
    {
      text: 'ホーム',
      link: '/ja-JP/',
    },
    {
      text: 'ドキュメント',
      link: '/ja-JP/docs/about',
      activeMatch: '/ja-JP/docs/'
    },
    {
      text: '多言語化',
      link: '/ja-JP/localization/about-localization',
      activeMatch: '/ja-JP/localization/'
    },
  ]
}

function sidebarDocs(): DefaultTheme.NavItem[] {
  return [
    {
      text: 'インフォメーション',
      items: [
        { text: 'Japanese Callouts', link: '/about' },
        { text: '導入推奨Mods', link: '/recommendation' },
        { text: '導入方法', link: '/installation' },
        { text: '貢献について', link: '/contributing' },
        { text: '重み付き確率計算について', link: '/weighted-probability-calculation' },
      ]
    },
    {
      text: '設定ファイル (XML)',
      items: [
        { text: '設定ファイルについて', link: '/config' },
        { text: '車両 (Vehicle)', link: '/config/vehicle' },
        { text: '人 (Ped)', link: '/config/ped' },
        { text: '武器 (Weapon)', link: '/config/weapon' },
        { text: '位置 (Position)', link: '/config/position' },
      ]
    },
    {
      text: 'コールアウト',
      items: [
        { text: 'コールアウトについて', link: '/callouts' },
        { text: '銀行強盗', link: '/callouts/bank-heist' },
        { text: '酔っぱらい集団', link: '/callouts/drunk-guys' },
        { text: '煽り運転', link: '/callouts/road-rage' },
        { text: '盗難車両', link: '/callouts/stolen-vehicle' },
        { text: 'コンビニ強盗', link: '/callouts/store-robbery' },
      ]
    },
  ]
}

function sidebarLocalization(): DefaultTheme.NavItem[] {
  return [
    {
      text: 'Information',
      items: [
        { text: '多言語化について', link: '/about-localization' },
        { text: 'ドキュメントの多言語化', link: '/docs-localization' },
      ]
    },
  ]
}