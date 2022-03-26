import React, {Component} from "react";
import {Button, Form, FormGroup} from 'react-bootstrap'
import { Table } from "react-bootstrap";

export class SearchAirReservationById extends Component{
    constructor(props){
        super(props);

        this.state = {
            id: '',
            airreservations:[],
        }
    }
    clearPage(event){
        this.setState({airreservations:[]});
        this.setState({id:''});

    }
    handleInputChanged(event){
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });        
    }

    handleSubmit(event){
        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_GET_AIR_RESERVATIONS_BY_ID+`${this.state.id}`
        console.log(url);
        fetch(url, {
            mode: 'cors',
            method:'GET',
            headers:{
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => response.json())
        .then(response => {
            this.setState({airreservations:response});
        });

    }

    render(){
        const {airreservations} = this.state;
        return(
            <div>
                <h3> Search Air Reservations </h3>
                    <Form>
                        <FormGroup>
                        <Form.Label>Id</Form.Label>
                            <Form.Control type="number" name="id" placeholder="Id" value={this.state.id} onChange={this.handleInputChanged.bind(this)}/>                                                    
                        </FormGroup>
                    </Form>
                    
                    <Button variant="primary" onClick={this.handleSubmit.bind(this)}>
                        Submit
                    </Button>
                    <Button variant="primary" onClick={this.clearPage.bind(this)}>
                        Clear
                    </Button>

                    <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr key="header">
                            <th>OriginAirport</th>
                            <th>Departure Datetime</th>
                            <th>TargetAirport</th>
                            <th>Arrival Datetime</th>
                            <th>Airline</th>
                            <th>FlightNumber</th>
                            <th>NumberOfAdults</th>
                            <th>NumberOfKids</th>
                        </tr>
                    </thead>
                    <tbody>
                        {airreservations.map(ar=>
                            <tr key={ar.Id}>
                                <td>{ar.OriginAirport}</td>
                                <td>{ar.DepartureDatetime}</td>
                                <td>{ar.TargetAirport}</td>
                                <td>{ar.ArrivalDatetime}</td>
                                <td>{ar.Airline}</td>
                                <td>{ar.FlightNumber}</td>
                                <td>{ar.NumberOfAdultPassagers}</td>
                                <td>{ar.NumberOfChieldPassagers}</td>
                            </tr>)}

                    </tbody>
                </Table>
            </div>
        )
    }
}