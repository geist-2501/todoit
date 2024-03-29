<script lang="ts">
    import type {ToDo} from "../model/todo";
    import {onEnter} from "$lib/a11y";
    import TaskIcon from "$lib/components/TaskIcon.svelte";
    import Priority from "$lib/components/Priority.svelte";

    export let toDo: ToDo;
    export let onClickRow: (toDo: ToDo) => void;
    
    const handleOpenToDo = () => onClickRow(toDo);
</script>

<div class="wrapper" role="button" tabindex="0" on:keydown={onEnter(handleOpenToDo)} on:click={handleOpenToDo}>
    <TaskIcon />
    <Priority priority={toDo.priority} />
    <p class="description">{toDo.description}</p>
</div>

<style>
    .wrapper {
        display: flex;
        padding: 10px;
        align-items: center;
        gap: 10px;
        border-radius: 5px;
        border: 1px solid var(--col-secondary);
        margin-bottom: 5px;
    }
    
    .description {
        color: var(--col-foreground);
        font-size: 16px;
        font-style: normal;
        font-weight: 400;
    }
</style>