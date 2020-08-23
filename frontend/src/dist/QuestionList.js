"use strict";
var __makeTemplateObject = (this && this.__makeTemplateObject) || function (cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};
exports.__esModule = true;
exports.QuestionList = void 0;
/** @jsx jsx */
var core_1 = require("@emotion/core");
var Styles_1 = require("./Styles");
var Question_1 = require("./Question");
exports.QuestionList = function (_a) {
    var data = _a.data, renderItem = _a.renderItem;
    return (core_1.jsx("ul", { css: core_1.css(templateObject_1 || (templateObject_1 = __makeTemplateObject(["\n        list-style: none;\n        margin: 10px 0 0 0;\n        padding: 0px 20px;\n        background-color: #fff;\n        border-bottom-left-radius: 4px;\n        border-bottom-right-radius: 4px;\n        border-top: 3px solid ", ";\n        box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);\n      "], ["\n        list-style: none;\n        margin: 10px 0 0 0;\n        padding: 0px 20px;\n        background-color: #fff;\n        border-bottom-left-radius: 4px;\n        border-bottom-right-radius: 4px;\n        border-top: 3px solid ", ";\n        box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);\n      "])), Styles_1.accent2) }, data.map(function (question) { return (core_1.jsx("li", { key: question.questionId, css: core_1.css(templateObject_2 || (templateObject_2 = __makeTemplateObject(["\n            border-top: 1px solid ", ";\n            :first-of-type {\n              border-top: none;\n            }\n          "], ["\n            border-top: 1px solid ", ";\n            :first-of-type {\n              border-top: none;\n            }\n          "])), Styles_1.gray5) }, renderItem ? renderItem(question) : core_1.jsx(Question_1.Question, { data: question }))); })));
};
var templateObject_1, templateObject_2;
