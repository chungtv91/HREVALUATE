"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
function addClass(element, classes) {
    classes.split(' ').forEach(function (clazz) {
        if (element.classList) {
            element.classList.add(clazz);
        }
        else if ((" " + element.className + " ").indexOf(" " + clazz + " ")) {
            element.className += " " + clazz;
        }
    });
}
function removeClass(element, classes) {
    classes.split(' ').forEach(function (clazz) {
        element.classList
            ? element.classList.remove(clazz)
            : (element.className = element.className.replace(clazz, ''));
    });
}
function classSet(element, classes) {
    var adding = [];
    var removing = [];
    Object.keys(classes).forEach(function (clazz) {
        if (clazz) {
            classes[clazz] ? adding.push(clazz) : removing.push(clazz);
        }
    });
    // Always remove before adding class because there might be a class which belong to both sets.
    // For example, the element will have class `a` after calling
    //  ```
    //  classSet(element, {
    //      'a a1 a2': true,
    //      'a b1 b2': false
    //  })
    //  ```
    removing.forEach(function (clazz) { return removeClass(element, clazz); });
    adding.forEach(function (clazz) { return addClass(element, clazz); });
}
exports.default = classSet;
//# sourceMappingURL=classSet.js.map