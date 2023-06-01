import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MenuState } from '../menu-state';
import { Room } from 'src/app/model/room';
import { RoomService } from 'src/app/services/room.service';
import { DmService } from 'src/app/services/dm.service';
import { DM } from 'src/app/model/dm';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dm-menu',
  templateUrl: './dm-menu.component.html',
  styleUrls: ['./dm-menu.component.scss']
})
export class DmMenuComponent {
  rooms: Room[] = [];
  dm: DM;

  @Output()
  menuStateChange = new EventEmitter<MenuState>();

  constructor(private roomService:RoomService, private dmService:DmService, private router:Router) {
    this.dm = this.dmService.dm;
    this.rooms = this.roomService.dmRoomList;
  }

  AddNewRoom() {
    this.router.navigate(['rooms', {add:true}])
  }

  Exit() {
    this.dmService.Logout();
    this.menuStateChange.emit(MenuState.Unregistered);
  }
}
