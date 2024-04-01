<script lang="ts">
    import ToDoRow from "../lib/components/ToDoRow.svelte";
    import {toDoStore} from "$lib/store";
    import type {ToDo} from "$lib/model/todo";
    import {onMount} from "svelte";
    import {todoSvc} from "$lib/service/todo-svc";
    import ToDoCreator from "$lib/components/ToDoCreator.svelte";
    import ToDoModal from "$lib/components/ToDoModal.svelte";
    import {prioritiesSortOrder} from "../lib/model/todo";

    onMount(async () => {
        const todos = await todoSvc.getAll();
        toDoStore.set(todos);
    });
    
    let viewToDoModalOpen = false;
    let selectedToDo: ToDo | null = null;
    
    const handleOnClickToDo = (toDo: ToDo) => {
        viewToDoModalOpen = true;
        selectedToDo = toDo;
    };
    
    const handleOnClickDone = async (toDo: ToDo) => {
        await todoSvc.markAsDone(toDo.id);
        const todos = await todoSvc.getAll();
        toDoStore.set(todos);
    }
    
    const handleCreateToDo = async (description: string) => {
        await todoSvc.create({Description: description, Priority: "Medium"});
        const todos = await todoSvc.getAll();
        toDoStore.set(todos);
    };
    
    let incompleteToDos: ToDo[];
    let completeToDos: ToDo[];
    $:incompleteToDos = $toDoStore.filter(x => !x.done).toSorted(x => -prioritiesSortOrder[x.priority]);
    $:completeToDos = $toDoStore.filter(x => x.done);
</script>

<section class="header">
    <img src="logo.svg" alt="logo">
    <div>
        <h3>Blair Cross</h3>
    </div>
</section>
<section class="container">
    <ToDoCreator onClickAdd="{handleCreateToDo}" />
    <h1>To Do</h1>
    {#each incompleteToDos as toDo}
        <ToDoRow toDo="{toDo}" onClickRow="{handleOnClickToDo}" onClickDone={handleOnClickDone} />
    {/each}
    {#if incompleteToDos.length === 0}
        <p>Empty</p>
    {/if}
    <h1>Done</h1>
    {#each completeToDos as toDo}
        <ToDoRow toDo="{toDo}" onClickRow="{handleOnClickToDo}" />
    {/each}
    {#if completeToDos.length === 0}
        <p>Empty</p>
    {/if}
</section>

<ToDoModal bind:modalOpen={viewToDoModalOpen} selectedToDo={selectedToDo} />

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