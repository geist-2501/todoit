<script lang="ts">
  import InputField from "$lib/components/InputField.svelte";
  import Button from "$lib/components/Button.svelte";
  
  export let onClickAdd: (description: string) => void;
  
  let description = '';
</script>

<div>
    <InputField bind:text="{description}" />
    <Button text="Add" onClick="{() => onClickAdd(description)}" />
</div>