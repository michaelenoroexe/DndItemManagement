import { Component } from '@angular/core';
import { MenuState } from './menu-state';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent {
  menu : MenuState = MenuState.Unregistered
  states = MenuState
}
