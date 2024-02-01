const backendUrl = "http://localhost:5095"

const getJson = async <T>(url: string): Promise<T> => {
    const response = await fetch(backendUrl + url);
    if (!response.ok) {
        throw new Error(`Request to ${url} failed with status ${response.status} ${response.statusText}`);
    }

    return response.json();
}

export const xhr = {
    getJson: getJson 
}