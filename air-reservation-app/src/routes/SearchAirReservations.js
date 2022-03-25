import React, {Component} from "react";
import {Button, Form} from 'react-bootstrap'
import { Table } from "react-bootstrap";
import Select from 'react-select'

export class SearchAirReservation extends Component{
    constructor(props){
        super(props);

        this.state = {
            originAirport: '',
            targetAirport: '',
            departureDatetime:'',
            arrivalDatetime:'',
            airline:'',
            airreservations:[],
            airports:[]
        }

    }

    fetchAirports(){
        fetch(process.env.REACT_APP_BASE_URI+process.env.REACT_APP_GET_ALL_AIRPORTS, {
            mode: 'cors',
            method: 'GET',
            headers: {'Access-Control-Allow-Origin': '*'}
          })
        .then(response => response.json())
        .then(response => {
            this.setState({airports:response});
        });   
    }

    componentDidMount(){
        this.fetchAirports();
    }

    handleInputChanged(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleOriginChange = (selectedOption) => {
        this.setState({
            originAirport: selectedOption.label
        })
      }

      handleTargetChange = (selectedOption) => {
        this.setState({
            targetAirport: selectedOption.label
        })
      }

    clearPage(event){
        this.setState({airreservations:[]});
        this.setState({originAirport:''});
        this.setState({targetAirport:''});
        this.setState({departureDatetime:''});
        this.setState({arrivalDatetime:''});
        this.setState({airline:''});
    }

    handleSubmit(event){
        const encodedOriginAirport = encodeURIComponent(this.state.originAirport);
        const encodedTargetAirport = encodeURIComponent(this.state.targetAirport);
        const encodedDepartureDatetime = encodeURIComponent(this.state.departureDatetime);
        const encodedArrivalDatetime = encodeURIComponent(this.state.arrivalDatetime);
        const encodedAirline = encodeURIComponent(this.state.airline);

        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_GET_AIR_RESERVATIONS_WITH_FILTER+`?originAirport=${encodedOriginAirport}&targetAirport=${encodedTargetAirport}&departureDatetime=${encodedDepartureDatetime}&arrivalDatetime=${encodedArrivalDatetime}&airline=${encodedAirline}`

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
        const airports = this.state.airports;

        let options = []
        airports.forEach(element => {
            options.push({
                value:element.Id,
                label:element.Name + ' (' + element.Code + ')'
            })
        });
        options.push({value:0, label:""})
        return(
            <div className="container">
                <h3> Search Air Reservations </h3>
                    <Form>
                        <Form.Group >
                        <Form.Label>OriginAirport</Form.Label>
                            <Select options={options} onChange={this.handleOriginChange}/>

                            <Form.Label>TargetAirport</Form.Label>
                            <Select options={options} onChange={this.handleTargetChange}/>
                            
                            <Form.Label>DepartureDate</Form.Label>
                            <Form.Control type="date" name="departureDatetime" placeholder="Departure Datetime" value={this.state.departureDatetime} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>ArrivalDate</Form.Label>
                            <Form.Control type="date" name="arrivalDatetime" placeholder="Arrival Datetime" value={this.state.arrivalDatetime} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>Airline</Form.Label>
                            <Form.Control type="text" name="airline" placeholder="Airline" value={this.state.airline} onChange={this.handleInputChanged.bind(this)}/>                                                          
                        </Form.Group>

                            <Button variant="primary" onClick={this.handleSubmit.bind(this)}>
                                Submit
                            </Button>
                            <Button variant="primary" onClick={this.clearPage.bind(this)}>
                                Clear
                            </Button>
                    </Form>


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
