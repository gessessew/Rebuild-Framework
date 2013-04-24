/// <reference path="../../Rebuild/Scripts/guid.ts" />
/// <reference path="typings/jasmine/jasmine.d.ts" />

describe("guid", function () {
    it("newGuid format is correct", () =>
        expect(Guid.regex().test(Guid.newGuid().valueOf())).toBeTruthy()
    );
    it("empty format is correct", () =>
        expect(Guid.regex().test(Guid.empty().valueOf())).toBeTruthy()
    );
    it("toString with 'X' parameter format is correct", () =>
        expect(Guid.regex('x').test(Guid.empty().toString('x'))).toBeTruthy()
    );
});