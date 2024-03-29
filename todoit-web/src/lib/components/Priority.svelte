<script lang="ts">
    import type {Priority} from "../model/todo";

    export let priority: Priority = 'Low';
    
    const styles: Record<Priority, { text: string, className: string }> = {
        Low: {
            text: 'L',
            className: 'low'
        },
        Medium: {
            text: 'M',
            className: 'medium'
        },
        High: {
            text: 'H',
            className: 'high'
        },
    };
    
    const currentStyle = styles[priority];
</script>

<div class="wrapper {currentStyle.className}">
    <p>{currentStyle.text}</p>
</div>

<style>
    .wrapper {
        border-radius: var(--border-radius);
        display: flex;
        justify-content: center;
        align-items: center;
        width: 24px;
        height: 24px;
    }
    
    .low {
        background: var(--col-priority-low);
    }


    .medium {
        background: var(--col-priority-medium);
    }


    .high {
        background: var(--col-priority-high);
    }
</style>

