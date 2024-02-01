<script lang="ts">
    import ToDoRow from "../lib/components/ToDoRow.svelte";
    import {toDoStore} from "$lib/store";
    import type {ToDo} from "$lib/model/todo";
    import {onMount} from "svelte";
    import {todoSvc} from "$lib/service/todo-svc";
    
    onMount(async () => {
        const todos = await todoSvc.getAll();
        toDoStore.set(todos);
    })
    
    let incompleteToDos: ToDo[];
    let completeToDos: ToDo[];
    $:incompleteToDos = $toDoStore.filter(x => !x.done);
    $:completeToDos = $toDoStore.filter(x => x.done);
</script>

<section class="header">
    <img src="logo.svg" alt="logo">
    <div>
        <h3>Blair Cross</h3>
    </div>
</section>
<section class="container">
    <h1>To Do</h1>
    {#each incompleteToDos as toDo}
        <ToDoRow toDo="{toDo}" />
    {/each}
    <h1>Done</h1>
    {#each completeToDos as toDo}
        <ToDoRow toDo="{toDo}" />
    {/each}
</section>

<style>
    .header {
        width: 100%;
        display: flex;
        justify-content: space-between;
        align-items: flex-end;
        padding: 100px;
    }
    
    .container {
        margin: auto;
        width: 50vw;
    }
</style>