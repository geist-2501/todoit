<script lang="ts">
    import type {ToDo} from "../model/todo";

    export let toDo: ToDo;
</script>

<div class="wrapper">
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