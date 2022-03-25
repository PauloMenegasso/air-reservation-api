import React, {Component} from "react";
import { Table, Button } from "react-bootstrap";

export class AirReservation extends Component{

    constructor(props){
        super(props);
        this.state={airreservations:[]}
    }

    refreshList() {
        fetch(process.env.REACT_APP_BASE_URI+process.env.REACT_APP_GET_ALL_AIR_RESERVATIONS, {
            mode: 'cors',
            method: 'GET',
            headers: {'Access-Control-Allow-Origin': '*'}
          })
        .then(response => response.json())
        .then(response => {
            this.setState({airreservations:response});
        });
    }

    componentDidMount(){
        this.refreshList();
    }

    deleteAirReservation(event){
        const id = event.target.getAttribute("name");

        const encodedId = encodeURIComponent(id);

        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_REMOVE_AIR_RESERVATION +`?id=${encodedId}`
         fetch(url, {
            mode: 'cors',
            method:'DELETE',
            headers:{
                'Access-Control-Allow-Origin': '*',
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(response => response.json())
        .then(response => {
            this.setState({airreservations:[]});
            this.refreshList();
            alert(response.Message)
        })

        this.refreshList();        
    }

    render(){
        const {airreservations} = this.state;
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr key="header">
                            <th>OriginAirport</th>
                            <th>DepartureDatetime</th>
                            <th>TargetAirport</th>
                            <th>ArrivalDatetime</th>
                            <th>Airline</th>
                            <th>FlightNumber</th>
                            <th>NumberOfAdults</th>
                            <th>NumberOfKids</th>
                            <th>Delete</th>
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
                                <td>
                                    <Button name={ar.Id} variant="danger" onClick={this.deleteAirReservation.bind(this)}>
                                        X
                                    </Button>                                    
                                </td>
                            </tr>)}

                    </tbody>
                </Table>
            </div>
        )
    }
}