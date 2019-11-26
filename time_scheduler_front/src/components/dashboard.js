import React, {Component} from 'react';
import SideNav, { Toggle, Nav, NavItem, NavIcon, NavText } from '@trendmicro/react-sidenav';
// Be sure to include styles at some point, probably during your bootstraping
import '@trendmicro/react-sidenav/dist/react-sidenav.css';  
import {DEFAULT_PAGE} from './helpers/const'

export default class Dashboard extends Component{
    constructor(props) {
        super(props);
        //this.routeChange = this.routeChange.bind(this);
        //this.render.bind(this)
        this.state = {
        };
      }
      routeChange = (newRoute) =>{
          //const to = '/' + newRoute;
          //if(window.location.href !== to){

          //}
        var oldLocation = DEFAULT_PAGE;
        let path = newRoute;
        var location = oldLocation + path
        console.log("props")
          console.log(this.props)
        //window.location.href = location
        //return location
      }
      render(){
          return(
              <div>
            <SideNav
            onSelect={(selected) => {
                this.routeChange(selected)
            }}
        >
            <SideNav.Toggle />
            <SideNav.Nav defaultSelected="Timesheet">
                <NavItem eventKey="Timesheet" href="/timesheet">
                    <NavIcon>
                        <i className="fa fa-fw fa-timesheet" style={{ fontSize: '1.75em' }} />
                    </NavIcon>
                    <NavText>
                        Timesheet
                    </NavText>
                </NavItem>
                <NavItem eventKey="Calendar" href="/calendar">
                    <NavIcon>
                    <i className="fa fa-fw fa-calendar" style={{ fontSize: '1.75em' }} />
                    </NavIcon>
                    <NavText>
                        Calendar
                    </NavText>
                </NavItem>
                <NavItem eventKey="Invoice" href="/invoice">
                    <NavIcon>
                    <i className="fa fa-fw fa-invoice" style={{ fontSize: '1.75em' }} />
                    </NavIcon>
                    <NavText>
                        Invoice
                    </NavText>
                </NavItem>
                <NavItem eventKey="Profile" href="/profile">
                    <NavIcon>
                    <i className="fa fa-fw fa-profile" style={{ fontSize: '1.75em' }} />
                    </NavIcon>
                    <NavText>
                        Profile
                    </NavText>
                </NavItem>
                <NavItem eventKey="charts">
                    <NavIcon>
                        <i className="fa fa-fw fa-line-chart" style={{ fontSize: '1.75em' }} />
                    </NavIcon>
                    <NavText>
                        Charts
                    </NavText>
                    <NavItem eventKey="charts/linechart">
                        <NavText>
                            Line Chart
                        </NavText>
                    </NavItem>
                    <NavItem eventKey="charts/barchart">
                        <NavText>
                            Bar Chart
                        </NavText>
                    </NavItem>
                </NavItem>
            </SideNav.Nav>
        </SideNav>
        </div>
          );
      }
}