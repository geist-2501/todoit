<script lang="ts">
    import type {IconName} from "../icon-definition";
    import {iconList} from "../icon-definition";

    export let iconName: IconName;
    
    let selected = iconList[iconName];
</script>

<span class="icon" aria-label={selected.alt}>{selected.symbol}</span>

<style>
    .icon {
        font-family: 'Material Symbols Outlined';
        font-weight: normal;
        font-style: normal;
        font-size: 24px;  /* Preferred icon size */
        display: inline-block;
        line-height: 1;
        text-transform: none;
        letter-spacing: normal;
        word-wrap: normal;
        white-space: nowrap;
        direction: ltr;
    }
</style>