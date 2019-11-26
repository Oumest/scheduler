import React, { Component } from 'react';
import Table from 'react-bootstrap/Table'
import Spinner from 'react-bootstrap/Spinner'

export default class Timesheet extends Component{
    constructor(props) {
        super(props);
        //this.render.bind(this)
        this.state = {                    // Maybe store all table values in array? Probably send values in with props?
            "Date" : "2019-11-12",
            "Begin" : "09:00",
            "End" : "10:00",
            "Duration" : "01:00 h",
            "Rate" : "-",
            "Customer" : "Test Customer",
            "Project" : "Test project",
            "Activity" : "Testing",
            "Description" : "Testing desc",
            "Tags" : "test",
            "Actions" : ""                  // Should have dropboxes with options in them
        };
      }
      componentDidMount = () => {
        var obj = {"Date" : "2019-11-12",
        "Begin" : "09:00",
        "End" : "10:00",
        "Duration" : "01:00 h",
        "Rate" : "-",
        "Customer" : "Test Customer",
        "Project" : "Test project",
        "Activity" : "Testing",
        "Description" : "Testing desc",
        "Tags" : "test",
        "Actions" : "",
        "TableHeight" : ""
      }
            var Dates = [obj]
            this.setState({Dates})
        console.log(this.state)
      }
      render(){
        if (!this.state.Dates) {
          return <Spinner animation="border" role="status">
          <span className="sr-only">Loading...</span>
        </Spinner>
      }
      else{
          return(
            
            <Table id ="table" responsive striped bordered hover size="md ">
            <thead>
              <tr>
                <th>Date</th>
                <th>Begin</th>
                <th>End</th>
                <th>Duration</th>
                <th>Rate</th>
                <th>Customer</th>
                <th>Project</th>
                <th>Activity</th>
                <th>Description</th>
                <th>Tags</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
                {this.state.Dates && this.state.Dates.map(
                  date => (
                    <tr>
                    <td>{date.Date}</td>
                    <td>{date.Begin}</td>
                    <td>{date.End}</td>
                    <td>{date.Duration}</td>
                    <td>{date.Rate}</td>
                    <td>{date.Customer}</td>
                    <td>{date.Project}</td>
                    <td>{date.Activity}</td>
                    <td>{date.Description}</td>
                    <td>{date.Tags}</td>
                    <td>{date.Actions}</td>
                    </tr>
                  )
                )}
            </tbody>
          </Table>
            
          );
      }
    }
}