import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { HttpClientModule } from '@angular/common/http'
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faAngleDown as fAD, faAngleUp as fAU } from '@fortawesome/free-solid-svg-icons';
import { GenControllPanelComponent } from './gen-control-panel/gen-control-panel.component';
import { ItemTableComponent } from './workspace/item-table/item-table.component';
import { ItemElementComponent } from './workspace/item-table/item-element/item-element.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { GlobalRoomTableComponent } from './welcome/global-room-table/global-room-table.component';
import { UnauthorizedMenuComponent } from './welcome/unauthorized-menu/unauthorized-menu.component';
import { DmMenuComponent } from './welcome/dm-menu/dm-menu.component';
import { RoomWithDmForFullTableComponent } from './welcome/global-room-table/room-with-dm-for-full-table/room-with-dm-for-full-table.component';
import { RegistrationMenuComponent } from './welcome/registration-menu/registration-menu.component';
import { SigninMenuComponent } from './welcome/signin-menu/signin-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GenControllPanelComponent,
    ItemTableComponent,
    ItemElementComponent,
    WelcomeComponent,
    GlobalRoomTableComponent,
    UnauthorizedMenuComponent,
    DmMenuComponent,
    RoomWithDmForFullTableComponent,
    RegistrationMenuComponent,
    SigninMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(lib:FaIconLibrary) {
    lib.addIcons(fAD, fAU);
  }
}
