import React, { Component } from 'react';

export class HomeIndexData extends Component {
    static displayName = HomeIndexData.name;

    constructor(props) {
        super(props);
        this.state = {
                foodCategory: {
                        admin: false,
                        sort: null,
                        descending: false,
                        foodsCategories: []
            },
                loading: true
            };
    }

    componentDidMount() {
        this.populateHomeIndexData();
    }

    static renderForecastsTable(foodCategory) {
        const isAdminStatus = foodCategory.admin
            ? <a asp-action="CreateOrUpdateFoodCategory" asp-controller="FoodCategory">Создать категорию</a>
            : '';
        const urlCreateOrUpdateFoodCategory = "/FoodCategory/CreateOrUpdateFoodCategory";
        return (
                <table className="table">
                    <thead id="tableHeader">
                    <tr>
                        <th>Images</th>
                        <th>
                            <a className="sortOrderClass" href="#" onClick="sortTableFoodCategory('@SortFoodCategory.Name', '@orderId', this)">
                                Название категории
                                <span className="spanArrow"></span>
                            </a>
                        </th>
                        <th>Еда</th>
                        <th>Допы</th>
                        <th>Редактировать</th>
                        <th> Удалить </th>
                    </tr>
                    </thead>
                    <tbody>
                    {foodCategory.foodsCategories.map(foodsCategory =>
                            <tr key={foodsCategory.name} className="dataRow">
                                <td className="thumbnail-img">
                                    <a href="#">
                                        <img alt="" classname="img-fluid" src="~/images/img-pro-01.jpg"/>
                                    </a>
                                </td>
                                <td className="total-pr">
                                    <p>
                                        {foodsCategory.name}
                                    </p>
                                </td>
                                <td className="name-pr">
                                    <a href="">Посмотреть</a>
                                </td>
                                <td className="name-pr">
                                    <a asp-action="Index" asp-controller="FoodItemExtra" asp-route-id="@foodCategory.Id">Допы</a>
                                </td>

                                <td className="remove-pr">
                                    <a href="/FoodCategory/CreateOrUpdateFoodCategory" asp-route-id="@foodCategory.Id">Редактировать</a>
                                </td>
                                <td className="remove-pr">
                                <a href="/FoodCategory/ConfirmDeleteFoodCategory" asp-route-id="@foodCategory.Id">
                                        <i className="fas fa-times"></i>
                                    </a>
                                </td>

                            </tr>
                        )}
                    </tbody>
                </table>
            );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : HomeIndexData.renderForecastsTable(this.state.foodCategory);

        return (
            <div>
                <h1 id="tabelLabel" >FoodCategory</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateHomeIndexData() {
        const response = await fetch('Home/Index');
        const data = await response.json();
        this.setState({ foodCategory: data, loading: false });
    }
}