import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Item } from './item';

@Injectable({
  providedIn: 'root'
})
export class ItemSaverService {

  public hubConnection : signalR.HubConnection  = null!;

  constructor() { 
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('http://localhost/api/itemHub/').build();

    this.hubConnection.start().catch(res => console.warn(res));
  }
}
