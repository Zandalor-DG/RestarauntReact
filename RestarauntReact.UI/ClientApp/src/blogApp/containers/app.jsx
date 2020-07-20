import React from 'react';
import ReactDom from 'react-dom';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import Header from './header/header.jsx';
import About from './about/about.jsx';
import FoodCategory from './foodCategory/foodCategory.jsx';


export default class App extends React.Component {
    render() {
        return (
            <Router>
                <div>
                    <Header />
                    <main>
                        <Switch>
                            <Route path="/about" component={About} />
                            <Route path="/" component={FoodCategory} />
                        </Switch>
                        </main>
                </div>
            </Router>
        );
    }
};