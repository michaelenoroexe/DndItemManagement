import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoomWithDm } from 'src/app/model/room';

@Component({
  selector: 'app-room-with-dm-for-full-table',
  templateUrl: './room-with-dm-for-full-table.component.html',
  styleUrls: ['./room-with-dm-for-full-table.component.scss']
})
export class RoomWithDmForFullTableComponent{
  @Input()
  room!:RoomWithDm;
}
