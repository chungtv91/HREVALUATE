"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
var luhn_1 = require("./luhn");
var mod11And10_1 = require("./mod11And10");
var mod37And36_1 = require("./mod37And36");
var verhoeff_1 = require("./verhoeff");
exports.default = {
    luhn: luhn_1.default,
    mod11And10: mod11And10_1.default,
    mod37And36: mod37And36_1.default,
    verhoeff: verhoeff_1.default,
};
//# sourceMappingURL=index.js.map