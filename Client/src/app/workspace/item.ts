import { ItemSaverService } from "./item-saver.service";

export class Item {

    public ItemId : number;
    public Name : string;
    public Number : number;


    public SaveChanges(saver: ItemSaverService) : void {

    }
    
    /**
     * Typical stored item.
     */
    public constructor(name : string, number : number = 0, id : number = 0) {
        this.ItemId = id;
        this.Name = name;
        this.Number = number;
    }
}
