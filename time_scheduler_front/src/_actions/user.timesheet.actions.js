import { timesheetConstants } from '../_constants';
import { userTimesheetService } from '../_services';
import { alertActions } from './';
import { history } from '../_helpers';

export const userTimesheetActions = {
    getAll
}

function getAll(userId){
    console.log(userId)
    return dispatch => {
        dispatch(request(userId));

        userTimesheetService.getAll(userId)
            .then(
                userTimes => dispatch(success(userTimes)),
                error => {
                    dispatch(failure(error.toString()));
                    dispatch(alertActions.error(error.toString()))
                }
            );
    };
    
    function request() { return {type: timesheetConstants.GETALL_REQUEST}}
    function success(userTimes) {return {type: timesheetConstants.GETALL_SUCCESS, userTimes}}
    function failure(error) {return {type: timesheetConstants.GETALL_FAILURE, error}}
}