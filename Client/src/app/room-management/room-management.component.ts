import { Component, OnInit } from '@angular/core';
import { Room } from '../model/room';
import { DmService } from '../services/dm.service';
import { RoomService } from '../services/room.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-room-management',
  templateUrl: './room-management.component.html',
  styleUrls: ['./room-management.component.scss']
})
export class RoomManagementComponent implements OnInit {

  change:boolean = false;
  changedRoomId: number = 0;
  changedRoomName: string = "";
  changedPassword: string ="******";
  currAction!: () => void;

  rooms: Room[] = []

  constructor(private roomService:RoomService, private route:ActivatedRoute) {
    roomService.StartWatchRoomList();
    this.rooms = roomService.dmRoomList;
  }
  ngOnInit(): void {
    if (this.route.snapshot.paramMap.get('add')) this.OpenRoomInfoPanel();
  }

  OpenRoomInfoPanel(id:number = 0) {
    if (id == 0) {
      this.changedRoomId = 0;
      this.changedRoomName = "";
      this.changedPassword = "";
      this.currAction = this.Add;
    }
    else {
      const curr = this.rooms.find(r => r.id == id)!;
      this.changedRoomId = curr.id;
      this.changedRoomName = curr.name;
      this.changedPassword = "******";
      this.currAction = this.Change;
    }
    this.change = true;
  }
  CloseInfo() {this.change = false}
  Change() {
    const curr = this.rooms.find(r => r.id == this.changedRoomId)!;
    if (this.changedPassword == "******") console.log("WOPass");
    else console.log("Pass");
    curr.name = this.changedRoomName;
  }
  Add() {
    const th = this;
    this.roomService.AddRoom(this.changedRoomName, this.changedPassword)
    .subscribe({next(value) { th.CloseInfo() }});
  }
}
