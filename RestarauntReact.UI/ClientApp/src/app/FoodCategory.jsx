let React = require("react");
let connect = reuire("react-redux").connect;
let actions = reqire("./actions.jsx");

class FoodCategoryForm extends React.Component {
    constructor(props) {
        super(props);
    }

    onClick() {
        if (this.refs.itemControllerInput.value !== "") {
            let itemText = this.refs.itemControllerInput.value;
            this.refs.itemControllerInput.value = "";
            return this.props.addItemController(itemText);
        }
    }

    render() {
        return (<div>
                    <input ref="itemControllerInput"/>
                    <button onClick={this.onClick.bind(this)}>Добавить</button>
                </div>);
    }
};

class FoodCategoryItem extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (<div>
                    <p>
                        <b>{this.props.text}</b>
                        <br/>
                        <button onClick={() => this.props.deleteItemController(this.props.text)}>Удалить</button>
                    </p>
                </div>);
    }
};

class FoodCategoryList extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                {this.props.phones.map(item =>
                    <FoodCategoryItem key={item}
                        text={item}
                        deleteItemController={this.props.deleteItemController} />
                )}
            </div>);
    }
};

class AppView extends React.Component {
    render() {
        return (<div>
            <FoodCategoryForm addItemController={this.props.addItemController} />
            <FoodCategoryList {...this.props}/>
        </div>);
    }
};

function mapStateToProps(state) {
    return {
            itemController : state.get("itemController")
        };
}

module.exports = connect(mapStateToProps, actions) (AppView);