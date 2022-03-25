import { Alert } from "bootstrap";
import React, {Component} from "react";
import { Table, Button } from "react-bootstrap";

export class Airports extends Component{

    constructor(props){
        super(props)

        this.state={airports:[]};
    }

    refreshList(){
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
        this.refreshList();
    }

    editAirport(event){
        const code = event.target.getAttribute("name");

        const encodedCode = encodeURIComponent(code);
        
        const newName = prompt("New Name");
        const newCode = prompt("New Code");

        const encodedNewName = encodeURIComponent(newName);
        const encodedNewCode = encodeURIComponent(newCode);

        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_UPDATE_AIRPORT+`?code=${encodedCode}&newName=${encodedNewName}&newCode=${encodedNewCode}`
         fetch(url, {
            mode: 'cors',
            method:'PUT',
            headers:{
                'Access-Control-Allow-Origin': '*',
                'Accept':'application/json',
                'Content-Type':'application/json'
            }
        })
        .then(response => response.json())
        .then(response => {
            this.setState({airports:[]});
            this.refreshList();
            alert(response.Message)
        })  
    }

    deleteAirport(event){
        const code = event.target.getAttribute("name");

        const encodedCode = encodeURIComponent(code);

        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_REMOVE_AIRPORT+`?code=${encodedCode}`
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
            this.setState({airports:[]});
            this.refreshList();
            alert(response.Message)
        })
    }

    render(){
        const {airports} = this.state;
        return(
            <div>
                <Table className="mt-4" striped bordered hover size="sm">
                    <thead>
                        <tr key="header">
                            <th>Name</th>
                            <th>Code</th>
                            <th>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        {airports.map(ar=>
                            <tr key={ar.Id}>
                                <td>{ar.Name}</td>
                                <td>{ar.Code}</td>
                                <td>
                                <Button name={ar.Code} variant="primary" onClick={this.editAirport.bind(this)}>
                                    Edit
                                </Button>
                                <Button name={ar.Code} variant="danger" onClick={this.deleteAirport.bind(this)}>
                                    Delete
                                </Button>
                                </td>
                            </tr>)}
                    </tbody>
                </Table>
            </div>
        )
    }
}