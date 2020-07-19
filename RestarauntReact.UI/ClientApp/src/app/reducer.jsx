let Map = require("immutable").Map;

let reducer = function(state = Map(), action) {
    switch (action.type) {
        case "SET_STATE":
            return state.merge(action.state);
        case "ADD_ITEM_CONTROLLER":
            return state.update("itemController",
                (itemController) => itemController.push(
                    action.itemController
                )
            );
        case "DELETE_ITEM_CONTROLLER":
            return state.update("itemController",
                    (itemController) => itemController.filterNot(
                        (item) => item === action.itemController
                    )
                );
    }
    return state;
}

module.exports = reducer;