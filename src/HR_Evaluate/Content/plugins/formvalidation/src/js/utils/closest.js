"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
function matches(element, selector) {
    var nativeMatches = element.matches || element.webkitMatchesSelector
        || element['mozMatchesSelector'] || element['msMatchesSelector']; // tslint:disable-line:no-string-literal
    if (nativeMatches) {
        return nativeMatches.call(element, selector);
    }
    // In case `matchesselector` isn't supported (such as IE10)
    // See http://caniuse.com/matchesselector
    var nodes = [].slice.call(element.parentElement.querySelectorAll(selector));
    return nodes.indexOf(element) >= 0;
}
function closest(element, selector) {
    var ele = element;
    while (ele) {
        if (matches(ele, selector)) {
            break;
        }
        ele = ele.parentElement;
    }
    return ele;
}
exports.default = closest;
//# sourceMappingURL=closest.js.map