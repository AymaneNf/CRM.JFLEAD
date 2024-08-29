import { Component, OnInit } from '@angular/core';
import {NgForOf} from "@angular/common";
import {LeadService} from "../../../core/services/Leads/lead.service";
import {Lead, LeadType,LeadStatus} from "../../../core/Models/Lead";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";

@Component({
  selector: 'app-pistes',
  standalone: true,
  imports: [NgForOf, ReactiveFormsModule,FormsModule],
  templateUrl: './pistes.component.html',
  styleUrls: ['./pistes.component.css']
})
export class PistesComponent implements OnInit {
  leads: Lead[] = [];  // Array to hold the list of leads
  selectedLead: Lead | null = null;  // For selecting a lead, for editing or viewing details
  errorMessage: string | null = null;
  filteredLeads: any[] = [];

  leadForm: FormGroup;
  LeadType = LeadType;
  LeadStatus = LeadStatus;
  sortOption: string = 'date'; // Default sort option
  searchQuery: string = '';
  constructor(private leadService: LeadService,
              private fb: FormBuilder) {
    this.leadForm = this.fb.group({
      type: [LeadType.Societe, Validators.required],
      denomination: [''],
      civilite: [''],
      nom: [''],
      prenom: [''],
      adresse: [''],
      codePostale: [''],
      ville: [''],
      region: [''],
      pays: [''],
      telephone: [''],
      fax: [''],
      email: [''],
      siteWeb: [''],
      categorie: [''],
      secteurActivite: [''],
      provenance: [''],
      status: [LeadStatus.Nouveau, Validators.required],
      assignedTo: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getAllLeads();
  }
  onSubmit(formValue: Lead) {
    // Handle form submission
    const lead =formValue;
    console.log("Lead :",lead);

    this.createLead(formValue)
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

  getLeadById(id: string): void {
    this.leadService.getLeadById(id).subscribe({
      next: (data) => {
        this.selectedLead = data;
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }

  createLead(newLead: Lead): void {
    this.leadService.createLead(newLead).subscribe({
      next: (data) => {
        this.leads.push(data);
        console.log(data);
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }

  updateLead(updatedLead: Lead): void {
    if (!updatedLead.id) {
      this.errorMessage = 'Lead ID is required for update';
      return;
    }

    this.leadService.updateLead(updatedLead.id, updatedLead).subscribe({
      next: (data) => {
        const index = this.leads.findIndex(l => l.id === data.id);
        if (index !== -1) {
          this.leads[index] = data;
        }
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
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

 gnLead(id: string, collaboratorId: number): void {
    this.leadService.assignLead(id, collaboratorId).subscribe({
      next: () => {
        this.getLeadById(id);  // Refresh the selected lead details after assignment
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }

  startLead(id: string): void {
    this.leadService.startLead(id).subscribe({
      next: () => {
        this.getLeadById(id);  // Refresh the selected lead details after starting the lead
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }

  convertLeadToWon(id: string): void {
    this.leadService.convertLeadToWon(id).subscribe({
      next: () => {
        this.getLeadById(id);  // Refresh the selected lead details after conversion to won
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }

  markLeadAsLost(id: string): void {
    this.leadService.markLeadAsLost(id).subscribe({
      next: () => {
        this.getLeadById(id);  // Refresh the selected lead details after marking as lost
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }

  createEventFromLead(id: string, eventDetails: string): void {
    this.leadService.createEventFromLead(id, eventDetails).subscribe({
      next: () => {
        this.getLeadById(id);  // Refresh the selected lead details after creating the event
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });
  }
}
