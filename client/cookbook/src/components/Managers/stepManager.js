import { getToken } from "../Managers/authManager"

const apiUrl = "api/step"

export const getAllSteps = () => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error("ERROR IN GETTTING STEPS")
            }
        })
    })
}

export const getStepById = (id) => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error("ERROR GETTTING STEP BY ID")
            }
        })
    })
}