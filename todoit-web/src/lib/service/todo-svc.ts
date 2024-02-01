import {xhr} from "$lib/service/xhr";
import type {ToDo} from "$lib/model/todo";

export const todoSvc = {
    getAll: () => xhr.getJson<ToDo[]>("/api/tasks")
};