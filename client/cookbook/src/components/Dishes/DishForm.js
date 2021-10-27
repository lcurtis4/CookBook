import { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router";
import { Label } from "reactstrap";


export default function DishForm() {
    const history = useHistory();
    const [dish, setDish] = useState({})
    const [isLoading, setIsLoading] = useState(true)
    const params = useParams()

    useEffect(() => {
        if (params.id) {
            getDishById(parmas.id).then(p => {
                setDish(p)
                setIsLoading(false)
            })
        }
    }, [])

    const handleInputChang = e => {
        const dishCopy = { ...dish }
        dishCopy[e.target.id] = e.target.value
        setDish(dishCopy)
    }

    const handleSave = e => {
        e.preventDefault()
        if (params.id) {
            setIsLoading(true)
            updateDish(dish)
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
                    onChange={handleInputChang}
                />
                <Label for="imageLocation">Header Image</Label>
                <Input type="text" name="imageLocation" id="imageLocation" placeholder="Image URL" value={post.imageLocation} onChange={handleInputChange} />
                <Label for="publishDateTime">Publication Date</Label>
                <Input type="date" name="publishDateTime" id="publishDateTime" placeholder="Publication Date" valid={post.publishDateTime} onChange={handleInputChange} />
            </FormGroup>
            <Button className="btn btn-primary" onClick={handleSave}>
                Submit
            </Button>
        </Form>
    )
}