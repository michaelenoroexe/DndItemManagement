export class CharacterItem {
    caracterId:number;
    itemId:number;
    number:number;
    currentDurability:number;

    constructor(
        characterId:number, 
        itemId:number, 
        number:number, 
        currentDurability:number) {
        this.caracterId = characterId;
        this.itemId = itemId;
        this.number = number;        this.currentDurability = currentDurability;
    }
}
