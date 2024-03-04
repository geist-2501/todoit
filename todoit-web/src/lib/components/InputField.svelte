<script lang="ts">
    export let text = '';
</script>

<input type="text" class="input" bind:value={text} />

<style>
    .input {
        border: var(--border-stroke) solid var(--col-secondary);
        border-radius: var(--border-radius);
        background: none;
        padding: 6px 10px;
        font-size: 14px;
        transition: border-color 100ms;
    }
    
    .input:hover {
        border-color: color-mix(in srgb, var(--col-secondary), #000 15%);
    }
    
    .input:focus {
        outline: none;
        border-color: var(--col-primary);
    }
</style>