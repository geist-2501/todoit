const backendUrl = "http://localhost:5095"

export const xhr = {
    getJson: async <T> (url: string) => {
        return await fetch(backendUrl + url) as T;
    } 
}