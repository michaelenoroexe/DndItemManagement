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
  newCharacterItem: CharacterItem | null = null;
  addNewCharacterItem: boolean = false;

  constructor(private router:Router, private chItemService:CharacterItemService) { }

  CreateNewCharacterItem() {
    this.newCharacterItem = new CharacterItem(this.characterId, 0, 0, 0);
    this.addNewCharacterItem = true;
  }
  AddCharacterItem() {
    this.newCharacterItem = null;
    this.addNewCharacterItem = false;
  }
  CancelCharacterItem() {
    this.newCharacterItem = null;
    this.addNewCharacterItem = false;
  }
  ItemChangeForChItem(it:any) {
    if(it == "New") this.CreateNewItem();
    
  }
  CreateNewItem() {
    this.router.navigate(['items', {add:true}]);
  }
}
