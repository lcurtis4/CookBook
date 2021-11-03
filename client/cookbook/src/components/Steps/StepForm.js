import { useEffect } from "react";
import { useHistory, useParams } from "react-router";
import { useState } from "react/cjs/react.development";
import { Button, Form, FormGroup, Label } from "reactstrap";
import { addStep, getStepById } from "../Managers/stepManager";

export default function StepForm() {
    const history = useHistory();
    const [step, setStep] = useState({})
    const [isLoading, setIsLoading] = useState(true)
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

    const handleSave = e => {
        e.preventDefault()
        if (params.id) {
            setIsLoading(true)
            .then(() => {
                history.push("/dish/:id")
            })
        } else {
            addStep(step)
                .then(() => {
                    history.go(-1)
                })
        }
    }

    return (
        <Form>
            <FormGroup>
                <Label for="stepText"></Label>
                <input 
                    type="text"
                    id="stepText"
                    placeholder="stepText"
                    value={step.stepText}
                    onChange={handleInputChange}
                />
                <input 
                    type="number"
                    id="stepOrder"
                    placeholder="Which step is this?"
                    value={step.stepOrder}
                    onChange={handleInputChange}
                />
                <Button className="btn btn-primary" onClick={handleSave}>
                    Submit
                </Button>
            </FormGroup> 
        </Form>
    )
}