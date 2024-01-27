<div class="root">
    <slot></slot>
</div>

<style>
    .root {
        --col-background: #EAEAEA;
        --col-foreground: #212121;
        --col-primary: #377BFF;
        --col-secondary: #BABABA;
        
        background: var(--col-background);
        font-family: 'Work Sans', sans-serif;
        min-width: 100vw;
        min-height: 100vh;
    }
</style>