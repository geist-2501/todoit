import {writable} from "svelte/store";
import type {ToDo} from "./model/todo";

export const toDoStore = writable<ToDo[]>([]);
export const currentUser = writable()