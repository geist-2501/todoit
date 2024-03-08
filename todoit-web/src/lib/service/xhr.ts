const backendUrl = "http://localhost:5095"

const getJson = async <T>(url: string): Promise<T> => {
    const response = await fetch(backendUrl + url);
    if (!response.ok) {
        throw new Error(`Request to ${url} failed with status ${response.status} ${response.statusText}`);
    }

    return response.json();
}

const postJson = async <TReq, TRet>(url: string, body?: TReq): Promise<TRet> => {
    const response = await fetch(backendUrl + url, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body)
    });
    
    return await response.json(); 
}

export const xhr = {
    getJson: getJson,
    postJson: postJson
}