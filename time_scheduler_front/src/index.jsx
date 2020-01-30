import React from 'react';
import { render } from 'react-dom';
import { Provider } from 'react-redux';

import { store } from './_helpers';
import { App } from './App';
import { Header } from './_components/Header';
import { Sidebar } from './_components/Sidebar/Sidebar';


render(
    <Provider store={store}>
        <Header/>
        <Sidebar/>
        <App />
    </Provider>,
    document.getElementById('app')
);