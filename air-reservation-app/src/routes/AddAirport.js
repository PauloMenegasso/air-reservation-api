import React, {Component} from "react";
import {Button, Form} from 'react-bootstrap'

export class AddAirport extends Component{
    constructor(props){
        super(props)

        this.state = {
            name:'',
            code:''
        }

    }

    handleInputChanged(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleSubmit(event){
        const encodedName = encodeURIComponent(this.state.name);
        const encodedCode = encodeURIComponent(this.state.code);

        const url = process.env.REACT_APP_BASE_URI+process.env.REACT_APP_INSERT_AIRPORT+`?name=${encodedName}&code=${encodedCode}`
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
        return(
            <div className="container">
                <h3> Add Airport </h3>
                    <Form>
                        <Form.Group >
                            <Form.Label>OriginAirport</Form.Label>
                            <Form.Control type="text" name="name" required placeholder="Name" value={this.state.name} onChange={this.handleInputChanged.bind(this)}/>
                            
                            <Form.Label>TargetAirport</Form.Label>
                            <Form.Control type="text" name="code" required placeholder="Code" value={this.state.code} onChange={this.handleInputChanged.bind(this)}/>                        
                        </Form.Group>

                            <Button variant="primary" onClick={this.handleSubmit.bind(this)}>
                                Insert
                            </Button>
                    </Form>
            </div>
        )
    }
}