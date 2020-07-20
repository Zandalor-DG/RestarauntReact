import React from 'react';
import { Link } from 'react-router-dom';

export default class Headers extends React.Component {
    render() {
        return (
            <header>
                <menu>
                    <ul>
                        <li>
                            <Link to="/">Еда</Link>
                        </li>
                        <li>
                            <Link to="/about">Обо мне</Link>
                        </li>
                    </ul>
                </menu>
            </header>
            );
    }
};