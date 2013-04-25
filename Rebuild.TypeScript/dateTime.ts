class DateTime
{
    private date: Date;

    constructor(private ms: number) {
    }

    addDays(days: number) {
        return new DateTime(this.ms + days * 86400000);
    }

    addHours(hours: number) {
        return new DateTime(this.ms + hours * 3600000);
    }

    addMinutes(minutes: number) {
        return new DateTime(this.ms + minutes * 60000);
    }

    addSeconds(seconds: number) {
        return new DateTime(this.ms + seconds * 1000);
    }

    private ensureDate() {
        if (!this.date)
            this.date = new Date(this.ms);

        return this.date || (this.date = new Date(this.ms));
    }

    getDate() {
        return new DateTime(Math.floor(this.ms / 86400000) * 86400000);
    }

    getHours() {
        return this.ms % 3600000;
    }

    getMinutes() {
        return this.ms % 60000;
    }

    getSeconds() {
        return this.ms % 1000;
    }

    valueOf() {
        return this.ms;
    }
}