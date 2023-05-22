import { Component } from '@angular/core';
import { RoomWithDm } from 'src/app/model/room';
import { faChessRook } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-global-room-table',
  templateUrl: './global-room-table.component.html',
  styleUrls: ['./global-room-table.component.scss']
})
export class GlobalRoomTableComponent {
  public castleIcon = faChessRook;
  public rooms : RoomWithDm[] = [
    new RoomWithDm(1, "Room1", "Dm1"),
    new RoomWithDm(2, "Room2", "Dm1"),
    new RoomWithDm(3, "Room3", "Dm1"),
    new RoomWithDm(4, "Room1", "Dm2"),
    new RoomWithDm(5, "Room2", "Dm2"),
    new RoomWithDm(6, "Room3", "Dm2"),
    new RoomWithDm(7, "Room4", "Dm2"),
    new RoomWithDm(8, "Room1", "Dm3"),
    new RoomWithDm(9, "Room2", "Dm3")
  ]
}
