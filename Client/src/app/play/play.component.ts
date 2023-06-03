import { Component, Input, OnInit } from '@angular/core';
import { Character } from '../model/character';
import { ItemService } from '../services/item.service';
import { CharacterItemService } from '../services/characteritem.service';
import { CharacterItem } from '../model/characterItem';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.scss', './full.scss', './slim.scss']
})
export class PlayComponent implements OnInit {
  @Input()
  roomId!:number;
  @Input()
  character!:Character;
  @Input()
  full:boolean = true;
  @Input()
  slim:boolean = false;

  characterItems:{[categoryId:number]:CharacterItem[]} = {};


  constructor(
    public itemService:ItemService, 
    private chItemService:CharacterItemService) {}

  ngOnInit(): void {
    const chItems = this.chItemService.GetCharacterItems(this.roomId, this.character.id);
    this.itemService.itemCategories
    .forEach(ic => {
      const itemsOfCateg = this.itemService.items
        .filter(i => i.itemCategoryId == ic.id);
      this.characterItems[ic.id] = chItems
        .filter(ci => itemsOfCateg.some(ioc => ci.itemId == ioc.id ));
    })
  }


}
