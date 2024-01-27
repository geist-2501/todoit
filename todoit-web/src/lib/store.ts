import {writable} from "svelte/store";
import type {ToDo} from "./model/todo";

export const toDoStore = writable<ToDo[]>([]);

toDoStore.set([
    {
        id: '1',
        description: 'This is a dummy to do',
        done: false,
        priority: 'low'
    },
    {
        id: '2',
        description: 'This is a dummy to do',
        done: false,
        priority: 'medium'
    },
    {
        id: '3',
        description: 'This is a dummy to do',
        done: false,
        priority: 'high'
    },
])