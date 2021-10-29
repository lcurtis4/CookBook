import { getToken } from "../modules/authManager";

const apiUrl = "/api/dish"

export const getAllDishes = () => {
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
                throw new Error("ERROR IN GETTING DISHES")
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
                throw new Error("ERROR GETTING DISH BY ID")
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