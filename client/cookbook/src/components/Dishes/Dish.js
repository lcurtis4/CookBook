import React from "react";
import { Card, CardBody, Button } from "reactstrap";
import { Link, useHistory } from "react-router-dom";

export default function Dish({ dish }) {

    const history = useHistory();

    return (
        <Card className="m-4">
            <CardBody>
                <strong><Link to={`dish/${dish.id}`}>{dish.title}</Link></strong> 
                <div className="font-weight-bold">{dish.title}</div>
            </CardBody>
        </Card>
    )
}