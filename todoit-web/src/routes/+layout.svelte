﻿<div class="root">
    <slot></slot>
</div>

<style>
    .root {
        --col-background: #EAEAEA;
        --col-foreground: #212121;
        --col-primary: #377BFF;
        --col-secondary: #BABABA;
        --col-priority-low: #80D688;
        --col-priority-medium: #E0B156;
        --col-priority-high: #D5555C;
        --border-radius: 5px;
        --border-stroke: 2px;
        
        background: var(--col-background);
        font-family: 'Work Sans', sans-serif;
        min-width: 100vw;
        min-height: 100vh;
    }
</style>