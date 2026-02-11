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
import { AddRoutes } from './Forms/add-routes/add-routes';
import { Admin } from './pages/admin/admin';
import { Vehicles } from './pages/vehicles/vehicles';
import { AddVehicle } from './Forms/add-vehicle/add-vehicle';
import { Schedules } from './pages/schedules/schedules';
import { Fares } from './pages/fares/fares';
import { UserPage } from './user/user-page/user-page';

export const routes: Routes = [

      { path: '', component: Home },
      { path: 'login', component: Login },
      { path: 'register', component: Register },
      { path: 'privacy', component: PrivacyPolicy },
      { path: 'contact', component: ContactUs },
      { path: 'profile', component: Account,canActivate:[authGuard] },
      { path: 'addRoute', component: AddRoutes,canActivate:[authGuard]  },
      { path: 'addvehicle', component: AddVehicle,canActivate:[authGuard]  },

      { path: 'vehicleRoute', component: VehicleRoute,canActivate:[authGuard]  },
      { path: 'adminPage', component: Admin,canActivate:[authGuard]},
      { path: 'vehicles', component: Vehicles,canActivate:[authGuard]},
      { path: 'schedules', component: Schedules,canActivate:[authGuard]},
      { path: 'Fares', component: Fares,canActivate:[authGuard]},
      { path: 'User', component: UserPage,canActivate:[authGuard]},
      
      { path: "**", component:PageNotFound},

];
