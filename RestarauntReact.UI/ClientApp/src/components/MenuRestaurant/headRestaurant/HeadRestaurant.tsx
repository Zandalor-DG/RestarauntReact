import React from 'react';
import css from './HeadRestaurant.module.css';

const HeadRestaurant = () => {
  return (
    <div className={css.headRestaurant}>
      <div>
        <input value="search" />
        <button> Search </button>
      </div>
    </div>
  );
};

export default HeadRestaurant;
