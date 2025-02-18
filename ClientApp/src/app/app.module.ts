import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, InjectionToken, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StoreModule } from '@ngrx/store';
import { BehaviorSubject } from 'rxjs';
import { InitializeService } from './Initialize.service';
import { RemoveHeaderInterceptor } from './RemoveHeaderInterceptor';

export function GetThemeChanger() {
  return new BehaviorSubject<string>('');
}

export const ThemeChanger = new InjectionToken<BehaviorSubject<string>>('changes theme');

export function initApp(service: InitializeService) {
  return () => service.fetch();
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'people', loadComponent: () => import('./people/people.component').then(m => m.PeopleComponent) },
      { path: 'places', loadComponent: () => import('./places/places.component').then(m => m.PlacesComponent) },
      { path: 'things', loadComponent: () => import('./things/things.component').then(m => m.ThingsComponent) },
      { path: '', redirectTo: 'people', pathMatch: 'full' },
    ], { initialNavigation: 'disabled' }),
    StoreModule.forRoot({}, {})
  ],
  providers: [{ provide: ThemeChanger, useFactory: GetThemeChanger },
    { provide: APP_INITIALIZER, useFactory: initApp, deps: [InitializeService], multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: RemoveHeaderInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
