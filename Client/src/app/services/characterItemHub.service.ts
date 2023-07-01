import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ItemCategory } from "../model/itemCategory";
import { environment } from "src/environment";
import { CharacterItem } from "../model/characterItem";
import * as signalR from "@microsoft/signalr";
import { Item } from "../model/item";

@Injectable({
    providedIn: "root"
})
export class CharacterItemHubService {
    private wached: boolean = false;
    private itemHub: signalR.HubConnection;

    public addchEvent: ((chItem: CharacterItem) => void)[] = []
    public changechEvent: ((chItem: CharacterItem) => void)[] = []
    public deletechEvent: ((chItem:{characterId:number, itemId:number}) => void)[] = []
    public addItemEvent: ((item: Item) => void)[] = []
    public changeItemEvent: ((item: Item) => void)[] = []
    public deleteItemEvent: ((itemId: number) => void)[] = []
    public getTokenEvent: ((token: string) => void)[] = []

    constructor() {
        const token = localStorage.getItem("Token")!;
        this.itemHub = new signalR.HubConnectionBuilder()
            .withUrl(environment.apiURL + 'hubs/ItemHub',
                { accessTokenFactory: () => token })
            .build();
        this.ConfigureHub();
    }
    public StartWatch() {
        if (!this.wached) {
            this.itemHub.start();
            this.wached = true
        }
    }
    public StartWatchAndJoinPlayer(roomId:number, characterId:number) {
        if (!this.wached) {
            this.itemHub.start().then(() => this.JoinRoomPlayer(roomId, characterId));
            this.wached = true;
        }
        else this.JoinRoomPlayer(roomId, characterId);
    }
    public StartWatchAndJoinDm(roomId:number) {
        if (!this.wached) {
            this.itemHub.start().then(() => this.JoinRoomDm(roomId));
            this.wached = true
        }
        else this.JoinRoomDm(roomId)
    }
    public StopWatch() {
        if (this.wached) {
            this.itemHub.stop();
            this.wached = false;
        }
    }
    private JoinRoomPlayer(roomId:number, characterId:number) {
        this.itemHub.send("PlayerJoinRoom", roomId, characterId);
    }
    private JoinRoomDm(roomId:number) {
        this.itemHub.send("DmJoinRoom", roomId);
    }
    private ConfigureHub() {
        this.itemHub.on("AddedCharacterItem", (chitem: CharacterItem) => {
            this.addchEvent.forEach(e => e(chitem));
        });
        this.itemHub.on("UpdatedCharacterItem", (chitem: CharacterItem) => {
            this.changechEvent.forEach(e => e(chitem))
        });
        this.itemHub.on("DeletedCharacterItem", (chItem:{characterId:number, itemId:number}) => {
            this.deletechEvent.forEach(e => e(chItem))
        });
        this.itemHub.on("AddedItem", (item: Item) => {
            this.addItemEvent.forEach(e => e(item));
        });
        this.itemHub.on("UpdatedItem", (item: Item) => {
            this.changeItemEvent.forEach(e => e(item))
        });
        this.itemHub.on("DeletedItem", (itemId: number) => {
            this.deleteItemEvent.forEach(e => e(itemId))
        });
        this.itemHub.on("GetToken", (token: string) => {
            this.getTokenEvent.forEach(e => e(token))
        });
    }
}
