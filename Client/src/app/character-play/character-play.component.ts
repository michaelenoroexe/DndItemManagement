import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CharacterService } from '../services/character.service';
import { Character } from '../model/character';

@Component({
  selector: 'app-character-play',
  templateUrl: './character-play.component.html',
  styleUrls: ['./character-play.component.scss']
})
export class CharacterPlayComponent implements OnInit {

  roomId:number = 0;
  @Input()
  character:Character = {id:0, name:""}

  constructor(
    private route:ActivatedRoute, 
    private characterService:CharacterService
  ) {}

  ngOnInit(): void {
    this.roomId = +this.route.snapshot.paramMap.get('roomId')!;
    if (this.character.id == 0) {
      const th = this;
      this.character.id = +this.route.snapshot.paramMap.get('chId')!;
      this.characterService.GetCharacterObserv(this.roomId)
        .subscribe({
          next(val:Character[]) {
            th.character.name = val.find(c => c.id == th.character.id)!.name
          }
        })
    }
  }

}
