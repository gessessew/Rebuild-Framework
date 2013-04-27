var TimeSpan = (function () {
    function TimeSpan(milliseconds) {
        this.ms = milliseconds | 0;
    }
    TimeSpan.prototype.add = function (days, hours, minutes, seconds, milliseconds) {
        return new TimeSpan(this.ms + TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    };
    TimeSpan.prototype.addSpan = function (ts) {
        return new TimeSpan(this.ms + ts.ms);
    };
    TimeSpan.prototype.duration = function () {
        return this.ms >= 0 ? this : new TimeSpan(-this.ms);
    };
    TimeSpan.from = function from(days, hours, minutes, seconds, milliseconds) {
        return new TimeSpan(TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    };
    TimeSpan.prototype.getDays = function () {
        return this.ms / 86400000;
    };
    TimeSpan.prototype.getHours = function () {
        return Math.floor(this.ms / 3600000) % 24;
    };
    TimeSpan.prototype.getMilliseconds = function () {
        return Math.floor(this.ms / 1000);
    };
    TimeSpan.prototype.getMinutes = function () {
        return Math.floor(this.ms / 60000) % 60;
    };
    TimeSpan.prototype.getSeconds = function () {
        return Math.floor(this.ms / 1000) % 60;
    };
    TimeSpan.prototype.getTotalDays = function () {
        return this.ms * 1.1574074074074074E-8;
    };
    TimeSpan.prototype.getTotalHours = function () {
        return this.ms * 2.7777777777777777E-7;
    };
    TimeSpan.prototype.getTotalMilliseconds = function () {
        return this.ms;
    };
    TimeSpan.prototype.getTotalMinutes = function () {
        return this.ms * 1.6666666666666667E-5;
    };
    TimeSpan.prototype.getTotalSeconds = function () {
        return this.ms * 1E-3;
    };
    TimeSpan.interval = function interval(value, scale) {
        return new TimeSpan(value * scale + (value >= 0.0 ? 0.5 : -0.5));
    };
    TimeSpan.prototype.negate = function () {
        return new TimeSpan(-this.ms);
    };
    TimeSpan.prototype.subtract = function (ts) {
        return new TimeSpan(this.ms - ts.ms);
    };
    TimeSpan.timeToMs = function timeToMs(days, hours, minutes, seconds, milliseconds) {
        return ((days | 0) * 3600 * 24 + (hours | 0) * 3600 + (minutes | 0) * 60 + (seconds | 0)) * 1000 + (milliseconds | 0);
    };
    TimeSpan.prototype.valueOf = function () {
        return this.ms;
    };
    return TimeSpan;
})();
