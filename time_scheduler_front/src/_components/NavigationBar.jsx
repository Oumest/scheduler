import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import { connect } from 'react-redux';
import { slide as Menu } from 'react-burger-menu'

class NavigationBar extends Component {
  showSettings(event) {
    event.preventDefault();
  }
    render() {
        const user = this.props.authentication.user;

        return (
          <React.Fragment>
            {user &&
          <Menu>
          <a id="home" className="menu-item" href="/">Home</a>
          <a id="about" className="menu-item" href="/about">About</a>
          <a id="contact" className="menu-item" href="/contact">Contact</a>
          <a onClick={ this.showSettings } className="menu-item--small" href="">Settings</a>
        </Menu> 
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

const connectedNavigationBar = connect(mapStateToProps)(NavigationBar);
export { connectedNavigationBar as NavigationBar };