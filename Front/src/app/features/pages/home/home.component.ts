import { Component, OnInit } from '@angular/core';
import {RouterLink, RouterOutlet} from '@angular/router';
import { StoreService } from '../../../core/services/store/store.service';
import { AuthService } from '../../../core/services/Auth/auth.service';
import {HeaderComponent} from "../../../core/componenets/header/header.component";

import {NgForOf, NgIf} from "@angular/common";
import {UserService} from "../../../core/services/User/user.service";
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, HeaderComponent, NgForOf, NgIf, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  loading:boolean = true;

  constructor(
    ) {

  }

  ngOnInit(): void {



    setTimeout(() => {

      this.loading = !this.loading;
       },1000);


  }







  protected readonly localStorage = localStorage;
}
