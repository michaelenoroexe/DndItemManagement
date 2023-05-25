import { Component, EventEmitter, Output } from '@angular/core';
import { MenuState } from '../menu-state';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment';

@Component({
  selector: 'app-registration-menu',
  templateUrl: './registration-menu.component.html',
  styleUrls: ['./registration-menu.component.scss']
})
export class RegistrationMenuComponent {

  @Output()
  menuStateChange = new EventEmitter<MenuState>();

  login!: string;
  password!: string;

  constructor(private http:HttpClient) {}

  Register() {
    const user = {Login:this.login, Password:this.password};
    this.http.post(environment.apiURL + "dm", user);
  }

  Back() {
    this.menuStateChange.emit(MenuState.Unregistered);
  }
}
