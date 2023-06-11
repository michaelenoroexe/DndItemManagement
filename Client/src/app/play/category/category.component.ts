import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CharacterItem } from 'src/app/model/characterItem';
import { Item } from 'src/app/model/item';
import { ItemCategory } from 'src/app/model/itemCategory';
import { CharacterItemService } from 'src/app/services/characteritem.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss', '../full.scss', '../slim.scss']
})
export class CategoryComponent {
  @Input()
  category!: ItemCategory;
  @Input()
  characterItems!: CharacterItem[];
  @Input()
  itemsForCategory!: Item[]
  @Input()
  characterId!:number
  @Input()
  dm:boolean = false;
  @Input()
  roomId:number = 0;
  newCharacterItem: CharacterItem = new CharacterItem(this.characterId, 0, 0, 0);
  addNewCharacterItem: boolean = false;

  constructor(private router:Router, private chItemService:CharacterItemService) { 
    chItemService.ListenAddingChEvent((chItem) => {
      if(chItem.characterId == this.characterId && 
        this.itemsForCategory.some(i => i.id == chItem.itemId)) this.characterItems.push(chItem);
    });
    chItemService.ListenDeleteChEvent((chItem) => {
      if (chItem.characterId == this.characterId) {
        const chItemIndex = this.characterItems.findIndex(ci => ci.itemId == chItem.itemId);
        this.characterItems.splice(chItemIndex, 1);
      };
    });
  }

  CreateNewCharacterItem() {
    this.newCharacterItem = new CharacterItem(this.characterId, 0, 0, 0);
    this.addNewCharacterItem = true;
  }
  AddCharacterItem() {
    this.chItemService.AddChItem(this.newCharacterItem!);
    this.addNewCharacterItem = false;
  }
  CancelCharacterItem() {
    this.newCharacterItem = new CharacterItem(this.characterId, 0, 0, 0);
    this.addNewCharacterItem = false;
  }
  ItemChangeForChItem(it:any) {
    if(it == "New") this.CreateNewItem();
    this.newCharacterItem!.itemId = +it;
  }
  CreateNewItem() {
    this.router.navigate(['items', {add:true}]);
  }
}
