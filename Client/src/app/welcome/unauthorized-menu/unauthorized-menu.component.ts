import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MenuState } from '../menu-state';

@Component({
  selector: 'app-unauthorized-menu',
  templateUrl: './unauthorized-menu.component.html',
  styleUrls: ['./unauthorized-menu.component.scss']
})
export class UnauthorizedMenuComponent {

  @Output()
  menuStateChange = new EventEmitter<MenuState>
  
  SignIn() { this.menuStateChange.emit(MenuState.SigningIn) }

  Register() { this.menuStateChange.emit(MenuState.Registration) }
}
