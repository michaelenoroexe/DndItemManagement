import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'

@Injectable({
  providedIn: 'root'
})
export class ItemsHubService {

  private connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7200/")
    .build();

    private itemsChanged: IChangeHandler[];

    private changeItem(item:Item) {
      this.connection.invoke("itemChanged", item);
    }

  constructor() { 
    this.itemsChanged = [];
    this.connection.on("itemChanged", chItem => this.itemsChanged.forEach(call => call(chItem)));
    this.connection.start();
  }
  updateItem(item:Item) {
    this.changeItem(item);
  }
  addItem(item:Item) {
    if (item.itemId != "") throw "Incorrect new item id";
    this.changeItem(item);
  }
}
export class Item {
  readonly itemId:string;
  name:string;
  number:number;
  constructor(id:string = "", name:string = "", num:number = 0) {
    this.itemId = id;
    this.name = name;
    this.number = num;
  }
}
export interface IChangeHandler {
  (item:Item):void;
}
