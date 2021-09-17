"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * Implements Mod 37, 36 (ISO 7064) algorithm
 *
 * @param {string} value
 * @param {string} [alphabet]
 * @returns {boolean}
 */
function mod37And36(value, alphabet) {
    if (alphabet === void 0) { alphabet = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ'; }
    var length = value.length;
    var modulus = alphabet.length;
    var check = Math.floor(modulus / 2);
    for (var i = 0; i < length; i++) {
        check = (((check || modulus) * 2) % (modulus + 1) + alphabet.indexOf(value.charAt(i))) % modulus;
    }
    return (check === 1);
}
exports.default = mod37And36;
//# sourceMappingURL=mod37And36.js.map