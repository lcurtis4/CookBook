import { useEffect } from "react";
import { useHistory } from "react-router";
import { useState } from "react/cjs/react.development";
import { deleteDish, getAllDishes } from "../Managers/dishManager";
import Dish from "./Dish"

export default function DishList() {
    const [dishes, setDishes] = useState([])

    const history = useHistory()

    const getDishFromState = () => {
        return getAllDishes().then((dishes) => {
            setDishes(dishes)
        })
    }

    const handleDelete = (dishId) => {
        if (
            window.confirm(
                `Are you sure you want to delete? Press OK to confirm.`
            )
        ) {
            deleteDish(dishId).then(getDishFromState())
        } else {
            history.push("/")
        }
    }

    useEffect(() => {
        getDishFromState()
    }, [])

    return (
        <section>
            {dishes.map((d) => 
                <Dish key={d.id} dish={d} handleDelete={handleDelete} />
            )}
        </section>
    )
}