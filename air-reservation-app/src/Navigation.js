import React, {Component} from "react";
import { NavLink } from "react-router-dom";
import { Navbar, Nav } from "react-bootstrap";


export class Navigation extends Component{
    render(){
        return(
            <Navbar bg="dark" expand="lg">
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse id="basic-navbar-nav">
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/">
                        Home
                    </NavLink>
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/airReservation">
                        Air Reservation
                    </NavLink>
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/addAirReservation">
                        Add Air reservation
                    </NavLink>
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/searchAirReservation">
                        Search Air reservation
                    </NavLink>
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/searchAirReservationById">
                        Search Air reservation by id
                    </NavLink>
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/airports">
                        Airports
                    </NavLink>         
                    <NavLink className="d-inline p-2 bg-dark text-white" to="/addAirport">
                        Add Airports
                    </NavLink>              
                </Navbar.Collapse>
            </Navbar>
        )
    }
}