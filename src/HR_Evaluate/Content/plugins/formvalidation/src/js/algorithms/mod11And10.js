"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * Implement modulus 11, 10 (ISO 7064) algorithm
 *
 * @param {string} value
 * @returns {boolean}
 */
function mod11And10(value) {
    var length = value.length;
    var check = 5;
    for (var i = 0; i < length; i++) {
        check = (((check || 10) * 2) % 11 + parseInt(value.charAt(i), 10)) % 10;
    }
    return (check === 1);
}
exports.default = mod11And10;
//# sourceMappingURL=mod11And10.js.map