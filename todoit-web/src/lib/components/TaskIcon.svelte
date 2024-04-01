<script lang="ts">
    import {onEnter} from "$lib/a11y";

    export let onClick: () => void;
</script>

<div class="task-icon" role="button" tabindex="0" on:keydown|stopPropagation={onEnter(onClick)} on:click|stopPropagation={onClick}></div>

<style>
    .task-icon {
        width: 12px;
        height: 12px;
        border: 1px solid var(--col-secondary);
        border-radius: 6px;
    }
</style>