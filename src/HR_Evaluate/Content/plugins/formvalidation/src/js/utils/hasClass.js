"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
function hasClass(element, clazz) {
    return element.classList
        ? element.classList.contains(clazz)
        : new RegExp("(^| )" + clazz + "( |$)", 'gi').test(element.className);
}
exports.default = hasClass;
//# sourceMappingURL=hasClass.js.map