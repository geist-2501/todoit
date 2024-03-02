<script lang="ts">
    import type {ToDo} from "../model/todo";
    import {onEnter} from "$lib/a11y";

    export let toDo: ToDo;
    export let onClickRow: (toDoId: string) => void;
    
    const handleOpenToDo = () => onClickRow(toDo.id);
</script>

<div class="wrapper" role="button" tabindex="0" on:keydown={onEnter(handleOpenToDo)} on:click={handleOpenToDo}>
    <div class="task-icon"></div>
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
    }
    
    .task-icon {
        width: 12px;
        height: 12px;
        border: 1px solid var(--col-secondary);
        border-radius: 6px;
    }
    
    .description {
        color: var(--col-foreground);
        font-size: 16px;
        font-style: normal;
        font-weight: 400;
    }
</style>