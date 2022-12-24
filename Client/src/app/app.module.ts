import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faAngleDown as fAD, faAngleUp as fAU } from '@fortawesome/free-solid-svg-icons';
import { GenControllPanelComponent } from './gen-control-panel/gen-control-panel.component';
import { ItemTableComponent } from './workspace/item-table/item-table.component';
import { ItemElementComponent } from './workspace/item-table/item-element/item-element.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GenControllPanelComponent,
    ItemTableComponent,
    ItemElementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(lib:FaIconLibrary) {
    lib.addIcons(fAD, fAU);
  }
}
