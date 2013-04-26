class TimeSpan {
    private ms: number;

    constructor(milliseconds?: number) {
        this.ms = milliseconds | 0;
    }

    add(days?: number, hours?: number, minutes?: number, seconds?: number, milliseconds?: number) {
        return new TimeSpan(this.ms + TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    }

    addSpan(ts: TimeSpan) {
        return new TimeSpan(this.ms + ts.ms);
    }

    duration() {
        return this.ms >= 0 ? this : new TimeSpan(-this.ms);
    }

    static from(days?: number, hours?: number, minutes?: number, seconds?: number, milliseconds?: number) {
        return new TimeSpan(TimeSpan.timeToMs(days, hours, minutes, seconds, milliseconds));
    }

    getDays() {
        return this.ms / 86400000;
    }

    getHours() {
        return Math.floor(this.ms / 3600000) % 24;
    }

    getMilliseconds() {
        return Math.floor(this.ms / 1000);
    }

    getMinutes() {
        return Math.floor(this.ms / 60000) % 60;
    }

    getSeconds() {
        return Math.floor(this.ms / 1000) % 60;
    }

    getTotalDays() {
        return this.ms * 1.1574074074074074E-8;
    }

    getTotalHours() {
        return this.ms * 2.7777777777777777E-7;
    }

    getTotalMilliseconds() {
        return this.ms;
    }

    getTotalMinutes() {
        return this.ms * 1.6666666666666667E-5;
    }

    getTotalSeconds() {
        return this.ms * 1E-3;
    }

    private static interval(value: number, scale: number) {
        return new TimeSpan(value * scale + (value >= 0.0 ? 0.5 : -0.5));
    }

    negate() {
        return new TimeSpan(-this.ms);
    }

    subtract(ts: TimeSpan) {
        return new TimeSpan(this.ms - ts.ms);
    }

    private static timeToMs(days: number, hours: number, minutes: number, seconds: number, milliseconds: number) {
        return ((days | 0) * 3600 * 24 + (hours | 0) * 3600 + (minutes | 0) * 60 + (seconds | 0)) * 1000 + (milliseconds | 0);
    }

    valueOf() {
        return this.ms;
    }
}