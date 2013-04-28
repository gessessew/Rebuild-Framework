/// <reference path="../Rebuild.TypeScript/rebuild.ts" />
/// <reference path="Scripts/typings/jasmine/jasmine.d.ts" />

var TimeSpan = rb.TimeSpan;

describe("timeSpan", function () {
    it("from 1000 ms = 1000 ms", () =>
        expect(TimeSpan.from(0, 0, 0, 0, 1000).valueOf()).toBe(1000)
    );
    it("from 1 sec = 1000 ms", () =>
        expect(TimeSpan.from(0, 0, 0, 1).valueOf()).toBe(1000)
    );
    it("from 1 minute = 60 * 1000 ms", () =>
        expect(TimeSpan.from(0, 0, 1).valueOf()).toBe(60 * 1000)
    );
    it("from 1 hour = 60 * 60 * 1000 ms", () =>
        expect(TimeSpan.from(0, 1).valueOf()).toBe(60 * 60 * 1000)
    );
    it("from 1 day = 24 * 60 * 60 * 1000 ms", () =>
        expect(TimeSpan.from(1).valueOf()).toBe(24 * 60 * 60 * 1000)
    );
    it("from 1 minute add 2 minute = 3 * 60 * 1000 ms", () =>
        expect(TimeSpan.from(0, 0, 1).add(0, 0, 2).valueOf()).toBe(3 * 60 * 1000)
    );
});