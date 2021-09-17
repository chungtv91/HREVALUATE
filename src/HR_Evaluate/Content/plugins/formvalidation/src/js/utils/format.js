"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * Format a string
 * It's used to format the error message
 * format('The field must between %s and %s', [10, 20]) = 'The field must between 10 and 20'
 *
 * @param {string} message
 * @param {string|string[]} parameters
 * @returns {string}
 */
function format(message, parameters) {
    var params = Array.isArray(parameters) ? parameters : [parameters];
    var output = message;
    params.forEach(function (p) {
        output = output.replace('%s', p);
    });
    return output;
}
exports.default = format;
//# sourceMappingURL=format.js.map