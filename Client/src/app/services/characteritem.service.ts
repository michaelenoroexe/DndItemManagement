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
    public ListenChangingChEvent(call:(chItem:CharacterItem) => void) {
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
        const token = localStorage.getItem("Token")!;
        this.characterItems[characterId] = chItemsRequest;
        chItems = chItemsRequest;
        this.http.get<CharacterItem[]>(`${environment.apiURL}rooms/${roomId}/character/${characterId}/chItems`,
            {headers: {"Authorization": "Bearer " + token}}).subscribe({
            next(value:CharacterItem[]) {
                value.forEach(ci => chItemsRequest.push(ci));
            }
        })
        return chItems;
    }
    public AddChItem(roomId:number, chItem:CharacterItem) {
        const token = localStorage.getItem("Token")!;
        this.http.post(`${environment.apiURL}rooms/${roomId}/character/${chItem.characterId}/chItems/${chItem.itemId}`, 
            chItem, {headers: {"Authorization": "Bearer " + token}}).subscribe({next(val) {}});
    }
    public FullChangeChItem(roomId:number, chItem:CharacterItem) {
        const token = localStorage.getItem("Token")!;
        this.http.put(`${environment.apiURL}rooms/${roomId}/character/${chItem.characterId}/chItems/${chItem.itemId}`, 
            chItem, {headers: {"Authorization": "Bearer " + token}}).subscribe({next(val) {}});;
    }
    public PartialChangeChItem(roomId:number, chItem:any) {
        const token = localStorage.getItem("Token")!;
        this.http.patch(`${environment.apiURL}rooms/${roomId}/character/${chItem.characterId}/chItems/${chItem.itemId}`, 
            chItem, {headers: {"Authorization": "Bearer " + token}}).subscribe({next(val) {}});;
    }
    public DeleteChItem(roomId:number, chItem:CharacterItem) {
        const token = localStorage.getItem("Token")!;
        this.http.delete(`${environment.apiURL}rooms/${roomId}/character/${chItem.characterId}/chItems/${chItem.itemId}`,
            {headers: {"Authorization": "Bearer " + token}}).subscribe({next(val) {}});;
    }
}
