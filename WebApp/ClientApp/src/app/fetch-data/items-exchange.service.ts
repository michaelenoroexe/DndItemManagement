import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemsExchangeService {

  constructor(private client:HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  GetItemList():Observable<Item[]> {
    return this.client.get<Item[]>(this.baseUrl+'Items');
  }
  AddItem(item:Item):Observable<Item> {
    return this.client.post<Item>(this.baseUrl+'Items', {Name:item.name, Number:item.number});
  }
  EditItem(item:Item):Observable<Object> {
    return this.client.put(this.baseUrl+'Items', item);
  }
  DeleteItem(item:Item):Observable<Object> {
    return this.client.delete(this.baseUrl+'Items', {body: {ItemId:item.itemId}});
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
