import { useHistory, useParams } from "react-router"
import { useState } from "react"
import { useEffect } from "react/cjs/react.development"
import { getAllSteps } from "../Managers/stepManager"
import Step from "./Step"
import { getAllDishes, getDishById } from "../Managers/dishManager"

export default function StepList() {
    const [dish, setDish] = useState([]);
    const {id} = useParams();

    const history = useHistory()
    
    const getDishFromState = () => {
        return getDishById(id).then((dish) => {
            setDish(dish)
        })
    }

    useEffect(() => {
        getDishFromState()
    }, [])

    return (
        <section>
            {dish.steps?.map((s) => (
                <Step key={s.id} step={s} />
            ))}
        </section>
    )
}