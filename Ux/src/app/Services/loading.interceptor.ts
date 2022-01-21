import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, map, Observable, tap } from 'rxjs';
import { OverlayService } from './overlay.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private loadingService: OverlayService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler):Observable<HttpEvent<any>>{

    Promise.resolve(null).then(() => this.loadingService.show());

    return next.handle(req).pipe(tap({
        next: x=>{
            if(x instanceof HttpResponse){
                this.loadingService.hide();
            }
        },
        error: x=> {
            this.loadingService.hide();
        }
    }));
  }
}