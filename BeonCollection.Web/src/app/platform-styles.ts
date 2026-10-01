export interface PlatformStyle {
  bandBg: string;
  bandText: string;
  widthRem: number;
  heightRem: number;
  label: string;
}

export const PLATFORM_STYLES: Record<string, PlatformStyle> = {
  PS4: {
    bandBg: '#003791',
    bandText: '#ffffff',
    widthRem: 1.7,
    heightRem: 16,
    label: 'PS4',
  },
  PS5: {
    bandBg: '#00439c',
    bandText: '#ffffff',
    widthRem: 1.7,
    heightRem: 16,
    label: 'PS5',
  },
  NS: {
    bandBg: '#e60012',
    bandText: '#ffffff',
    widthRem: 1.6,
    heightRem: 13,
    label: 'SWITCH',
  },
};

export const DEFAULT_PLATFORM_STYLE: PlatformStyle = {
  bandBg: '#3a3d44',
  bandText: '#ffffff',
  widthRem: 1.6,
  heightRem: 15,
  label: '',
};

export function getPlatformStyle(platform: string): PlatformStyle {
  return PLATFORM_STYLES[platform] ?? { ...DEFAULT_PLATFORM_STYLE, label: platform };
}

const BODY_PALETTES = [
  { bg: '#f4f2ec', text: '#141414' },
  { bg: '#f4f2ec', text: '#141414' },
  { bg: '#141414', text: '#f4f2ec' },
  { bg: '#141414', text: '#f4f2ec' },
  { bg: '#8c1f28', text: '#f4f2ec' },
  { bg: '#1b2a4a', text: '#f4f2ec' },
];

export function getBodyPalette(title: string): { bg: string; text: string } {
  let hash = 0;
  for (let i = 0; i < title.length; i++) {
    hash = (hash * 31 + title.charCodeAt(i)) >>> 0;
  }
  return BODY_PALETTES[hash % BODY_PALETTES.length];
}