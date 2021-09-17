"use strict";
/**
 * FormValidation (https://formvalidation.io)
 * The best validation library for JavaScript
 * (c) 2013 - 2020 Nguyen Huu Phuoc <me@phuoc.ng>
 */
Object.defineProperty(exports, "__esModule", { value: true });
/**
 * Validate a date
 *
 * @param {string} year The full year in 4 digits
 * @param {string} month The month number
 * @param {string} day The day number
 * @param {boolean} [notInFuture] If true, the date must not be in the future
 * @returns {boolean}
 */
function isValidDate(year, month, day, notInFuture) {
    if (isNaN(year) || isNaN(month) || isNaN(day)) {
        return false;
    }
    if (year < 1000 || year > 9999 || month <= 0 || month > 12) {
        return false;
    }
    var numDays = [
        31,
        // Update the number of days in Feb of leap year
        (year % 400 === 0 || (year % 100 !== 0 && year % 4 === 0)) ? 29 : 28,
        31, 30, 31, 30, 31, 31, 30, 31, 30, 31,
    ];
    // Check the day
    if (day <= 0 || day > numDays[month - 1]) {
        return false;
    }
    if (notInFuture === true) {
        var currentDate = new Date();
        var currentYear = currentDate.getFullYear();
        var currentMonth = currentDate.getMonth();
        var currentDay = currentDate.getDate();
        return (year < currentYear
            || (year === currentYear && month - 1 < currentMonth)
            || (year === currentYear && month - 1 === currentMonth && day < currentDay));
    }
    return true;
}
exports.default = isValidDate;
//# sourceMappingURL=isValidDate.js.map