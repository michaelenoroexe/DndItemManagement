import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomService } from '../services/room.service';
import { CharacterService } from '../services/character.service';
import { Character } from '../model/character';
import { Room } from '../model/room';
import { CharacterItemHubService } from '../services/characterItemHub.service';

@Component({
  selector: 'app-join-room',
  templateUrl: './join-room.component.html',
  styleUrls: ['./join-room.component.scss']
})
export class JoinRoomComponent {

  room:Room = new Room(0, "", false);
  characters: Character[];
  selectedCharacter: Character | null = null;
  password:string = "";

  constructor(
    private router: Router,
    private route:ActivatedRoute,
    private hubService:CharacterItemHubService, 
    private roomService:RoomService,
    private characterService:CharacterService) {
      hubService.StartWatch();
      hubService.getTokenEvent.push(this.SaveToken);
    const th = this;
    const room = +route.snapshot.paramMap.get("roomId")!;
    roomService.GetRoom(room).subscribe({next(value:Room) {
        th.room = value;
    }});
    this.characters = characterService.GetCharacters(room);
  }

  SelectCharacter(character:Character) {
    this.selectedCharacter = character;
  }
  SignIn() {
    this.hubService.JoinRoomObj(this.room.id, this.password, this.selectedCharacter!.id)
    this.router.navigate(["character/play", {roomId:this.room.id, chId:this.selectedCharacter!.id}]);
  }
  SaveToken(token:string) {
    localStorage.setItem("Token", token);
  }
}
