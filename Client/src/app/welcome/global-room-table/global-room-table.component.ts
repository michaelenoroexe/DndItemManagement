import { Component, Input, OnInit } from '@angular/core';
import { RoomWithDm } from 'src/app/model/room';
import { faChessRook } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-global-room-table',
  templateUrl: './global-room-table.component.html',
  styleUrls: ['./global-room-table.component.scss']
})
export class GlobalRoomTableComponent{
  @Input()
  public rooms : RoomWithDm[] = []

  public castleIcon = faChessRook;
}
