"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * @param {HTMLElement} form The form element
 * @param {string} field The field name
 * @param {HTMLElement} element The field element
 * @param {HTMLElement[]} elements The list of elements which have the same name as `field`
 * @return {string}
 */
function getFieldValue(form, field, element, elements) {
    var type = (element.getAttribute('type') || '').toLowerCase();
    var tagName = element.tagName.toLowerCase();
    switch (tagName) {
        case 'textarea':
            return element.value;
        case 'select':
            var select = element;
            var index = select.selectedIndex;
            return (index >= 0) ? select.options.item(index).value : '';
        case 'input':
            if ('radio' === type || 'checkbox' === type) {
                var checked = elements.filter(function (ele) { return ele.checked; }).length;
                return checked === 0 ? '' : checked + '';
            }
            else {
                return element.value;
            }
        default:
            return '';
    }
}
exports.default = getFieldValue;
//# sourceMappingURL=getFieldValue.js.map