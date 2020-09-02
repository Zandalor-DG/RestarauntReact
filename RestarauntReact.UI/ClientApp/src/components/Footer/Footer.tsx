import React from 'react';
import css from './Footer.module.css';

const Footer = () => {
  return (
    <nav className={css.footer}>
      <span>
        <a href="#s">Profile</a>
      </span>
      <span>
        <a href="#s">Message</a>
      </span>
      <span>
        <a href="#s">News</a>
      </span>
      <span>
        <a href="#s">Music</a>
      </span>
      <span>
        <a href="#s">Settings</a>
      </span>
    </nav>
  );
};

export default Footer;
