import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faAngleDown as fAD, faAngleUp as fAU } from '@fortawesome/free-solid-svg-icons';
import { GenControllPanelComponent } from './gen-control-panel/gen-control-panel.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GenControllPanelComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(lib:FaIconLibrary) {
    lib.addIcons(fAD, fAU);
  }
}
