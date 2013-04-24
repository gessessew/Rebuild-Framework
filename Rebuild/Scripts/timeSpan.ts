class TimeSpan {
    private _ms: number;

    constructor(milliseconds?: number) {
        this._ms = milliseconds | 0;
    }
    add(days?: number, hours?: number, minutes?: number, seconds?: number, milliseconds?: number) {
        return new TimeSpan(this._ms + TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    }
    addSpan(ts: TimeSpan) {
        return new TimeSpan(this._ms + ts._ms);
    }
    days() {
        return this._ms / 86400000;
    }
    duration() {
        return this._ms >= 0 ? this : new TimeSpan(-this._ms);
    }
    static from(days?: number, hours?: number, minutes?: number, seconds?: number, milliseconds?: number) {
        return new TimeSpan(TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    }
    hours() {
        return this._ms / 3600000 % 24;
    }
    private static interval(value: number, scale: number) {
        return new TimeSpan(value * scale + (value >= 0.0 ? 0.5 : -0.5));
    }
    milliseconds() {
        return this._ms % 1000;
    }
    minutes() {
        return this._ms / 60000 % 60;
    }
    negate() {
        return new TimeSpan(-this._ms);
    }
    seconds() {
        return this._ms / 1000 % 60;
    }
    subtract(ts: TimeSpan) {
        return new TimeSpan(this._ms - ts._ms);
    }
    private static timeToMs(days: number, hours: number, minutes: number, seconds: number, milliseconds: number) {
        return ((days | 0) * 3600 * 24 + (hours | 0) * 3600 + (minutes | 0) * 60 + (seconds | 0)) * 1000 + (milliseconds | 0);
    }
    totalDays() {
        return this._ms * 1.1574074074074074E-8;
    }
    totalHours() {
        return this._ms * 2.7777777777777777E-7;
    }
    totalMilliseconds() {
        return this._ms;
    }
    totalMinutes() {
        return this._ms * 1.6666666666666667E-5;
    }
    totalSeconds() {
        return this._ms * 1E-3;
    }
    valueOf() {
        return this._ms;
    }
}