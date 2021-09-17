"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * Execute a callback function
 *
 * @param {Function | string} functionName Can be
 * - name of global function
 * - name of namespace function (such as A.B.C)
 * - a function
 * @param {any[]} args The callback arguments
 * @return {any}
 */
function call(functionName, args) {
    if ('function' === typeof functionName) {
        return functionName.apply(this, args);
    }
    else if ('string' === typeof functionName) {
        // Node that it doesn't support node.js based environment because we are trying to access `window`
        var name_1 = functionName;
        if ('()' === name_1.substring(name_1.length - 2)) {
            name_1 = name_1.substring(0, name_1.length - 2);
        }
        var ns = name_1.split('.');
        var func = ns.pop();
        var context = window;
        for (var _i = 0, ns_1 = ns; _i < ns_1.length; _i++) {
            var t = ns_1[_i];
            context = context[t];
        }
        return (typeof context[func] === 'undefined') ? null : context[func].apply(this, args);
    }
}
exports.default = call;
//# sourceMappingURL=call.js.map