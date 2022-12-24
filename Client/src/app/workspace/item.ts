import { ItemSaverService } from "./item-saver.service";

export class Item {
    private _id : string | null = null!;
    private _name : string = null!;
    private _number : number = null!;

    public SaveChanges(saver: ItemSaverService) : void {

    }

    public get Name() : string { return this._name; }
    public set Name(v : string) { this._name = v; }

    public get Number() : number { return this._number; }
    public set Number(v : number) { this._number = v; }
    
    /**
     * Typical stored item.
     */
    public constructor(name : string, number : number = 0, id : string | null = null) {
        this._id = id;
        this._name = name;
        this._number = number;
    }
}
