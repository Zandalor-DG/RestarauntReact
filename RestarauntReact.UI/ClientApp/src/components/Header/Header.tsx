import React from 'react';
import logo from './logo.svg';
import css from './Header.module.css';

const Header = () => {
  return (
    <header className={css.header}>
      <img src={logo} alt="logo"></img>
    </header>
  );
};

export default Header;
