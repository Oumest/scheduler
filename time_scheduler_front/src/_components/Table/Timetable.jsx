import BootstrapTable from 'react-bootstrap-table-next';
import { connect } from 'react-redux';
import React, { Component } from 'react';
import cellEditFactory from 'react-bootstrap-table2-editor';
import paginationFactory from 'react-bootstrap-table2-paginator';


const columns = [{
    dataField: 'id',
    text: 'Product ID',
    headerStyle: {
        padding: '40px'
      }
  }, {
    dataField: 'date',
    text: 'Date',
    headerStyle: {
        padding: '40px'
      }
  }, {
    dataField: 'duration',
    text: 'Duration',
    headerStyle: {
        padding: '40px'
      }
  },
  {
    dataField: 'customer',
    text: 'Customer',
    headerStyle: {
        padding: '40px'
      }
  },
  {
    dataField: 'project',
    text: 'Project',
    headerStyle: {
        padding: '40px'
      }
  },
  {
    dataField: 'activity',
    text: 'Activity',
    headerStyle: {
        padding: '40px'
      }
  },
  {
    dataField: 'description',
    text: 'Description',
    headerStyle: {
        padding: '40px'
      }
  },];

  const products = [
      {
      id: 1,
      date: "2020-01-29",
      duration: "02:00 h",
      customer: "test customer",
      project: "test project",
      activity: "test activity",
      description : "test description"
    }
]
const items = JSON.parse(localStorage.userTimes);
const vals = items.map(timesheets => ({date: timesheets.Start_time, 
    duration: timesheets.Duration,
    customer: timesheets.Customer_name,
    project: timesheets.Project_name,
    activity: timesheets.Activity_name,
    description: timesheets.Description
}))

class TimeTable extends Component{
    
    render(){
        return(
            <React.Fragment>
            <BootstrapTable keyField='id' data={ vals } columns={ columns } noDataIndication={ noDataIndication } columns={ columns } pagination={ paginationFactory() } />
            </React.Fragment>
        )
    }
}

function noDataIndication(){
    console.log(items[0])
    return "Table is empty"
}

function mapStateToProps(state) {
    const { authentication } = state;
    return {
        authentication
    };
}

const connectedTimeTable = connect(mapStateToProps)(TimeTable);
export { connectedTimeTable as TimeTable };