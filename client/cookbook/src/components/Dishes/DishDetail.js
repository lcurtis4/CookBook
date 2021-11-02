import { useEffect } from "react";
import { useHistory, useParams } from "react-router";
import { Button, ListGroup, ListGroupItem } from "reactstrap";
import { useState } from "react/cjs/react.development";
import { getDishById } from "../Managers/dishManager";

export default function DishDetail () {
    const [dish, setDish] = useState({});
    const {id} = useParams();
    const histroy = useHistory();

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
                            <Button className="btn btn-dark fload-right" 
                            //onClick={() => {
                              // history.pushState(`/steps/${step.id}`)
                            //</ListGroup>}}
                            >Add Step</Button>
                        </ListGroup>
                    </ListGroup>
                </div>
            </div>
        </div>
    )
}