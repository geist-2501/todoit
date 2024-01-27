<script lang="ts">
    import ToDoRow from "../lib/components/ToDoRow.svelte";
    import type {ToDo} from "../lib/model/todo";
    
    const dummyToDo: ToDo = {
        id: '123',
        description: 'This is a To Do task',
        priority: "low",
        done: false
    }
</script>

<section class="header">
    <img src="logo.svg" alt="logo">
    <div>
        <h3>Blair Cross</h3>
    </div>
</section>
<section class="container">
    <h1>To Do</h1>
    <ToDoRow toDo="{dummyToDo}" />
    <h1>Done</h1>
    <ToDoRow toDo="{dummyToDo}" />
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