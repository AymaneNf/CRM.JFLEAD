import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {NgIf} from "@angular/common";
import {StoreService} from "../../services/store/store.service";
import {AuthService} from "../../services/Auth/auth.service";
import {routes} from "../../../app.routes";
import {UserService} from "../../services/User/user.service";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {forbiddenCharsValidator} from "../../Validators/forbiddenCharsValidator";
import {noSpacesValidator} from "../../Validators/noSpaceValidator";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    RouterLink,
    NgIf,
    ReactiveFormsModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{
  @Input() drawerCheckboxId!: string;
  @Input() modalInput!: string;
  @Input() hidden!: boolean;

  profilepic : any = null;
  path:string="";
  user:any;
  invNumber:any;
  workspaceCode!:string;
  codeForm!:FormGroup;
  constructor(private store:StoreService,
              private authService:AuthService,
              private userService:UserService,
              private router:Router,
              private route:ActivatedRoute,
              private builder:FormBuilder) {
  }



  ngOnInit(): void {
    //this.onLoadCurrentUser()
    this.path = this.route.snapshot.url[0].path;
    this.codeForm = this.builder.group({
      code:this.builder.control('',[
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(8),
        forbiddenCharsValidator(/[#!]/),
        noSpacesValidator()
      ])
    })


  }
  onLogout(){
    this.authService.logout();
    this.router.navigate(['Login']);


  }


  onLoadCurrentUser(){
    this.userService.getUserDto(localStorage.getItem('id')).subscribe({
      next:(res:any)=>{
        this.user =res.result;
        console.log(this.user);
      }
    })
  }


  protected readonly localStorage = localStorage;

}
