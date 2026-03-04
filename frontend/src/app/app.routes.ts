import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { PageNotFound } from './pages/page-not-found/page-not-found';
import { PrivacyPolicy } from './pages/privacy-policy/privacy-policy';
import { ContactUs } from './pages/contact-us/contact-us';
import { Account } from './pages/account/account';
import { authGuard } from './guards/auth-guard';
import { adminGuard } from './guards/admin-guard-guard';
import { VehicleRoute } from './pages/vehicle-route/vehicle-route';
import { AddRoutes } from './Forms/add-routes/add-routes';
import { Admin } from './pages/admin/admin';
import { Vehicles } from './pages/vehicles/vehicles';
import { AddVehicle } from './Forms/add-vehicle/add-vehicle';
import { Schedules } from './pages/schedules/schedules';
import { Fares } from './pages/fares/fares';
import { UserPage } from './user/user-page/user-page';
import { Bookings } from './pages/bookings/bookings';
import { Users } from './pages/users/users';
import { MyBookings } from './user/my-bookings/my-bookings';

export const routes: Routes = [

  // ── Public routes (no guard) ──────────────────────
  { path: '', component: Home },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'privacy', component: PrivacyPolicy },
  { path: 'contact', component: ContactUs },

  // ── Authenticated routes (any logged in user) ─────
  { path: 'profile', component: Account, canActivate: [authGuard] },
  { path: 'User', component: UserPage, canActivate: [authGuard] },
  { path: 'myBookings', component: MyBookings, canActivate: [authGuard] },

  // ── Admin only routes ─────────────────────────────
  { path: 'adminPage', component: Admin, canActivate: [adminGuard] },
  { path: 'addRoute', component: AddRoutes, canActivate: [adminGuard] },
  { path: 'addvehicle', component: AddVehicle, canActivate: [adminGuard] },
  { path: 'vehicleRoute', component: VehicleRoute, canActivate: [adminGuard] },
  { path: 'vehicles', component: Vehicles, canActivate: [adminGuard] },
  { path: 'schedules', component: Schedules, canActivate: [adminGuard] },
  { path: 'Fares', component: Fares, canActivate: [adminGuard] },
  { path: 'Bookings', component: Bookings, canActivate: [adminGuard] },
  { path: 'Users', component: Users, canActivate: [adminGuard] },

  { path: '**', component: PageNotFound },
];