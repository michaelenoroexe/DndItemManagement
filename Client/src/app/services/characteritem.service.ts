import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ItemCategory } from "../model/itemCategory";
import { environment } from "src/environment";
import { CharacterItem } from "../model/characterItem";
import * as signalR from "@microsoft/signalr";

@Injectable({
    providedIn: "root"
})
export class CharacterItemService {
    private wached: boolean = false;
    private itemHub: signalR.HubConnection;
    private characterItems:{[chId:number]:CharacterItem[]} = {};

    private addchEvent:((chItem:CharacterItem) => void)[] = []
    public ListenAddingChEvent(call:(chItem:CharacterItem) => void) {
        this.addchEvent.push(call);
    }
    private changechEvent:((chItem:CharacterItem) => void)[] = []
    public ListenChangingChEvent(call:(chItem:CharacterItem) => void) {
        this.changechEvent.push(call);
    }
    private deletechEvent:((chItemId:number) => void)[] = []
    public ListenDeleteChEvent(call:(chItemId:number) => void) {
        this.deletechEvent.push(call);
    }

    constructor(private http:HttpClient) {
        const token = localStorage.getItem("Token")!;
        this.itemHub = new signalR.HubConnectionBuilder()
                            .withUrl(environment.apiURL + 'hubs/ItemHub', 
                            {accessTokenFactory: () => token})
                            .build();
        this.ConfigureHub();
    }
    public StartWatchRoomList() {
        if (!this.wached) {
            this.itemHub.start();
            this.wached = true
        }
    }
    public StopWatchRoomList() {
        if (this.wached) {
            this.itemHub.stop();
            this.wached = false;
        }
    }
    public GetCharacterItems(roomId:number, characterId:number) {
        const chItems = this.characterItems[characterId]
        if (chItems != undefined) return chItems
        const chItemsRequest:CharacterItem[] = []
        this.characterItems[characterId] = chItemsRequest;
        this.http.get<CharacterItem[]>(`${environment.apiURL}rooms/${roomId}/character/${characterId}/chItems`)
        .subscribe({
            next(value:CharacterItem[]) {
                value.forEach(ci => chItemsRequest.push(ci));
            }
        })
        return chItems;
    }

    private ConfigureHub() {
        this.itemHub.on("AddedCharacterItemInfo", (chitem:CharacterItem) => {
            this.addchEvent.forEach(ev => ev(chitem));
        });
        this.itemHub.on("ChangeCharacterItemInfo", (chitem:CharacterItem) => {  
            this.changechEvent.forEach(e => e(chitem))
        });
        this.itemHub.on("DeleteCharacterItemInfo", (chitemId:number) => {
            this.deletechEvent.forEach(e => e(chitemId))
        });
    }
}
