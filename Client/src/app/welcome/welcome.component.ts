import { AfterContentChecked, Component } from '@angular/core';
import { MenuState } from './menu-state';
import { RoomWithDm } from '../model/room';
import { RoomService } from '../services/room.service';
import { DmService } from '../services/dm.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements AfterContentChecked {
  menu : MenuState = MenuState.Unregistered;
  states = MenuState;
  public rooms: RoomWithDm[];

  constructor(private roomService:RoomService, private dmService:DmService) {
    roomService.StartWatchRoomList();
    if (dmService.dm.id == 0) this.menu = MenuState.Unregistered;
    else this.menu = MenuState.Dm;
    this.rooms = this.roomService.allRoomList;
  }
  ngAfterContentChecked(): void {
    if (this.menu != MenuState.Dm && this.dmService.dm.id != 0) this.menu = MenuState.Dm;
  }
}
