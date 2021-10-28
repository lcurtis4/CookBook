import { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { getDishById, addDish } from "../../managers/dishManager";

export default function DishForm() {
    const history = useHistory();
    const [dish, setDish] = useState({})
    const [isLoading, setIsLoading] = useState(true)
    const params = useParams()

    useEffect(() => {
        if (params.id) {
            getDishById(params.id).then(p => {
                setDish(p)
                setIsLoading(false)
            })
        }
    }, [])

    const handleInputChange = e => {
        const dishCopy = { ...dish }
        dishCopy[e.target.id] = e.target.value
        setDish(dishCopy)
    }

    const handleSave = e => {
        e.preventDefault()
        if (params.id) {
            setIsLoading(true)
            .then(()=>{
                history.push("/dish")
            })
        } else {
            addDish(dish)
                .then(()=> {
                    history.push("/dish")
                })
        }
    }

    return (
        <Form>
            <FormGroup>
                <Label for="Title">Title</Label>
                <input 
                    type="text"
                    name="title"
                    id="title"
                    placeholder="title"
                    value={dish.title}
                    onChange={handleInputChange}
                />
                <Label for="imageLocation">Header Image</Label>
                <Input type="text" name="imageLocation" id="imageLocation" placeholder="Image URL" value={dish.imageLocation} onChange={handleInputChange} />
                <Label for="publishDateTime">Publication Date</Label>
                <Input type="date" name="publishDateTime" id="publishDateTime" placeholder="Publication Date" valid={dish.publishDateTime} onChange={handleInputChange} />
            </FormGroup>
            <Button className="btn btn-primary" onClick={handleSave}>
                Submit
            </Button>
        </Form>
    )
}