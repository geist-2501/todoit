export const onEnter = (func: () => void) => (event: KeyboardEvent) => {
    if (event.key === "Enter") func();
}