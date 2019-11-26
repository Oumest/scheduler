import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Switch} from "react-router-dom";
import Home from "./Home";
import Profile from './Profile';
import NoMatch from './NoMatch';
import Timesheet from './Timesheet';
import Calendar from './Calendar';
import Invoice from './Invoice';
import logo from './logo.svg';
import './App.css';
import Dashboard from './components/dashboard';
import { createBrowserHistory as createHistory } from 'history'
const history = createHistory();

class App extends Component {
  constructor(props) {
    super(props);
    //this.render.bind(this)
    this.state = {
    };
  }
  componentDidMount=()=>{
    console.log(this.props)
  }
  render(){
  return (
    <React.Fragment>
      <Dashboard/>
      <Router history={history}>
        <Switch>
        <Route exact path="/" component={Home} />
             <Route path="/timesheet" component={Timesheet} />
             <Route path="/profile" component={Profile} />
             <Route path="/calendar" component={Calendar}/>
             <Route path="/invoice" component={Invoice}/>
        </Switch>
      </Router>
    </React.Fragment>
  );
  }
}

export default App;
