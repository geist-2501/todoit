import {xhr} from "$lib/service/xhr";
import type {ToDo} from "$lib/model/todo";
import type {Priority} from "../model/todo";

export interface CreateToDoRequest {
    Description: string;
    Priority: Priority;
} 

export const todoSvc = {
    getAll: () => xhr.getJson<ToDo[]>("/api/tasks"),
    create: (createRequest: CreateToDoRequest) => xhr.postJson("/api/tasks", createRequest),
    update: (toDo: ToDo) => xhr.putJson("/api/tasks", toDo),
    markAsDone: (id: string) => xhr.putJson(`/api/tasks/${id}/done`),
};