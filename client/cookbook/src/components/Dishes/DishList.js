import { useEffect } from "react";
import { useHistory } from "react-router";
import { useState } from "react/cjs/react.development";
import { getAllDishes } from "../../Managers/dishManager";
import Dish from "./Dish"

export default function DishList() {
    const [dishes, setDishes] = useState([])

    const history = useHistory()

    const getDishFromState = () => {
        return getAllDishes().then((dishes) => {
            setDishes(dishes)
        })
    }

    useEffect(() => {
        getDishFromState()
    }, [])

    return (
        <section>
            {dishes.map((d) => 
                <Dish key={d.id} dish={d}/>
                //add delete method
            )}
        </section>
    )
}