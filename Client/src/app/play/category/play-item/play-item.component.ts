import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CharacterItem } from 'src/app/model/characterItem';
import { Item } from 'src/app/model/item';
import { ItemService } from 'src/app/services/item.service';

@Component({
  selector: 'app-play-item',
  templateUrl: './play-item.component.html',
  styleUrls: ['./play-item.component.scss']
})
export class PlayItemComponent implements OnInit {
  @Input()
  item!:CharacterItem;
  @Input()
  addingNew:boolean = false;
  @Output()
  changeStateEvent:EventEmitter<CharacterItem> = new EventEmitter<CharacterItem>()
  itemInfo!:Item;
  full:boolean = false;
  changed:boolean = false;

  constructor(public itemService:ItemService) {}
  ngOnInit(): void {
    this.itemInfo = this.itemService.items.find(it => it.id == this.item.itemId)!;
    if(this.addingNew) {
      this.full = true;
      this.changed = true;
    }

  }
}
