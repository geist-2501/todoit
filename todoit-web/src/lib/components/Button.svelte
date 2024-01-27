<script lang="ts">
    export let text: string;
    export let onClick: () => void;
</script>

<button onclick="onClick">{text}</button>