import { Component, EventEmitter, Output } from '@angular/core';
import { environment } from 'src/environment';
import { MenuState } from '../menu-state';
import { HttpClient } from '@angular/common/http';
import { DmService } from 'src/app/services/dm.service';

@Component({
  selector: 'app-signin-menu',
  templateUrl: './signin-menu.component.html',
  styleUrls: ['./signin-menu.component.scss']
})
export class SigninMenuComponent {
  
  @Output()
  menuStateChange = new EventEmitter<MenuState>();

  login!: string;
  password!: string;
  errorMessage: string = "";

  constructor(private dmService:DmService) {}

  SignIn() {
    const th = this;
    this.dmService.SignIn(this.login, this.password).subscribe({
      next(value:any) {
        localStorage.setItem("Token", value.token);
        th.dmService.GetFullDm();
        th.menuStateChange.emit(MenuState.Dm);
      },
      error(err) {
          th.errorMessage = err.value;
      },
    })
  }

  Back() {
    this.menuStateChange.emit(MenuState.Unregistered);
  }
}
