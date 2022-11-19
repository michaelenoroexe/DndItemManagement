import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  items: {Name:string, Number:number}[] = [
    {Name:"Паёк", Number:6},
    {Name:"Палатка", Number:1},
    {Name:"Факелы", Number:2},
    {Name:"Свечи", Number:3},
    {Name:"Колышки", Number:2}
  ]
  newItemName:string = "";
  newItemNumber:number = 0;

  AddItem()
  {
    this.items.push({Name:this.newItemName, Number:this.newItemNumber});
  }
}
