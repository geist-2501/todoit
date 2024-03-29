<script lang="ts">
    import Modal from "$lib/components/Modal.svelte";
    import type {ToDo} from "../model/todo";
    import TaskIcon from "$lib/components/TaskIcon.svelte";
    import Priority from "$lib/components/Priority.svelte";
    import Button from "$lib/components/Button.svelte";
    import Icon from "$lib/components/Icon.svelte";
    
    export let modalOpen: boolean;
    export let selectedToDo: ToDo | null;
    
    let editActive: boolean = false;
</script>

<Modal bind:showModal={modalOpen}>
    {#if selectedToDo == null}
        <p>No item selected</p>
        <p>This is probably an internal error</p>
    {:else}
        <div class="header">
            <TaskIcon />
            <Priority priority={selectedToDo.priority} />
            <p>{selectedToDo.description}</p>
            <Button><Icon iconName="edit" /></Button>
        </div>
        <hr />
        <p>ToDo input field</p>
    {/if}
</Modal>

<style>
    .header {
        display: flex;
        flex-direction: row;
    }
</style>
