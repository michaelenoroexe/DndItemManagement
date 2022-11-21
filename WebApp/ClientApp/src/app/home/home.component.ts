import { Component } from '@angular/core';
import { Item, ItemsExchangeService } from '../fetch-data/items-exchange.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  items: Item[] = []
  selectedItem: Item;

  constructor(private itemsExService:ItemsExchangeService) {
    itemsExService.GetItemList().subscribe(res => this.items = res);
    this.selectedItem = new Item();
  }
  //#region Manage items.
  async AddItem() {
    if (this.selectedItem == null) return;
    let res = await this.itemsExService.AddItem(
      this.selectedItem).toPromise();
    this.items.push(res);
  }
  async EditItem() {
    if (this.selectedItem == null) return;
    await this.itemsExService.EditItem(this.selectedItem).toPromise();
  }
  async DeleteItem() {
    if (this.selectedItem == null) return;
    await this.itemsExService.DeleteItem(this.selectedItem).toPromise();
  }
  //#endregion
  //#region Select current item
  SelectCurrent(it:Item) {this.selectedItem = it;}
  SelectBlank() {this.selectedItem = new Item();}
  //#endregion
}
