type IconList = Record<string, { symbol: string, alt: string }>;

export const iconList: IconList = {
    'ellipsis': {
        symbol: '&#e5d3;',
        alt: 'more'
    },
    'edit': {
        symbol: '&#e3c9;',
        alt: 'edit'
    }
}

export type Icon = keyof typeof iconList;