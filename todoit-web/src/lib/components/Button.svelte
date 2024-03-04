<script lang="ts">
    export let text: string;
    export let onClick: () => void;
</script>

<button class="button" on:click="{onClick}">{text}</button>

<style>
    .button {
        color: var(--col-background);
        background-color: var(--col-primary);
        border-radius: var(--border-radius);
        border: none;
        padding: calc(var(--border-stroke) + 6px) calc(var(--border-stroke) + 10px);
        font-size: 14px;
        transition: background-color 100ms;
    }
    
    .button:hover {
        background-color: color-mix(in srgb, var(--col-primary), #000 15%);    
    }
</style>