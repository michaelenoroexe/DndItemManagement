export class Room {
    public id : number;
    public name : string;

    public constructor (id:number, name:string) {
        this.id = id;
        this.name = name;
    }
}
export class RoomWithDm extends Room {

    public dmName : string;

    public constructor (id:number, name:string, dmName:string) {
        super(id, name);
        this.dmName = dmName;
    }
}
