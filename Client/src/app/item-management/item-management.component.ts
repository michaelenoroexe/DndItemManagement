import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ItemService } from '../services/item.service';
import { Item } from '../model/item';
import { ItemCategory } from '../model/itemCategory';
import { RoomService } from '../services/room.service';
import { Room } from '../model/room';
import { faPen, faTrash } from '@fortawesome/free-solid-svg-icons';
import { DmService } from '../services/dm.service';
import { firstValueFrom } from 'rxjs';
import { DM } from '../model/dm';
import { CharacterItemHubService } from '../services/characterItemHub.service';

@Component({
  selector: 'app-item-management',
  templateUrl: './item-management.component.html',
  styleUrls: ['./item-management.component.scss']
})
export class ItemManagementComponent implements OnInit {
  penIcon = faPen;
  delIcon = faTrash;

  room:Room = new Room(0, "asd", false);
  addNew:boolean = false;
  tempItem:Item = new Item(0, "", 1, 1, 1, "", "", 0, new ItemCategory(0, ""));

  constructor(
    hubService:CharacterItemHubService,
    public itemService:ItemService, 
    private roomService:RoomService,
    private route:ActivatedRoute,
    private dmService:DmService) {
      roomService.StopWatch();
      hubService.StartWatchAndJoinDm(+this.route.snapshot.paramMap.get('roomId')!);
    }
  
  async ngOnInit(): Promise<void> {
    const th = this;
    this.roomService.GetRoom(+this.route.snapshot.paramMap.get('roomId')!)
    .subscribe({next(ro:any) {
      th.room.id = ro.id;
      th.room.name = ro.name;
      th.room.started = ro.started;
    }});
  }
  SelectItemForChange(item:Item) {
    this.tempItem.id = item.id;
    this.tempItem.name = item.name;
    this.tempItem.itemCategoryId = item.itemCategoryId;
    this.tempItem.itemCategory = item.itemCategory;
    this.tempItem.itemDescription = item.itemDescription;
    this.tempItem.maxDurability = item.maxDurability;
    this.tempItem.price = item.price;
    this.tempItem.secretItemDescription = item.secretItemDescription;
    this.tempItem.weight = item.weight;
  }
  WorkWithNewItem(categId:number) {
    this.tempItem = new Item(0, "", 1, 1, 1, "", "", 
    categId, this.itemService.itemCategories.find(ic => ic.id == categId)!);
  }
  AddItem() {
    this.itemService.AddItem(this.room.id, this.tempItem);
  }
  UpdateItem() {
    this.itemService.FullChangeItem(this.room.id, this.tempItem);
  }
  Cancel() {
    this.tempItem = new Item(0, "", 1, 1, 1, "", "", 0, new ItemCategory(0, ""));
  }
  DeleteItem() {
    this.itemService.DeleteItem(this.room.id, this.tempItem);
  }
}
