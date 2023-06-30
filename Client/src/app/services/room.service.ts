import { Injectable } from "@angular/core";
import { Room, RoomWithDm } from "../model/room";
import * as signalR from "@microsoft/signalr";
import { environment } from "src/environment";
import { DmService } from "./dm.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class RoomService {

    private wached: boolean = false;
    public dmRoomList:Room[] = [];
    roomHub: signalR.HubConnection;

    public allRoomList:RoomWithDm[] = []

    public async UpdateDmRoomList() {
        const dm = this.dmService.dm;
        this.dmRoomList.splice(0, this.dmRoomList.length);
        this.allRoomList.filter(r => r.dmName == dm.login)
            .forEach(r => this.dmRoomList!.push(r));
    }
    public GetDmRoomList(dmId:number) {
        return this.http.get<Room[]>(`${environment.apiURL}dm/${dmId}/rooms`);
    }
    constructor(private dmService:DmService, private http:HttpClient) {
        let patchDoc = {name: "NewName"}
        this.http.patch<Room>(`${environment.apiURL}dm/1/rooms/1`, patchDoc).subscribe((nex) => {});
        const token = localStorage.getItem("Token")!;
        this.roomHub = new signalR.HubConnectionBuilder()
                            .withUrl(environment.apiURL + 'hubs/roomHub')
                            .build();
        this.ConfigureHub();
    }

    public StartWatch() {
        if (!this.wached) {
            this.roomHub.start();
            this.wached = true
        }
    }
    public StopWatch() {
        if (this.wached) {
            this.roomHub.stop();
            this.wached = false;
        }
    }
    public GetRoom(roomId:number) {        
        return this.http.get<Room>(`${environment.apiURL}rooms/${roomId}`);
    }
    public StartRoom(room:Room) {
        room.started = true;
        return this.UpdateRoom(room);
    }
    public AddRoom(name:string, password:string) {
        const token = localStorage.getItem("Token")!;
        return this.http.post(`${environment.apiURL}dm/${this.dmService.dm.id}/rooms`, 
        { name:name, password:password }, {headers: {"Authorization": "Bearer " + token}});
    }
    public UpdateRoom(room:Room) {
        const token = localStorage.getItem("Token")!;
        return this.http.put(`${environment.apiURL}dm/${this.dmService.dm.id}/rooms/${room.id}`, 
        room, {headers: {"Authorization": "Bearer " + token}});
    }
    private ConfigureHub() {
        this.roomHub.on("AddedRooms", rooms => {
            rooms.forEach((r:any) => this.allRoomList.push(r));
            this.UpdateDmRoomList();
        });
        this.roomHub.on("UpdatedRoom", (room:Room) => {  
            const changedRoom = this.allRoomList.find(r => r.id == room.id)!;
            changedRoom.name = room.name;
        });
        this.roomHub.on("DeletedRoom", roomId => {
            const roomIndex = this.allRoomList.findIndex(r => r.id == roomId);
            this.allRoomList.splice(roomIndex, 1);
        });
    }
}
