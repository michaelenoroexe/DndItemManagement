import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { HttpClientModule } from '@angular/common/http'
import { FormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faAngleDown as fAD, faAngleUp as fAU } from '@fortawesome/free-solid-svg-icons';
import { GenControllPanelComponent } from './gen-control-panel/gen-control-panel.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { GlobalRoomTableComponent } from './welcome/global-room-table/global-room-table.component';
import { UnauthorizedMenuComponent } from './welcome/unauthorized-menu/unauthorized-menu.component';
import { DmMenuComponent } from './welcome/dm-menu/dm-menu.component';
import { RoomWithDmForFullTableComponent } from './welcome/global-room-table/room-with-dm-for-full-table/room-with-dm-for-full-table.component';
import { RegistrationMenuComponent } from './welcome/registration-menu/registration-menu.component';
import { SigninMenuComponent } from './welcome/signin-menu/signin-menu.component';
import { RoomManagementComponent } from './room-management/room-management.component';
import { FullRoomComponent } from './room-management/full-room/full-room.component';
import { CharacterForRoomManagementComponent } from './room-management/full-room/character-for-room-management/character-for-room-management.component';
import { DmPlayComponent } from './dm-play/dm-play.component';
import { CharacterPlayComponent } from './character-play/character-play.component';
import { PlayComponent } from './play/play.component';
import { CategoryComponent } from './play/category/category.component';
import { PlayItemComponent } from './play/category/play-item/play-item.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ItemManagementComponent } from './item-management/item-management.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GenControllPanelComponent,
    WelcomeComponent,
    GlobalRoomTableComponent,
    UnauthorizedMenuComponent,
    DmMenuComponent,
    RoomWithDmForFullTableComponent,
    RegistrationMenuComponent,
    SigninMenuComponent,
    RoomManagementComponent,
    FullRoomComponent,
    CharacterForRoomManagementComponent,
    DmPlayComponent,
    CharacterPlayComponent,
    PlayComponent,
    CategoryComponent,
    PlayItemComponent,
    ItemManagementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(lib:FaIconLibrary) {
    lib.addIcons(fAD, fAU);
  }
}
