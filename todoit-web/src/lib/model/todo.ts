export const priorities = ['low', 'medium', 'high'] as const;
export type Priority = typeof priorities[number];

export interface ToDo {
    id: string;
    description: string;
    priority: Priority;
    done: boolean;
}