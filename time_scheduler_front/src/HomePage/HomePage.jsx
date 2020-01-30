import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { userActions, userTimesheetActions } from '../_actions';
import { TimeTable } from '../_components/Table/Timetable';

const user = JSON.parse(localStorage.user);
class HomePage extends React.Component {
    componentDidMount() {
        this.props.getUsers();
        this.props.getUserTimes(user.id);
    }

    handleDeleteUser(id) {
        return (e) => this.props.deleteUser(id);

    }

    render() {
        const { user, users, userTimes } = this.props;
        return (
            <div className="col-md-6 col-md-offset-3">
                <h1>Hi {user.username}!</h1>
                <p>You're logged in with React!!</p>
                <h3>All registered users:</h3>
                {users.loading && <em>Loading users...</em>}
                {users.error && <span className="text-danger">ERROR: {users.error}</span>}
                {users.items &&
                    <ul>
                        {users.items.map((user, index) =>
                            <li key={user.id}>
                                {user.username}
                                {
                                    user.deleting ? <em> - Deleting...</em>
                                    : user.deleteError ? <span className="text-danger"> - ERROR: {user.deleteError}</span>
                                    : <span> - <a onClick={this.handleDeleteUser(user.id)}>Delete</a></span>
                                }
                            </li>
                        )}
                    </ul>
                }

                <React.Fragment>
                    <TimeTable/>
                    </React.Fragment>
                <p>
                    <Link to="/login">Logout</Link>
                </p>
            </div>
        );
    }
}

function mapState(state) {
    const { users, authentication, userTimes} = state;
    const { user } = authentication;
    return { user, users, userTimes};
}

const actionCreators = {
    getUsers: userActions.getAll,
    deleteUser: userActions.delete,
    getUserTimes: userTimesheetActions.getAll
}

const connectedHomePage = connect(mapState, actionCreators)(HomePage);
export { connectedHomePage as HomePage };