import { ItemCategory } from "./itemCategory";

export class Item {
    id: number;
    name: string; 
    maxDurability: number;
    price: number; 
    weight: number;
    secretItemDescription: string; 
    itemDescription: string;
    itemCategoryId: number;
    itemCategory: ItemCategory;

    constructor(
        id: number,
        name: string, 
        maxDurability: number,
        price: number,
        weight: number,
        secretItemDescription: string, 
        itemDescription: string,
        itemCategoryId: number,
        itemCategory: ItemCategory
        ) {
        this.id = id;
        this.name = name; 
        this.maxDurability = maxDurability;
        this.price = price
        this.weight = weight
        this.secretItemDescription = secretItemDescription 
        this.itemDescription = itemDescription;
        this.itemCategoryId = itemCategoryId
        this.itemCategory = itemCategory
    }
}
