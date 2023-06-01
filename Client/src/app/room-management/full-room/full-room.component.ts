import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Character } from 'src/app/model/character';
import { Room } from 'src/app/model/room';
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons'
import { DmService } from 'src/app/services/dm.service';
import { environment } from 'src/environment';
import { CharacterService } from 'src/app/services/character.service';
import { RoomService } from 'src/app/services/room.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-full-room',
  templateUrl: './full-room.component.html',
  styleUrls: ['./full-room.component.scss']
})
export class FullRoomComponent implements OnInit {
  @ViewChild('roomName') roomName!: ElementRef;
  @Input()
  room!: Room;
  characters: Character[] = [];
  changingRoomName: boolean = false;
  addingCharacter: boolean = false;
  penIcon = faPen;
  delIcon = faTrash;

  constructor(private characterService:CharacterService, 
    private roomService:RoomService, private router:Router) { }

  ngOnInit(): void {
    this.characters = this.characterService.GetCharacters(this.room.id);
  }
  StartRoom() {
    this.roomService.StartRoom(this.room);
    this.router.navigate(["dm/play", {roomId:this.room.id}]);
  }
  //#region Room name
  UpdateRoomName() {
    if (!this.changingRoomName) {
      this.changingRoomName = true;
      this.roomName.nativeElement.focus();
    }
  }
  ApplyRoomNameChange() {
    if (this.changingRoomName) {
      this.changingRoomName = false;
      this.roomService.UpdateRoom(this.room).subscribe({error(err) {console.log(err)}});
    }
  }
  //#endregion
  //#region Character
  AddNewCharacter() {
    this.addingCharacter = true;
    this.characters.push(new Character(0, ""));
  }
  UpdateState(ch:Character) {
    this.addingCharacter = false;
    if (ch.name.length < 1) {
      const chIndex = this.characters.findIndex(c => c.id == ch.id);
      this.characters.splice(chIndex, 1);
    }
    else {
      // New valid character
      if (ch.id == 0) {
        this.characterService.AddCharacters(this.room.id, ch.name)
        .subscribe({next(character:Character) { ch.id = character.id }});
      }
      // Update valid character
      else this.characterService.UpateCharacter(this.room.id, ch).subscribe({error(err) {console.log(err)}});
    }
  }
  //#endregion
}
