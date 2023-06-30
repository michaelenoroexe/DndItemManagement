import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CharacterItem } from 'src/app/model/characterItem';
import { Item } from 'src/app/model/item';
import { ItemService } from 'src/app/services/item.service';
import { faCaretDown } from '@fortawesome/free-solid-svg-icons'
import { CharacterItemService } from 'src/app/services/characteritem.service';

@Component({
  selector: 'app-play-item',
  templateUrl: './play-item.component.html',
  styleUrls: ['./play-item.component.scss']
})
export class PlayItemComponent implements OnInit {
  @Input()
  item!:CharacterItem;
  @Input()
  roomId:number = 0;
  @Input()
  dm:boolean = false;
  itemInfo!:Item;
  changingItem!:CharacterItem;
  changingItemInfo!:Item;
  full:boolean = false;
  changed:boolean = false;
  down = faCaretDown;

  constructor(public itemService:ItemService, private chItemService:CharacterItemService) {}
  ngOnInit(): void {
    this.itemService.items.forEach(ic => {
      const ii = ic.find(it => it.id == this.item.itemId);
      if (ii != undefined) {
        this.itemInfo = ii;
      }
    });
  }
  ChangeCharacterInfo() {
    this.item.currentDurability = +this.item.currentDurability;
    this.chItemService.FullChangeChItem(this.roomId, this.item);
  }
  ChangeFullInfo() {
    this.changingItem.currentDurability = +this.changingItem.currentDurability;
    this.changingItem.itemNumber = +this.changingItem.itemNumber;

    this.changingItemInfo.maxDurability = +this.changingItemInfo.maxDurability;
    this.changingItemInfo.price = +this.changingItemInfo.price;
    this.changingItemInfo.weight = +this.changingItemInfo.weight;

    this.chItemService.FullChangeChItem(this.roomId, this.changingItem);
    this.itemService.FullChangeItem(this.roomId, this.changingItemInfo);
    this.full = false;
    this.changed = false;
  }
  Expand() {
    this.changingItemInfo = new Item(
      this.itemInfo.id, this.itemInfo. name, 
      this.itemInfo.maxDurability, this.itemInfo. price, 
      this.itemInfo.weight, this.itemInfo. secretItemDescription, 
      this.itemInfo.itemDescription, this.itemInfo. itemCategoryId, 
      this.itemInfo.itemCategory); 
    this.changingItem = new CharacterItem(this.item.characterId, 
                                          this.item.itemId, 
                                          this.item.itemNumber, 
                                          this.item.currentDurability);
    this.full = true;
    this.changed = false;
  }
  PreventExpand(event:any) {
    event.stopPropagation();
  }
}
