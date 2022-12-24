import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ItemSaverService {

  private hubConnection : signalR.HubConnection  = null!;
  
  public StartConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('http://localhost/').build();

    this.hubConnection.start().then(() => console.log('Connection Started')).catch(res => console.warn(res));
  }

  constructor() { 
    this.StartConnection();
  }
}
