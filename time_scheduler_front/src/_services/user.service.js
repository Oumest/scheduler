import { authHeader } from '../_helpers';
import config from 'config';
import {URL, LOGIN, REGISTER, ALLUSERS} from '../_helpers/const';
//import { url } from 'inspector';

export const userService ={
    login,
    logout,
    register,
    getAll,
    //getById,
    update,
    delete: _delete,
    getUserInfo
}

async function getUserInfo(username){ // BACKEND_ACCOUNTINFO existerar ej. Lägg till i backend
    const data = {
        "username" : username
    }
    const requestOptions = {
        method: 'Post',
        headers: {'Content-Type': 'application/json; charset=UTF-8'},
        body: JSON.stringify(data)
    }
    var response = await fetch(URL + BACKEND_ACCOUNTINFO, requestOptions)
    var val = await response.json();
    
    response = handleUserInfo(val)

    return response;
}

function login(username, password){ // login method to backend. @param1: username, @param2: password
    const data = {
        "username" : username,
        "password" : password
    }
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json; charset=UTF-8' },
        body: JSON.stringify(data)
    };
    return fetch(URL + LOGIN, requestOptions)
        .then(handleResponse)
        .then(user => {
            // store user details with token in localstorage to keep logged in between page refresh
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        })
}

function logout() {
    localStorage.removeItem('user');
}

function register(user){ // Needs "username", "password" and "email"
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };
    return fetch(URL + REGISTER, requestOptions).then(handleResponse);
}

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(URL + ALLUSERS, requestOptions).then(handleResponse);
}

function update(user) {
    const requestOptions = {
        method : 'PUT',
        headers: {...authHeader(), 'Content-Type': 'application/json'},
        body: JSON.stringify(user)
    };
    return fetch(URL, requestOptions).then(handleResponse); // + userid link
}

function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    }
    return fetch(URL, requestOptions).then(handleResponse); // change url to get user by id link
}

function handleResponse(response){
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if(!response.ok){
            if(response.status === 401) {
                //auto logout if api returns status 401
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }
        return data;
    })
}