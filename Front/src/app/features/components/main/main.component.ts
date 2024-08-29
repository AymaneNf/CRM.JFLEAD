import {Component, OnInit} from '@angular/core';
import {NgForOf} from "@angular/common";
import {StoreService} from "../../../core/services/store/store.service";
import {AuthService} from "../../../core/services/Auth/auth.service";
import {UserService} from "../../../core/services/User/user.service";
import {RouterLink} from "@angular/router";
import {log} from "@angular-devkit/build-angular/src/builders/ssr-dev-server";
import {LeadService} from "../../../core/services/Leads/lead.service";
import {Lead} from "../../../core/Models/Lead";
import {FormGroup} from "@angular/forms";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink
  ],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent implements OnInit{
  loggeduser:any = '';
  userName:string="";
  leads: Lead[] = [];  // Array to hold the list of leads
  errorMessage: string | null = null;
  filteredLeads: any[] = [];
  sortOption: string = 'date'; // Default sort option
  searchQuery: string = '';
  loading:boolean = false;

  constructor(private store:StoreService,
              private service:AuthService,
              private leadService: LeadService,
              private userService:UserService) {
    service.init().subscribe({
      next:(r)=>{
        console.log("db response !:",r);
      }
    })
  }
  ngOnInit() {
    this.loggeduser = this.store.isLogged();
    console.log("Id here : ",localStorage.getItem('id'));
    this.loadUser();
    this.getAllLeads();
  }
  deleteLead(id: string): void {
    this.leadService.deleteLead(id).subscribe({
      next: () => {
        this.leads = this.leads.filter(l => l.id !== id);
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }
  onrefresh(){

    this.loading = !this.loading;
    this.getAllLeads();
    setTimeout(() => {
      this.loading = !this.loading;
    },1000);

  }

  filterleads(): void {
    // Filter leads based on user role and id
    const userId = localStorage.getItem('id');


    // Sort filtered leads based on sortOption
    switch (this.sortOption) {
      case 'date':
        this.filteredLeads.sort((a, b) => {
          // Ensure both a and b have a valid createdOn date
          if (!a.createdOn || !b.createdOn) return 0;

          return new Date(b.createdOn).getTime() - new Date(a.createdOn).getTime();
        });
        break;
      case 'nom':
        this.filteredLeads.sort((a, b) => {
          // Ensure both a and b have a valid nom
          if (!a.nom || !b.nom) return 0;

          return a.nom.localeCompare(b.nom);
        });
        break;
      default:
        break;
    }


    if (this.searchQuery) {
      console.log("searching ...")
      this.filteredLeads = this.leads.filter(lead =>
        lead.nom?.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }else {
      this.filteredLeads = [...this.leads];
    }
  }

  onSearch(): void {
    this.filterleads();
  }
  onChangeSortOption(event: any): void {
    this.sortOption = event.target.value;
    this.filterleads();
  }


  getAllLeads(): void {
    this.leadService.getAllLeads().subscribe({
      next: (data) => {
        this.leads = data;
        this.filteredLeads = this.leads; // Filter leads based on the search query
      },
      error: (err) => {
        this.errorMessage = err.message;
        console.log(err);
      },
      complete: () => console.log("done")
    });
  }

 loadUser(){
   this.service.getUserDto("AD@gmail.com").subscribe({
     next:(Response)=> {
       this.userName = Response.result.name;
     },
     error:(e)=>{
       console.log(e);
     },
     complete:()=> {

     }
   });
 }
}
