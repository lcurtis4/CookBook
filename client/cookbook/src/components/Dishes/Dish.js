import React from "react";
import { Card, CardBody, Button } from "reactstrap";
import { Link, useHistory } from "react-router-dom";

export default function Dish({ dish, handleDelete }) {

    const history = useHistory();

    return (
        <Card className="m-4">
            <CardBody>
                <strong><Link to={`dish/${dish.id}`}>{dish.title}</Link></strong> 
                <div className="font-weight-bold">{dish.title}</div>
                <Button className="btn btn-danger float-right" onClick={() => handleDelete(dish.id)}>Delete</Button>
            </CardBody>
        </Card>
    )
}