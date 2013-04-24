var TimeSpan = (function () {
    function TimeSpan(milliseconds) {
        this._ms = milliseconds | 0;
    }
    TimeSpan.prototype.add = function (days, hours, minutes, seconds, milliseconds) {
        return new TimeSpan(this._ms + TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    };
    TimeSpan.prototype.addSpan = function (ts) {
        return new TimeSpan(this._ms + ts._ms);
    };
    TimeSpan.prototype.days = function () {
        return this._ms / 86400000;
    };
    TimeSpan.prototype.duration = function () {
        return this._ms >= 0 ? this : new TimeSpan(-this._ms);
    };
    TimeSpan.from = function from(days, hours, minutes, seconds, milliseconds) {
        return new TimeSpan(TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    };
    TimeSpan.prototype.hours = function () {
        return this._ms / 3600000 % 24;
    };
    TimeSpan.interval = function interval(value, scale) {
        return new TimeSpan(value * scale + (value >= 0.0 ? 0.5 : -0.5));
    };
    TimeSpan.prototype.milliseconds = function () {
        return this._ms % 1000;
    };
    TimeSpan.prototype.minutes = function () {
        return this._ms / 60000 % 60;
    };
    TimeSpan.prototype.negate = function () {
        return new TimeSpan(-this._ms);
    };
    TimeSpan.prototype.seconds = function () {
        return this._ms / 1000 % 60;
    };
    TimeSpan.prototype.subtract = function (ts) {
        return new TimeSpan(this._ms - ts._ms);
    };
    TimeSpan.timeToMs = function timeToMs(days, hours, minutes, seconds, milliseconds) {
        return ((days | 0) * 3600 * 24 + (hours | 0) * 3600 + (minutes | 0) * 60 + (seconds | 0)) * 1000 + (milliseconds | 0);
    };
    TimeSpan.prototype.totalDays = function () {
        return this._ms * 1.1574074074074074E-8;
    };
    TimeSpan.prototype.totalHours = function () {
        return this._ms * 2.7777777777777777E-7;
    };
    TimeSpan.prototype.totalMilliseconds = function () {
        return this._ms;
    };
    TimeSpan.prototype.totalMinutes = function () {
        return this._ms * 1.6666666666666667E-5;
    };
    TimeSpan.prototype.totalSeconds = function () {
        return this._ms * 1E-3;
    };
    TimeSpan.prototype.valueOf = function () {
        return this._ms;
    };
    return TimeSpan;
})();
