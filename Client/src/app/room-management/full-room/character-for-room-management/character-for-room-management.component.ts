import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Character } from 'src/app/model/character';
import { CharacterService } from 'src/app/services/character.service';

@Component({
  selector: 'app-character-for-room-management',
  templateUrl: './character-for-room-management.component.html',
  styleUrls: ['./character-for-room-management.component.scss']
})
export class CharacterForRoomManagementComponent implements AfterViewInit {
  @ViewChild("characterName") characterName!: ElementRef
  @Input()
  character!:Character;
  @Input()
  roomId!:number;
  @Output()
  characterUpdate:EventEmitter<Character> = new EventEmitter<Character>();

  edited:boolean = false;
  penIcon = faPen;
  delIcon = faTrash;

  constructor(private characterService:CharacterService) { }
  ngAfterViewInit(): void {
    if (this.character.id == 0) this.UpdateCharacterName();
  }

  UpdateCharacterName() {
    if (!this.edited) {
      this.edited = true;
      this.characterName.nativeElement.focus();
    }
  }
  ApplyCharacterNameChange() {
    if (this.edited) {
      this.edited = false;
      this.characterUpdate.emit(this.character);
    }
  }
}
