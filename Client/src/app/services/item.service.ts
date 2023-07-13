import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ItemCategory } from "../model/itemCategory";
import { environment } from "src/environment";
import { Item } from "../model/item";
import { CharacterItemHubService } from "./characterItemHub.service";
import { firstValueFrom } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class ItemService {

    public itemCategories:ItemCategory[] = []
    public items:Item[][] = []

    private ListenAddingItemEvent(call:(item:Item) => void) {
        this.hub.addItemEvent.push(call);
    }
    private ListenChangingItemEvent(call:(item:Item) => void) {
        this.hub.changeItemEvent.push(call);
    }
    private ListenDeleteItemEvent(call:(itemId:number) => void) {
        this.hub.deleteItemEvent.push(call);
    }

    constructor(private http:HttpClient, private hub:CharacterItemHubService) {
        this.Initialize();
        this.ListenAddingItemEvent((item:Item) => {
            if (this.items[item.itemCategoryId] == undefined) this.items[item.itemCategoryId] = [];
            this.items[item.itemCategoryId].push(item)
        });
        this.ListenChangingItemEvent((item:Item) => {
            const oldItem = this.items[item.itemCategoryId].find(it => it.id == item.id)!;
            oldItem.name = item.name;
            oldItem.maxDurability = item.maxDurability;
            oldItem.price = item.price;
            oldItem.weight = item.weight;
            oldItem.secretItemDescription = item.secretItemDescription;
            oldItem.itemDescription = item.itemDescription;
        });
        this.ListenDeleteItemEvent((itemId:number) => {
            let itInd: number = -1;
            let categIndex = 0;
            for (let index = 0; index < this.items.length; index++) {
                itInd = this.items[index].findIndex(it => it.id == itemId);
                if (itInd != -1) {
                    categIndex = index;
                    break;
                }
            }
            this.items[categIndex].splice(itInd, 1);
        })
    };
    public StartWatch() { this.hub.StartWatch(); }
    public StopWatch() { this.hub.StopWatch(); }
    public Initialize() {
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
                    if (th.items[i.itemCategoryId] == undefined) th.items[i.itemCategoryId] = [];
                    th.items[i.itemCategoryId].push(i);
                });
            }
        })
    }
    public async GetCategories() {
        return firstValueFrom(this.http.get<ItemCategory[]>(`${environment.apiURL}itemcategories`));
    }
    public AddItem(roomId:number, item:Item) {
        const fullItem = item as any;
        const token = localStorage.getItem("Token")!;
        fullItem.roomId = roomId;
        this.http.post(`${environment.apiURL}rooms/${roomId}/items`, 
            fullItem, {headers: {"Authorization": "Bearer " + token}}).subscribe({next(ar) {}});
    }
    public FullChangeItem(roomId:number, item:Item) {
        const fullItem = item as any;
        const token = localStorage.getItem("Token")!;
        fullItem.roomId = roomId;
        this.http.put(`${environment.apiURL}rooms/${roomId}/items/${item.id}`, 
            fullItem, {headers: {"Authorization": "Bearer " + token}}).subscribe({next(ar) {}});
    }
    public DeleteItem(roomId:number, item:Item) {
        const token = localStorage.getItem("Token")!;
        this.http.delete(`${environment.apiURL}rooms/${roomId}/items/${item.id}`,
            {headers: {"Authorization": "Bearer " + token}}).subscribe({next(ar) {}});
    }
}
