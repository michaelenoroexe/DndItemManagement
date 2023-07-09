import { AfterContentChecked, Component } from '@angular/core';
import { MenuState } from './menu-state';
import { RoomWithDm } from '../model/room';
import { RoomService } from '../services/room.service';
import { DmService } from '../services/dm.service';
import { ItemService } from '../services/item.service';
import { Logger, LoggerProvider } from '../services/logger.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements AfterContentChecked {
  logger : Logger
  menu : MenuState = MenuState.Unregistered;
  states = MenuState;
  public rooms: RoomWithDm[];

  constructor(private dmService:DmService, 
      roomService:RoomService, 
      itemService:ItemService, 
      LogProv:LoggerProvider,
      router:Router) {
    this.logger = LogProv.GetLogger(router.url);
    roomService.StartWatch();
    itemService.StopWatch();
    if (dmService.dm.id == 0) this.menu = MenuState.Unregistered;
    else this.menu = MenuState.Dm;
    this.rooms = roomService.allRoomList;
  }
  ngAfterContentChecked(): void {
    if (this.menu != MenuState.Dm && this.dmService.dm.id != 0) {
      this.menu = MenuState.Dm;
      this.logger.Information(`Initialized dm: ${this.dmService.dm.login}`);
    }
  }
}
