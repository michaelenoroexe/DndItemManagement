import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ItemCategory } from "../model/itemCategory";
import { environment } from "src/environment";
import { CharacterItem } from "../model/characterItem";
import * as signalR from "@microsoft/signalr";
import { CharacterItemHubService } from "./characterItemHub.service";

@Injectable({
    providedIn: "root"
})
export class CharacterItemService {
    private characterItems:{[chId:number]:CharacterItem[]} = {};

    public ListenAddingChEvent(call:(chItem:CharacterItem) => void) {
        this.hub.addchEvent.push(call);
    }
    private ListenChangingChEvent(call:(chItem:CharacterItem) => void) {
        this.hub.changechEvent.push(call);
    }
    public ListenDeleteChEvent(call:(chItem:{characterId:number, itemId:number}) => void) {
        this.hub.deletechEvent.push(call);
    }

    constructor(private http:HttpClient, private hub: CharacterItemHubService) { 
        this.ListenAddingChEvent((chItem) => {this.characterItems[chItem.characterId].push(chItem)});
        this.ListenChangingChEvent((chItem) => {this.characterItems[chItem.characterId].push(chItem)});
        this.ListenAddingChEvent((chItem) => {this.characterItems[chItem.characterId].push(chItem)});
    }
    public StartWatch() { this.hub.StartWatch(); }
    public StopWatch() { this.hub.StopWatch(); }

    public GetCharacterItems(roomId:number, characterId:number) {
        let chItems = this.characterItems[characterId]
        if (chItems != undefined) return chItems
        const chItemsRequest:CharacterItem[] = []
        this.characterItems[characterId] = chItemsRequest;
        chItems = chItemsRequest;
        this.http.get<CharacterItem[]>(`${environment.apiURL}rooms/${roomId}/character/${characterId}/chItems`)
        .subscribe({
            next(value:CharacterItem[]) {
                value.forEach(ci => chItemsRequest.push(ci));
            }
        })
        return chItems;
    }
    public AddChItem(chItem:CharacterItem) {
        this.hub.AddCharacterItem(chItem);
    }
    public ChangeChItem(chItem:CharacterItem) {
        this.hub.ChangeCharacterItem(chItem);
    }
    public DeleteChItem(chItem:CharacterItem) {
        this.hub.DeleteCharacterItem(chItem);
    }
}
