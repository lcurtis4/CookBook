import { useEffect } from "react";
import { useHistory, useParams } from "react-router";
import { useState } from "react/cjs/react.development";
import { Form, FormGroup, Label } from "reactstrap";
import { getStepById } from "../Managers/stepManager";

export default function StepForm() {
    const history = useHistory();
    const [step, setStep] = useState({})
    cosnt [isLoading, setIsLoading] = useState(true)
    const params = useParams()

    useEffect(() => {
        if (params.id) {
            getStepById(params.id).then(p => {
                setStep(p)
                setIsLoading(false)
            })
        }
    }, [])

    const handleInputChange = e => {
        const stepCopy = { ... step }
        stepCopy[e.target.id] = e.target.value
        setStep(stepCopy)
    }

    return (
        <Form>
            <FormGroup>
                <Label for="stepText"></Label>
                <Input 
                    type="text"
                    id="stepText"
                    placeholder="stepText"
                    value={step.stepText}
                    onChange={handleInputChange}
                />
            </FormGroup> 
        </Form>
    )
}