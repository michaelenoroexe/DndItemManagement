import { Component } from '@angular/core';

@Component({
  selector: 'app-gen-control-panel',
  templateUrl: './gen-control-panel.component.html',
  styleUrls: ['./gen-control-panel.component.scss']
})
export class GenControllPanelComponent {
  public extendedProfileControl : boolean = false
  public gmPlayer : boolean = true

  public changeProfileControl(state:boolean) : void {
    this.extendedProfileControl = state;
  }
}
