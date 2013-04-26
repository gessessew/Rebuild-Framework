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
});
