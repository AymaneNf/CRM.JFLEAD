import {Component, ElementRef, HostListener, ViewChild} from '@angular/core';
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css'
})
export class ContactsComponent {
  @ViewChild('searchInput') searchInput!:ElementRef;


  @HostListener('window:keydown',['$event'])
  handleKeyboardEvent(event:KeyboardEvent){
    if (event.ctrlKey && event.key === 'q') {
      event.preventDefault(); // Prevent default behavior for Ctrl+Q
      this.focusSearchInput();
    }
  }
  focusSearchInput() {
    this.searchInput.nativeElement.focus();
  }
}
