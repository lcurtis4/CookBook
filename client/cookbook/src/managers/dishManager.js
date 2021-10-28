import { getToken } from "../modules/authManager";

const apiUrl = "/api/dish"

export const getAllDish = () => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Beareer ${token}`
            }
        }).then(res => {
            if (res.ok) {
                return res.json();
            } else {
                throw new Error("ERROR IN GETTING POSTS")
            }
        })
    })
};

export const getDishById = (id) => {
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
                throw new Error("ERROR GETTING POST BY ID")
            }
        })
    })
}

export const getDishByUserId = () => {
    return getToken().then((token) => {
        return fetch(`${apiUrl}/myDish`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(res => res.json())
    })
}

export const addDish = (dish) => {
    return getToken().then((token) => {
        return fetch(apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(dish),
        })
    })
}