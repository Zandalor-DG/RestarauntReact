import React from 'react';
import css from './MenuRestaurant.module.css';
import HeadRestaurant from './headRestaurant/HeadRestaurant';

const MenuRestaurant = () => {
  return (
    <div className={css.tableRestaurant}>
      <HeadRestaurant />
      <div>ava-description</div>
      <div>
        My post
        <div>New post</div>
        <div>
          <div>Post 1</div>
          <div>Post 2</div>
        </div>
      </div>
    </div>
  );
};

export default MenuRestaurant;
