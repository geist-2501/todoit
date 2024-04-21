<script lang="ts">
  import InputField from "$lib/components/InputField.svelte";
  import Button from "$lib/components/Button.svelte";
  
  let email: string = '';
  let password: string = '';
  
  const handleOnClick = () => {};
</script>

<div>
    <h1>Login</h1>
    <InputField placeholder="Email" bind:text={email} />
    <InputField placeholder="Password" bind:text={password} />
    <Button text="Login" onClick={handleOnClick} />
</div>