export class CharacterItem {
    characterId:number;
    itemId:number;
    itemNumber:number;
    currentDurability:number;

    constructor(
        characterId:number, 
        itemId:number, 
        number:number, 
        currentDurability:number) {
        this.characterId = characterId;
        this.itemId = itemId;
        this.itemNumber = number;        
        this.currentDurability = currentDurability;
    }
}
