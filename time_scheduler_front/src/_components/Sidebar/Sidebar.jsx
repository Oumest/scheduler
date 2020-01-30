import React, {Component} from 'react';
import { connect } from 'react-redux';

import SideNav, { Toggle, Nav, NavItem, NavIcon, NavText } from '@trendmicro/react-sidenav';

import './react-sidenav.css'
import {history} from '../../_helpers';

class Sidebar extends Component{
    constructor(props) {
            super(props);
        }
    render() {
        
        
        const loggedIn = this.props.authentication.loggedIn;

        return(
            <React.Fragment>
                {loggedIn &&
                <SideNav
                onSelect={(selected) => {
                    history.replace(selected, "passed state")
                }}
            >
                <SideNav.Toggle />
                <SideNav.Nav defaultSelected="home">
                    <NavItem eventKey="">
                        <NavIcon>
                            <i className="fa fa-fw fa-home" style={{ fontSize: '1.75em' }} />
                        </NavIcon>
                        <NavText>
                            My Times
                        </NavText>
                    </NavItem>
                    <NavItem eventKey="Dashboard">
                        <NavIcon>
                            <i className="fa fa-fw fa-dashboard" style={{ fontSize : '1.75em'}}/>
                        </NavIcon>
                        <NavText>
                            Dashboard
                        </NavText>
                    </NavItem>
                    <NavItem eventKey="Calendar">
                        <NavIcon>
                            <i className="fa fa-fw fa-calendar" style={{ fontSize : '1.75em'}}/>
                        </NavIcon>
                        <NavText>
                            Calendar
                        </NavText>
                    </NavItem>
                    {/* <NavItem eventKey="charts">
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
                    </NavItem> */}
                </SideNav.Nav>
            </SideNav>
                }
            </React.Fragment>
        )
    }
}

function mapStateToProps(state) {
    const { authentication } = state;
    return {
        authentication
    };
}

const connectedSidebar = connect(mapStateToProps)(Sidebar);
export { connectedSidebar as Sidebar };