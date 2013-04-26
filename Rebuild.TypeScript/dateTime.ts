/// <reference path="../Rebuild.TypeScript.Test/timeSpan.ts" />

enum DayOfWeek {
    sunday,
    monday,
    tuesday,
    wednesday,
    thursday,
    friday,
    saturday
}

class DateTime
{
    private static dayOfWeeks = [DayOfWeek.sunday,DayOfWeek.monday,DayOfWeek.tuesday,DayOfWeek.wednesday,DayOfWeek.thursday,DayOfWeek.friday,DayOfWeek.saturday];
    private static daysToMonth365 = [0,31,59,90,120,151,181,212,243,273,304,334,365];
    private static daysToMonth366 = [0,31,60,91,121,152,182,213,244,274,305,335,366];

    constructor(private ms: number) {
    }

    addDays(days: number) {
        return new DateTime(this.ms + days * 86400000);
    }

    addHours(hours: number) {
        return new DateTime(this.ms + hours * 3600000);
    }

    addMilliseconds(milliseconds: number) {
        return new DateTime(this.ms + milliseconds);
    }

    addMinutes(minutes: number) {
        return new DateTime(this.ms + minutes * 60000);
    }

    addMonths(months: number) {
        if (months < -120000 || months > 120000)
            throw new RangeException();

        var year = this.getDatePart(0);
        var month = this.getDatePart(2);
        var day = this.getDatePart(3);
        var num4 = month - 1 + months;

        if (num4 >= 0) {
            month = num4 % 12 + 1;
            year += num4 / 12;
        }
        else {
            month = 12 + (num4 + 1) % 12;
            year += (num4 - 11) / 12;
        }

        if (year < 1 || year > 9999)
            throw new RangeException();

        var num5 = DateTime.daysInMonth(year, month);
        if (day > num5)
            day = num5;

        return new DateTime(DateTime.dateToTicks(year, month, day) + this.ms % 86400000);
    }
    addSeconds(seconds: number) {
        return new DateTime(this.ms + seconds * 1000);
    }

    addTimespan(timeSpan: TimeSpan) {
        return new DateTime(this.ms + timeSpan.getTotalMilliseconds());
    }

    addYears(value: number) {
        if (value < -10000 || value > 10000) {
            throw new RangeException();
        }
        return this.addMonths(value * 12);
    }

    compareTo(date: DateTime) {
        var v = date.valueOf();
        return this.ms > v ? 1 : (this.ms < v ? -1 : 0);
    }

    static dateToTicks(year: number, month: number, day: number) {
        if (year >= 1 && year <= 9999 && month >= 1 && month <= 12) {
            var array = DateTime.isLeapYear(year) ? DateTime.daysToMonth366 : DateTime.daysToMonth365;

            if (day >= 1 && day <= array[month] - array[month - 1]) {
                var num = year - 1;
                return (num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 86400000;
            }
        }
        throw new RangeException();
    }

    static daysInMonth(year: number, month: number) {
        if (month < 1 || month > 12)
            throw new RangeException();

        var array = DateTime.isLeapYear(year) ? DateTime.daysToMonth366 : DateTime.daysToMonth365;
        return array[month] - array[month - 1];
    }

    equals(date: DateTime) {
        return date.valueOf() == this.ms;
    }

    private static formatNumber2(n: number) {
        return n < 10 ? "0" + n : n.toString();
    }

    static fromDate(year: number, month: number, day: number, hour?: number, minute?: number, second?: number, millisecond?: number) {
        return new DateTime(DateTime.dateToTicks(year, month, day) + hour * 3600000 + minute * 60000 + second * 1000 + millisecond);
    }

    static fromNativeDate(date: Date) {
        return new DateTime(date.valueOf() + 62135596800000 - date.getTimezoneOffset() * 60000);
    }

    getDate() {
        return new DateTime(this.ms - Math.floor(this.ms % 86400000));
    }

    private getDatePart(part: number) {
        var i = Math.floor(this.ms / 86400000);
        var num = Math.floor(i / 146097);
        i -= num * 146097;
        var num2 = Math.floor(i / 36524);

        if (num2 == 4)
            num2 = 3;

        i -= num2 * 36524;
        var num3 = Math.floor(i / 1461);
        i -= num3 * 1461;
        var num4 = Math.floor(i / 365);

        if (num4 == 4)
            num4 = 3;

        if (part == 0)
            return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;

        i -= num4 * 365;
        if (part == 1)
            return i + 1;

        var array = num4 == 3 && (num3 != 24 || num2 == 3) ? DateTime.daysToMonth366 : DateTime.daysToMonth365;
        var num5 = i >> 6;
        while (i >= array[num5]) {
            num5++;
        }

        if (part == 2)
            return num5;

        return i - array[num5 - 1] + 1;
    }

    getDay() {
        return this.getDatePart(3);
    }

    getDayOfWeek() {
        return DateTime.dayOfWeeks[(this.ms / 86400000 + 1) % 7];
    }

    getDayOfYear() {
        return this.getDatePart(1);
    }

    getFirstDayOfMonth() {
        return DateTime.fromDate(this.getYear(), this.getMonth(), 1);
    }

    getFirstDayOfYear() {
        return DateTime.fromDate(this.getYear(), 1, 1);
    }

    getHour() {
        return Math.floor(this.ms / 3600000) % 24;
    }

    getLastDayOfMonth() {
        return this.getFirstDayOfMonth().addMonths(1).addDays(-1);
    }

    getLastDayOfYear() {
        return DateTime.fromDate(this.getYear(), 12, 31);
    }

    getMillisecond() {
        return Math.floor(this.ms / 1000);
    }

    getMinute() {
        return Math.floor(this.ms / 60000) % 60;
    }
    
    getMonth() {
        return this.getDatePart(2);
    }

    getNextDay(dayOfWeek: DayOfWeek) {
        var v = Math.floor(this.getDayOfWeek() - dayOfWeek);
        return this.addDays(v >= 0 ? 7 - v : -v);
    }

    static getNow() {
        return DateTime.fromNativeDate(new Date());
    }

    getPeviousDay(dayOfWeek: DayOfWeek) {
        var v = Math.floor(dayOfWeek - this.getDayOfWeek());
        return this.addDays(v >= 0 ? v - 7 : v);
    }

    getSecond() {
        return Math.floor(this.ms / 1000) % 60;
    }

    getYear() {
        return this.getDatePart(0);
    }

    static isLeapYear(year: number) {
        if (year < 1 || year > 9999)
            throw new RangeException();
            
        return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
    }

    toString() {
        var M = DateTime.formatNumber2(this.getMonth());
        var d = DateTime.formatNumber2(this.getDay());
        var h = DateTime.formatNumber2(this.getHour());
        var m = DateTime.formatNumber2(this.getMinute());
        var s = DateTime.formatNumber2(this.getSecond());
        return this.getYear() + "/" + M + "/" + d + " " + h + ":" + m + ":" + s;
    }

    valueOf() {
        return this.ms;
    }
}