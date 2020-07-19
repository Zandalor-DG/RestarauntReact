let addItemController = function (itemController) {
    return {
        type: "ADD_ITEM_CONTROLLER",
        itemController
        }
};
let deleteItemController = function (itemController) {
    return{
        type: "DELETE_ITEM_CONTROLLER",
        itemController
        }
};
module.exports = { addItemController: addItemController, deleteItemController : deleteItemController};