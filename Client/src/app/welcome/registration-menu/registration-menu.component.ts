import { Component, EventEmitter, Output } from '@angular/core';
import { MenuState } from '../menu-state';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment';
import { DmService } from 'src/app/services/dm.service';

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
  errorMessage: string = "";

  constructor(private dmService:DmService) {}

  Register() {
    const th = this;
    this.dmService.Register(this.login, this.password).subscribe({
      next(value) {
        th.menuStateChange.emit(MenuState.SigningIn);
      },
      error(err) {
          th.errorMessage = err.value;
      },
    });
    
  }

  Back() {
    this.menuStateChange.emit(MenuState.Unregistered);
  }
}
