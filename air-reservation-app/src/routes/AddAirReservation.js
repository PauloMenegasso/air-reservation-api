import React, {Component} from "react";
import {Button, Form} from 'react-bootstrap'
import Select from 'react-select'

export class AddAirReservation extends Component{
    constructor(props){
        super(props);

        this.state = {
            originAirport: '',
            targetAirport: '',
            departureDatetime:'',
            arrivalDatetime:'',
            airline:'',
            flightNumber:'',
            numberOfAdultPassagers:'',
            numberOfChieldPassagers:'',
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

    handleSubmit(event){
        const encodedOriginAirport = encodeURIComponent(this.state.originAirport);
        const encodedTargetAirport = encodeURIComponent(this.state.targetAirport);
        const encodedDepartureDatetime = encodeURIComponent(this.state.departureDatetime);
        const encodedArrivalDatetime = encodeURIComponent(this.state.arrivalDatetime);
        const encodedAirline = encodeURIComponent(this.state.airline);
        const encodedFlight = encodeURIComponent(this.state.flightNumber);
        const encodedAdults = encodeURIComponent(this.state.numberOfAdultPassagers);
        const encodedKids = encodeURIComponent(this.state.numberOfChieldPassagers);

        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_INSERT_AIR_RESERVATION+`?originAirport=${encodedOriginAirport}&targetAirport=${encodedTargetAirport}&departureDatetime=${encodedDepartureDatetime}&arrivalDatetime=${encodedArrivalDatetime}&airline=${encodedAirline}&flightNumber=${encodedFlight}&numberOfAdults=${encodedAdults}&numberOfKids=${encodedKids}`
         fetch(url, {
            mode: 'cors',
            method:'POST',
            headers:{
                'Access-Control-Allow-Origin': '*',
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(response => response.json())
        .then(response => alert(response.Message))
    }

    render(){
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
                <h3> Add Air Reservation </h3>
                    <Form>
                        <Form.Group >

                            <Form.Label>OriginAirport</Form.Label>
                                <Select options={options} onChange={this.handleOriginChange}/>

                                <Form.Label>TargetAirport</Form.Label>
                                <Select options={options} onChange={this.handleTargetChange}/>
                            
                            <Form.Label>DepartureDatetime</Form.Label>
                            <Form.Control type="datetime-local" name="departureDatetime" required placeholder="Departure Datetime" value={this.state.departureDatetime} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>ArrivalDatetime</Form.Label>
                            <Form.Control type="datetime-local" name="arrivalDatetime" required placeholder="Arrival Datetime" value={this.state.arrivalDatetime} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>Airline</Form.Label>
                            <Form.Control type="text" name="airline" required placeholder="Airline" value={this.state.airline} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>FlightNumber</Form.Label>
                            <Form.Control type="number" name="flightNumber" required placeholder="Flight Number" value={this.state.flightNumber} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>NumberOfAdultPassagers</Form.Label>
                            <Form.Control type="number" name="numberOfAdultPassagers" required placeholder="Number Of Adult Passagers" value={this.state.numberOfAdultPassagers} onChange={this.handleInputChanged.bind(this)}/>                        
                            
                            <Form.Label>NumberOfChieldPassagers</Form.Label>
                            <Form.Control type="number" name="numberOfChieldPassagers" required placeholder="Number Of Child Passagers" value={this.state.numberOfChieldPassagers} onChange={this.handleInputChanged.bind(this)}/>                                           
                        </Form.Group>

                            <Button variant="primary" onClick={this.handleSubmit.bind(this)}>
                                Submit
                            </Button>
                    </Form>
            </div>
        )
    }
}
