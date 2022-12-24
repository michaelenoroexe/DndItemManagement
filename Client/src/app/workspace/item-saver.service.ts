import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Item } from './item';

@Injectable({
  providedIn: 'root'
})
export class ItemSaverService {

  private hubConnection : signalR.HubConnection  = null!;

  public UpdateItem(item:Item) {
    this.hubConnection.send("UpdateItem", item);
  }
  
  public StartConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('http://localhost/api/itemHub/').build();

    this.hubConnection.start().then(() => this.UpdateItem(new Item("NewItem", 0, "asdff"))).catch(res => console.warn(res));
  }

  constructor() { 
    this.StartConnection();
  }
}
