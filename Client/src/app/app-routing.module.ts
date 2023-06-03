import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WelcomeComponent } from './welcome/welcome.component';
import { RoomManagementComponent } from './room-management/room-management.component';
import { DmPlayComponent } from './dm-play/dm-play.component';
import { CharacterPlayComponent } from './character-play/character-play.component';
import { ItemManagementComponent } from './item-management/item-management.component';

const routes: Routes = [
  { path: "", component: WelcomeComponent },
  { path: "rooms", component: RoomManagementComponent },
  { path: "items", component: ItemManagementComponent },
  { path: "dm/play", component: DmPlayComponent },
  { path: "character/play", component: CharacterPlayComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
