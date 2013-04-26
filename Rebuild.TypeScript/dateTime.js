/// <reference path="../Rebuild.TypeScript.Test/timeSpan.ts" />
var DayOfWeek;
(function (DayOfWeek) {
    DayOfWeek._map = [];
    DayOfWeek._map[0] = "sunday";
    DayOfWeek.sunday = 0;
    DayOfWeek._map[1] = "monday";
    DayOfWeek.monday = 1;
    DayOfWeek._map[2] = "tuesday";
    DayOfWeek.tuesday = 2;
    DayOfWeek._map[3] = "wednesday";
    DayOfWeek.wednesday = 3;
    DayOfWeek._map[4] = "thursday";
    DayOfWeek.thursday = 4;
    DayOfWeek._map[5] = "friday";
    DayOfWeek.friday = 5;
    DayOfWeek._map[6] = "saturday";
    DayOfWeek.saturday = 6;
})(DayOfWeek || (DayOfWeek = {}));
var DateTime = (function () {
    function DateTime(ms) {
        this.ms = ms;
    }
    DateTime.dayOfWeeks = [
        DayOfWeek.sunday, 
        DayOfWeek.monday, 
        DayOfWeek.tuesday, 
        DayOfWeek.wednesday, 
        DayOfWeek.thursday, 
        DayOfWeek.friday, 
        DayOfWeek.saturday
    ];
    DateTime.daysToMonth365 = [
        0, 
        31, 
        59, 
        90, 
        120, 
        151, 
        181, 
        212, 
        243, 
        273, 
        304, 
        334, 
        365
    ];
    DateTime.daysToMonth366 = [
        0, 
        31, 
        60, 
        91, 
        121, 
        152, 
        182, 
        213, 
        244, 
        274, 
        305, 
        335, 
        366
    ];
    DateTime.prototype.addDays = function (days) {
        return new DateTime(this.ms + days * 86400000);
    };
    DateTime.prototype.addHours = function (hours) {
        return new DateTime(this.ms + hours * 3600000);
    };
    DateTime.prototype.addMilliseconds = function (milliseconds) {
        return new DateTime(this.ms + milliseconds);
    };
    DateTime.prototype.addMinutes = function (minutes) {
        return new DateTime(this.ms + minutes * 60000);
    };
    DateTime.prototype.addMonths = function (months) {
        if(months < -120000 || months > 120000) {
            throw new RangeException();
        }
        var year = this.getDatePart(0);
        var month = this.getDatePart(2);
        var day = this.getDatePart(3);
        var num4 = month - 1 + months;
        if(num4 >= 0) {
            month = num4 % 12 + 1;
            year += num4 / 12;
        } else {
            month = 12 + (num4 + 1) % 12;
            year += (num4 - 11) / 12;
        }
        if(year < 1 || year > 9999) {
            throw new RangeException();
        }
        var num5 = DateTime.daysInMonth(year, month);
        if(day > num5) {
            day = num5;
        }
        return new DateTime(DateTime.dateToTicks(year, month, day) + this.ms % 86400000);
    };
    DateTime.prototype.addSeconds = function (seconds) {
        return new DateTime(this.ms + seconds * 1000);
    };
    DateTime.prototype.addTimespan = function (timeSpan) {
        return new DateTime(this.ms + timeSpan.getTotalMilliseconds());
    };
    DateTime.prototype.addYears = function (value) {
        if(value < -10000 || value > 10000) {
            throw new RangeException();
        }
        return this.addMonths(value * 12);
    };
    DateTime.prototype.compareTo = function (date) {
        var v = date.valueOf();
        return this.ms > v ? 1 : (this.ms < v ? -1 : 0);
    };
    DateTime.dateToTicks = function dateToTicks(year, month, day) {
        if(year >= 1 && year <= 9999 && month >= 1 && month <= 12) {
            var array = DateTime.isLeapYear(year) ? DateTime.daysToMonth366 : DateTime.daysToMonth365;
            if(day >= 1 && day <= array[month] - array[month - 1]) {
                var num = year - 1;
                return (num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 86400000;
            }
        }
        throw new RangeException();
    };
    DateTime.daysInMonth = function daysInMonth(year, month) {
        if(month < 1 || month > 12) {
            throw new RangeException();
        }
        var array = DateTime.isLeapYear(year) ? DateTime.daysToMonth366 : DateTime.daysToMonth365;
        return array[month] - array[month - 1];
    };
    DateTime.prototype.equals = function (date) {
        return date.valueOf() == this.ms;
    };
    DateTime.formatNumber2 = function formatNumber2(n) {
        return n < 10 ? "0" + n : n.toString();
    };
    DateTime.fromDate = function fromDate(year, month, day, hour, minute, second, millisecond) {
        return new DateTime(DateTime.dateToTicks(year, month, day) + hour * 3600000 + minute * 60000 + second * 1000 + millisecond);
    };
    DateTime.fromNativeDate = function fromNativeDate(date) {
        return new DateTime(date.valueOf() + 62135596800000 - date.getTimezoneOffset() * 60000);
    };
    DateTime.prototype.getDate = function () {
        return new DateTime(this.ms - Math.floor(this.ms % 86400000));
    };
    DateTime.prototype.getDatePart = function (part) {
        var i = Math.floor(this.ms / 86400000);
        var num = Math.floor(i / 146097);
        i -= num * 146097;
        var num2 = Math.floor(i / 36524);
        if(num2 == 4) {
            num2 = 3;
        }
        i -= num2 * 36524;
        var num3 = Math.floor(i / 1461);
        i -= num3 * 1461;
        var num4 = Math.floor(i / 365);
        if(num4 == 4) {
            num4 = 3;
        }
        if(part == 0) {
            return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
        }
        i -= num4 * 365;
        if(part == 1) {
            return i + 1;
        }
        var array = num4 == 3 && (num3 != 24 || num2 == 3) ? DateTime.daysToMonth366 : DateTime.daysToMonth365;
        var num5 = i >> 6;
        while(i >= array[num5]) {
            num5++;
        }
        if(part == 2) {
            return num5;
        }
        return i - array[num5 - 1] + 1;
    };
    DateTime.prototype.getDay = function () {
        return this.getDatePart(3);
    };
    DateTime.prototype.getDayOfWeek = function () {
        return DateTime.dayOfWeeks[(this.ms / 86400000 + 1) % 7];
    };
    DateTime.prototype.getDayOfYear = function () {
        return this.getDatePart(1);
    };
    DateTime.prototype.getFirstDayOfMonth = function () {
        return DateTime.fromDate(this.getYear(), this.getMonth(), 1);
    };
    DateTime.prototype.getFirstDayOfYear = function () {
        return DateTime.fromDate(this.getYear(), 1, 1);
    };
    DateTime.prototype.getHour = function () {
        return Math.floor(this.ms / 3600000) % 24;
    };
    DateTime.prototype.getLastDayOfMonth = function () {
        return this.getFirstDayOfMonth().addMonths(1).addDays(-1);
    };
    DateTime.prototype.getLastDayOfYear = function () {
        return DateTime.fromDate(this.getYear(), 12, 31);
    };
    DateTime.prototype.getMillisecond = function () {
        return Math.floor(this.ms / 1000);
    };
    DateTime.prototype.getMinute = function () {
        return Math.floor(this.ms / 60000) % 60;
    };
    DateTime.prototype.getMonth = function () {
        return this.getDatePart(2);
    };
    DateTime.prototype.getNextDay = function (dayOfWeek) {
        var v = Math.floor(this.getDayOfWeek() - dayOfWeek);
        return this.addDays(v >= 0 ? 7 - v : -v);
    };
    DateTime.getNow = function getNow() {
        return DateTime.fromNativeDate(new Date());
    };
    DateTime.prototype.getPeviousDay = function (dayOfWeek) {
        var v = Math.floor(dayOfWeek - this.getDayOfWeek());
        return this.addDays(v >= 0 ? v - 7 : v);
    };
    DateTime.prototype.getSecond = function () {
        return Math.floor(this.ms / 1000) % 60;
    };
    DateTime.prototype.getYear = function () {
        return this.getDatePart(0);
    };
    DateTime.isLeapYear = function isLeapYear(year) {
        if(year < 1 || year > 9999) {
            throw new RangeException();
        }
        return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
    };
    DateTime.prototype.toString = function () {
        var M = DateTime.formatNumber2(this.getMonth());
        var d = DateTime.formatNumber2(this.getDay());
        var h = DateTime.formatNumber2(this.getHour());
        var m = DateTime.formatNumber2(this.getMinute());
        var s = DateTime.formatNumber2(this.getSecond());
        return this.getYear() + "/" + M + "/" + d + " " + h + ":" + m + ":" + s;
    };
    DateTime.prototype.valueOf = function () {
        return this.ms;
    };
    return DateTime;
})();
//@ sourceMappingURL=dateTime.js.map
