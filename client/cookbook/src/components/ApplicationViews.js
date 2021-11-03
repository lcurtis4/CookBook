import React from "react"
import { Switch, Route, Redirect } from "react-router-dom"
import DishDetail from "./Dishes/DishDetail"
import DishList from "./Dishes/DishList"
import Login from "./Login"
import Register from "./Register"
import DishForm from "./Dishes/DishForm"
import StepList from "./Steps/StepList"
import StepForm from "./Steps/StepForm"

export default function ApplicationViews({ isLoggedIn }) {
    return (
        <main>
            <Switch>
                <Route path="/login">
                    <Login />
                </Route>

                <Route path="/register">
                    <Register />
                </Route>

                <Route path="/addDish" exact>
                    <DishForm />
                </Route>
                
                <Route path="/dish" exact>
                    <DishList /> 
                </Route>

                <Route path="/dish/:id" exact>
                    <DishDetail />
                    <StepList />
                </Route>

                <Route path="/dish/edit/:id" exact>
                    <DishForm />
                </Route>

                <Route path="/addStep/:dishId" exact>
                    <StepForm />
                </Route>
            </Switch>
        </main>
    )
}
