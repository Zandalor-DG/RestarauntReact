import { combineReducer } from 'redux'
import foodCategory from './foodCategory/foodCategoryReducer.jsx'
import header from './header/header.jsx'

export default combineReducer({
    foodCategory,
    header
    })