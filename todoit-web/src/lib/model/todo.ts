export const priorities = ['Low', 'Medium', 'High'] as const;
export type Priority = typeof priorities[number];
export const prioritiesSortOrder: Record<Priority, number> = {
    Low: 0,
    Medium: 1,
    High: 2
} as const;

export interface ToDo {
    id: string;
    description: string;
    priority: Priority;
    done: boolean;
}