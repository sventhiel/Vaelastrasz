<script lang="ts">
	import { signIn, signOut } from '@auth/sveltekit/client';
	import { page } from '$app/stores';
    export let provider: any
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation</p>

  
  <form action={provider.signinUrl} method="POST">
    {#if provider.callbackUrl}
      <input type="hidden" name="callbackUrl" value={provider.callbackUrl} />
    {/if}
    <button type="submit" class="button">
      <slot>Sign in with {provider.name}</slot>
    </button>
  </form>