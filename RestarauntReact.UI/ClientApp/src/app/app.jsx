let React = require("react");
let ReactDOM = require("react-dom");
let redux = require("redux");
let Provider = require("react-redux").Provider;
let reducer = require("./reducer");
let AppView = require("./FoodCategory");


let store = redux.createStore(reducer);

export class app extends Component {
    static displayName = HomeIndexData.name;

    constructor(props) {
        super(props);
        this.state = {
                foodCategory : {
                        admin : false,
                        sort : null,
                        descending : false,
                        foodsCategories : []
                    },
                loading : true
            };
    }

    componentDidMount() {
        this.populateHomeIndexData();
    }

    static populateHomeIndexData() {
        const response = await fetch('Home/Index');
        const data = await response.json();
        this.setState({ foodCategory : data, loading : false });
    }
}

store.dispatch({
        type: "SET_STATE",
    state: {
            foodCategory : app.populateHomeIndexData()
            }
});

ReactDOM.render(
        <Provider store={store}>
            <AppView />
        </Provider>,
        document.getElementById("container")
    );