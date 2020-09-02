import React from 'react';
import css from './TableRestaurant.module.css';

const TableRestaurant = () => {
  return (
    <div className={css.headRestaurant}>
      <div>
        <input value="search" />
        <button> Search </button>
      </div>
    </div>
  );
};

export default TableRestaurant;
