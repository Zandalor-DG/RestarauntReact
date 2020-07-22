import React from 'react';
import ReactDOM from 'react-dom';
import { connect } from 'react-redux';
import {getPosts} from "./foodCategoryActions.jsx"

class FoodCategory extends React.Component {

    componentDidMount() {
        this.props.getPosts(0);
    }

    render() {
        let posts = this.props.posts.records.map(item => {
            return (
                <div key={item.postId} className="post">
                    <div className="header">{item.header}</div>
                    <div className="content">{item.foodCategory}</div>
                </div>
                );
        });
        return (
            <div id="foodCategory">
                {posts}
            </div>
        );
    }
};

let mapProps = (state) => {
    return {
            posts : state.data,
            error : state.error
        }
};

let mapDispatch = (dispatch) => {
    return {
            getPosts : (index, tags) => dispatch(getPosts(index, tags))
        }
};

export default connect(mapProps, mapDispatch)(FoodCategory);