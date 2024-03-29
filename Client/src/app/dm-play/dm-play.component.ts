import { Component, OnInit } from '@angular/core';
import { Character } from '../model/character';
import { ActivatedRoute } from '@angular/router';
import { CharacterService } from '../services/character.service';
import { CharacterItemHubService } from '../services/characterItemHub.service';
import { firstValueFrom } from 'rxjs';
import { Room } from '../model/room';
import { RoomService } from '../services/room.service';
import { ItemService } from '../services/item.service';

@Component({
  selector: 'app-dm-play',
  templateUrl: './dm-play.component.html',
  styleUrls: ['./dm-play.component.scss']
})
export class DmPlayComponent implements OnInit {

  roomId:number = 0;
  room: Room = new Room(0, "", true);
  characters:Character[] = [];

  constructor(
    hubService:CharacterItemHubService,
    private route:ActivatedRoute, 
    private characterService:CharacterService,
    private roomService:RoomService
  ) {
    this.roomId = +this.route.snapshot.paramMap.get('roomId')!;
    roomService.StopWatch();
    hubService.StartWatchAndJoinDm(this.roomId);
  }

  async ngOnInit() {
    const th = this;
    this.roomService.GetRoom(this.roomId).subscribe({next(val) {
      th.room.id = val.id;
      th.room.name = val.name; 
      th.room.started = val.started;
    }});
    const characters = await firstValueFrom(this.characterService.GetCharacterObserv(this.roomId));
    this.characters = characters;
  }

}
