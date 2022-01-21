import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { LoadingOverlayComponent } from '../shared/loading-overlay/loading-overlay.component';

@Injectable({
  providedIn: 'root'
})
export class OverlayService {
  count:number = 0;
  private overlayRef: OverlayRef = null;

  constructor(private overlay:Overlay) { }

  public show(message=''){
    if(!this.overlayRef){
      this.overlayRef = this.overlay.create();
    }    
    
    if(this.count == 0){     
      const overlayPortal = new ComponentPortal(LoadingOverlayComponent); 
      this.overlayRef.attach(overlayPortal);
    }    
    this.count++;
  }

  public hide(){
    this.count--;
    if(!!this.overlayRef && this.count == 0){
      this.overlayRef.detach();
    }
  }
}
