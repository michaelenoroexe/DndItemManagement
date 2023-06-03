import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ItemCategory } from "../model/itemCategory";
import { environment } from "src/environment";
import { Item } from "../model/item";

@Injectable({
    providedIn: "root"
})
export class ItemService {

    public itemCategories:ItemCategory[] = []
    public items:Item[] = []

    constructor(private http:HttpClient) {
        const th = this;
        this.http.get<ItemCategory[]>(`${environment.apiURL}itemcategories`).subscribe({
            next(value:ItemCategory[]) {
                value.forEach(ic => th.itemCategories.push(ic));
            }
        })
        this.http.get<Item[]>(`${environment.apiURL}items`).subscribe({
            next(value:Item[]) {
                value.forEach(i => {
                    i.itemCategory = th.itemCategories.find(ic => ic.id == i.itemCategoryId)!;
                    th.items.push(i);
                });
            }
        })
    }

    GetItemCategories() {
        const itemCateg: ItemCategory[] = []

        return itemCateg;
    }
}
