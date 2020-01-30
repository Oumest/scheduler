import {URL, USERTIMESHEETS} from '../_helpers/const';

export const userTimesheetService = {
    getAll
}

function getAll(userId){
    console.log("hej")
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json; charset=UTF-8'}
    };

    return fetch(URL + USERTIMESHEETS + userId, requestOptions)
        .then(handleResponse)
        .then(userTimes => {
            localStorage.setItem('userTimes', JSON.stringify(userTimes));
            return userTimes;
        })
}

function handleResponse(response){
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if(!response.ok){
            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }
        return data;
    })
}