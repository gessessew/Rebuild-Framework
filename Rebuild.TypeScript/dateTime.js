var DateTime = (function () {
    function DateTime(ms) {
        this.ms = ms;
    }
    DateTime.prototype.addDays = function (days) {
        return new DateTime(this.ms + days * 86400000);
    };
    DateTime.prototype.addHours = function (hours) {
        return new DateTime(this.ms + hours * 3600000);
    };
    DateTime.prototype.addMinutes = function (minutes) {
        return new DateTime(this.ms + minutes * 60000);
    };
    DateTime.prototype.addSeconds = function (seconds) {
        return new DateTime(this.ms + seconds * 1000);
    };
    DateTime.prototype.ensureDate = function () {
        if(!this.date) {
            this.date = new Date(this.ms);
        }
        return this.date || (this.date = new Date(this.ms));
    };
    DateTime.prototype.getDate = function () {
        return new DateTime(Math.floor(this.ms / 86400000) * 86400000);
    };
    DateTime.prototype.getHours = function () {
        return this.ms % 3600000;
    };
    DateTime.prototype.getMinutes = function () {
        return this.ms % 60000;
    };
    DateTime.prototype.getSeconds = function () {
        return this.ms % 1000;
    };
    DateTime.prototype.valueOf = function () {
        return this.ms;
    };
    return DateTime;
})();
