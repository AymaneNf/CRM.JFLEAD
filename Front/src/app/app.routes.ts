import { Routes } from '@angular/router';
import { LoginComponent } from './features/pages/login/login.component';
import { HomeComponent } from './features/pages/home/home.component';
import { RegisterComponent } from './features/pages/register/register.component';
import { PageNotFoundComponent } from './features/pages/page-not-found/page-not-found.component';
import {ProfileComponent} from "./features/pages/profile/profile.component";
import {AuthGuard} from "./core/Guards/auth.guard";
import {WelcomeComponent} from "./features/pages/welcome/welcome.component";
import {ConnectionlostComponent} from "./features/pages/connectionlost/connectionlost.component";
import {MainComponent} from "./features/components/main/main.component";
import {ContactsComponent} from "./features/components/contacts/contacts.component";
import {DocumentsComponent} from "./features/components/documents/documents.component";
import {PistesComponent} from "./features/components/pistes/pistes.component";


export const routes: Routes = [

  { path: '',
    pathMatch: 'full',
    redirectTo: 'Welcome' },

  { path: 'Login',
    component: LoginComponent,
    title: 'Login' },

  { path: 'Home',
    component: HomeComponent,
    canActivate:[AuthGuard],
    title: 'Home',
    children:[
      {path: '',redirectTo: 'main',pathMatch: "full"},
      {path: 'main',component: MainComponent,title: "main"},
      {path: 'Contacts',component: ContactsComponent,title: "Contacts"},
      {path: 'Docs',component: DocumentsComponent,title: "Docs"},
      {path: 'Pistes',component: PistesComponent,title: "pistes"}
    ]
  },

  { path: 'Register',
    component: RegisterComponent,
    title: 'Register' },




  {path:'Profile',
    title:'Profile',
    component:ProfileComponent},




  {path:'Welcome',
    title:'Welcome',
    component:WelcomeComponent},
{path:'ConnectionLost',
    title:'Connection Lost',
    component:ConnectionlostComponent},

  { path: '**',
    pathMatch: 'full',
    component: PageNotFoundComponent },

];
