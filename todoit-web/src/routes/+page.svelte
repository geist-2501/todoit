<script lang="ts">
    import ToDoRow from "../lib/components/ToDoRow.svelte";
    import {toDoStore} from "$lib/store";
    import type {ToDo} from "$lib/model/todo";
    import {onMount} from "svelte";
    import {todoSvc} from "$lib/service/todo-svc";
    import Modal from "$lib/components/Modal.svelte";
    import ToDoCreator from "$lib/components/ToDoCreator.svelte";

    onMount(async () => {
        const todos = await todoSvc.getAll();
        toDoStore.set(todos);
    });
    
    let viewToDoModalOpen = false;
    let selectedToDoId: string | null = null;
    let selectedToDo: ToDo | null = null;
    $:selectedToDo = $toDoStore.find(x => x.id === selectedToDoId) ?? null;
    const handleOnClickToDo = () => {
        viewToDoModalOpen = true;
    };
    
    const handleCreateToDo = async (description: string) => {
        await todoSvc.create({Description: description, Priority: "medium"});
        const todos = await todoSvc.getAll();
        toDoStore.set(todos);
    };
    
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
    <ToDoCreator onClickAdd="{handleCreateToDo}" />
    <h1>To Do</h1>
    {#each incompleteToDos as toDo}
        <ToDoRow toDo="{toDo}" onClickRow="{handleOnClickToDo}" />
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
<Modal bind:showModal={viewToDoModalOpen}>
    {#if selectedToDo == null}
        <p>No item selected</p>
        <p>This is probably an internal error</p>
    {:else }
        <p>{selectedToDo.description}</p>
        <hr />
        <p>ToDo input field</p>
    {/if}
</Modal>

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