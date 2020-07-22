// import 'bootstrap/dist/css/bootstrap.css';
// import React from 'react';
// import ReactDOM from 'react-dom';
// import { BrowserRouter } from 'react-router-dom';
// import App from './App';
// import registerServiceWorker from './registerServiceWorker';
//
// const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
// const rootElement = document.getElementById('root');
//
// ReactDOM.render(
//   <BrowserRouter basename={baseUrl}>
//     <App />
//   </BrowserRouter>,
//   rootElement);
//
// registerServiceWorker();
//

import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import thunk from 'redux-thunk';
import App from "./blogApp/containers/app";
import registerServiceWorker from './registerServiceWorker';
import rootReducer from './blogApp/containers/rootReducer.jsx';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

function configureStore(initialState) {
    return createStore(rootReducer, initialState, applyMiddleware(thunk))
}

const store = configureStore()

ReactDOM.render(
    <Provider store={store}>
         <App />
    </Provider>,
       rootElement);

registerServiceWorker();