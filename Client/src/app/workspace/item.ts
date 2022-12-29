import { ItemSaverService } from "./item-saver.service";

export class Item {
    private itemId : number;
    private name : string;
    private number : number;

    public get ItemId() : number { return this.itemId; }
    public set ItemId(v : number) { this.itemId = v; }
    public get Name() : string { return this.name; }
    public set Name(v : string) { this.name = v; }
    public get Number() : number { return this.number; }
    public set Number(v : number) { this.number = v; }

    public SaveChanges(saver: ItemSaverService) : void {

    }
    
    /**
     * Typical stored item.
     */
    public constructor(name : string, number : number = 0, id : number = 0) {
        this.itemId = id;
        this.name = name;
        this.number = number;
    }
}
