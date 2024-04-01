<script lang="ts">
    import type {IconName} from "$lib/icon-definition";
    import Icon from "$lib/components/Icon.svelte";

    export let iconName: IconName;
    export let onClick: () => void;
</script>

<button class="button" on:click={onClick}>
    <Icon iconName={iconName} />
</button>

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