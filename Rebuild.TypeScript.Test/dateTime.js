describe("dateTime", function () {
    it("fromNativeDate", function () {
        for(var y = 1970; y < 2013; y++) {
            var d = DateTime.fromNativeDate(new Date(y, 0, 1));
            expect(d.getYear()).toBe(y);
            expect(d.getMonth()).toBe(1);
            expect(d.getDay()).toBe(1);
            expect(d.getHour()).toBe(0);
        }
    });
    it("weekOfYear", function () {
        expect(DateTime.fromDate(2013, 1, 1).weekOfYear()).toBe(1);
        expect(DateTime.fromDate(2013, 1, 1).weekOfYear(CalendarWeekRule.firstFullWeek)).toBe(53);
        expect(DateTime.fromDate(2013, 1, 8).weekOfYear(CalendarWeekRule.firstFullWeek)).toBe(1);
        expect(DateTime.fromDate(2013, 1, 8).weekOfYear()).toBe(2);
        expect(DateTime.fromDate(2012, 12, 31).weekOfYear()).toBe(53);
        expect(DateTime.fromDate(2012, 12, 31).weekOfYear(CalendarWeekRule.firstFullWeek)).toBe(53);
    });
});
