import { Component } from '@angular/core';
import { ItemSaverService } from '../item-saver.service';

@Component({
  selector: 'app-item-table',
  templateUrl: './item-table.component.html',
  styleUrls: ['./item-table.component.scss']
})
export class ItemTableComponent {

  constructor(private saver : ItemSaverService) {
    console.log('Constructor of itemTable');
  }
}
