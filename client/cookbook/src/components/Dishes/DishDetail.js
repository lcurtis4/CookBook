import { useEffect } from "react";
import { useParams } from "react-router";
import { ListGroup, ListGroupItem } from "reactstrap";
import { useState } from "react/cjs/react.development";
import { getDishById } from "../../Managers/dishManager";

export default function DishDetail () {
    const [dish, setDish] = useState({});
    const {id} = useParams();

    useEffect(() => {
        getDishById(id).then(setDish);
    }, []);

    return (
        <div className='container'>
            <div className='row justify-content-center'>
                <div className='col-sm-12 col-lg-6'>
                    <ListGroup>
                        <h3>{dish.title}</h3>
                        <ListGroup>
                            <ListGroupItem>{dish.title}</ListGroupItem>
                        </ListGroup>
                    </ListGroup>
                </div>
            </div>
        </div>
    )
}