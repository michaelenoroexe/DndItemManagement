export class Room {
    public id: number;
    public name: string;
    public started: boolean;

    public constructor (id:number, name:string, started:boolean) {
        this.id = id;
        this.name = name;
        this.started = started;
    }

    public Clone():Room {
        return new Room(this.id, this.name, this.started);
    }
}
export class RoomWithDm extends Room {

    public dmName : string;

    public constructor (id:number, name:string, started:boolean, dmName:string) {
        super(id, name, started);
        this.dmName = dmName;
    }
}
