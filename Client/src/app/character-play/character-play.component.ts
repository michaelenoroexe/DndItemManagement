import { AfterContentInit, AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CharacterService } from '../services/character.service';
import { Character } from '../model/character';
import { CharacterItemService } from '../services/characteritem.service';
import { CharacterItemHubService } from '../services/characterItemHub.service';
import { timeout } from 'rxjs';

@Component({
  selector: 'app-character-play',
  templateUrl: './character-play.component.html',
  styleUrls: ['./character-play.component.scss']
})
export class CharacterPlayComponent implements OnInit {

  roomId:number = 0;
  character:Character = {id:0, name:""}

  constructor(
    private route:ActivatedRoute, 
    private characterService:CharacterService,
    private hubService:CharacterItemHubService
  ) {
    this.roomId = +this.route.snapshot.paramMap.get('roomId')!;
    this.character.id = +this.route.snapshot.paramMap.get('chId')!;
    hubService.StartWatchAndJoinPlayer(this.roomId, this.character.id);
  }

  ngOnInit(): void {
    const th = this;
    this.characterService.GetCharacterObserv(this.roomId)
      .subscribe({
        next(val:Character[]) {
          th.character.name = val.find(c => c.id == th.character.id)!.name
        }
      });
  }
}
