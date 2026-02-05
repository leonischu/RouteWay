import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { PageNotFound } from './pages/page-not-found/page-not-found';
import { PrivacyPolicy } from './pages/privacy-policy/privacy-policy';
import { ContactUs } from './pages/contact-us/contact-us';
import { Account } from './pages/account/account';
import { authGuard } from './guards/auth-guard';
import { VehicleRoute } from './pages/vehicle-route/vehicle-route';

export const routes: Routes = [

      { path: '', component: Home },
      { path: 'login', component: Login },
      { path: 'register', component: Register },
      { path: 'privacy', component: PrivacyPolicy },
      { path: 'contact', component: ContactUs },
      { path: 'profile', component: Account,canActivate:[authGuard] },
      { path: 'vehicleRoute', component: VehicleRoute },
      { path: "**", component:PageNotFound},

];
