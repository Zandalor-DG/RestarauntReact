import React from 'react';
import './App.css';
import Header from './components/Header/Header';
import MenuRestaurant from './components/MenuRestaurant/MenuRestaurant';
import Footer from './components/Footer/Footer';

const App = () => {
  return (
    <div className="app-wrapper">
      <Header />
      <MenuRestaurant />
      <Footer />
    </div>
  );
};

export default App;
