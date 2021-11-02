import { useHistory } from "react-router";
import { Card, CardBody } from "reactstrap";

export default function Step({ step, handleDelete }) {
    const history = useHistory();

    return (
        <Card className="m-4">
            <CardBody>
                <p>{step.stepText}</p>
            </CardBody>
        </Card>
    )
}