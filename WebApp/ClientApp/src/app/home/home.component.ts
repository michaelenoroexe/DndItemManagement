import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  items: any = []
  selectedItem: any;

  constructor() {
  }
  //#region Manage items.
  async AddItem() {
    if (this.selectedItem == null) return;
    let res = {}
    this.items.push(res);
  }
  async EditItem() {
    if (this.selectedItem == null) return;
  }
  async DeleteItem() {
    if (this.selectedItem == null) return;
  }
  //#endregion
  //#region Select current item
  SelectCurrent(it:any) {this.selectedItem = it;}
  SelectBlank() {this.selectedItem = {}}
  //#endregion
}
