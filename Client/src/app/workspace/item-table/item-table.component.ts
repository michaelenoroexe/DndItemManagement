import { Component } from '@angular/core';
import { Item } from '../item';
import { ItemSaverService } from '../item-saver.service';

@Component({
  selector: 'app-item-table',
  templateUrl: './item-table.component.html',
  styleUrls: ['./item-table.component.scss']
})
export class ItemTableComponent {
  public serverRespond : string = ""
  public message : string = ""


  constructor(private saver : ItemSaverService) {
    saver.hubConnection.on("handleMessage", (mess) => this.serverRespond = this.serverRespond.concat("<br />", mess))
  }

  public send() : void {
    this.saver.hubConnection.send("UpdateItem", new Item("NewName", 5)).then(_ => this.message = "");
  }
}