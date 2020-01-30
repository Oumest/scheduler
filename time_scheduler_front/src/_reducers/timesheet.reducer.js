import {timesheetConstants} from '../_constants';

export function userTimes(state = {}, action){
    switch(action.type) {
        case timesheetConstants.GETALL_REQUEST:
            return {
                loading: true
            };
        case timesheetConstants.GETALL_SUCCESS:
            return{
                items: action.userTimes
            };
        case timesheetConstants.GETALL_FAILURE:
            return{
                error: action.error
            };
        
        default:
            return state
    }
}