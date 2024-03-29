<script lang="ts">
    export let showModal: boolean;
    
    let dialog: HTMLDialogElement;
    
    $: if (dialog && showModal) dialog.showModal();
</script>

<!-- svelte-ignore a11y-click-events-have-key-events a11y-no-noninteractive-element-interactions -->
<dialog
    bind:this={dialog}
    on:close={() => (showModal = false)}
    on:click|self={() => dialog.close()}
>
    <!-- svelte-ignore a11y-no-static-element-interactions -->
    <div on:click|stopPropagation>
        {#if $$slots.header}
            <slot name="header" />
            <hr>
        {/if}
        <slot />
        {#if $$slots.footer}
            <hr>
            <slot name="footer" />
        {/if}
    </div>
</dialog>

<style>
    dialog {
        left: 0;
        right: 0;
        top: 0;
        bottom: 0;
        margin: auto;
        position: absolute;
        border: var(--border-stroke) solid var(--col-secondary);
        border-radius: var(--border-radius);
    }
</style>