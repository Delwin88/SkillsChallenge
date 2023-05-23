import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from '../table/table.component';
import { PeopleService } from './people.service';
import { TableData } from '../table-data';
import { QuickSearchDirective } from '../quick-search.directive';

@Component({
  selector: 'app-people',
  standalone: true,
  imports: [CommonModule, TableComponent, QuickSearchDirective],
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {
  isLoading = true;
  rows!: { [key: string]: any }[];
  readonly columns: TableData[] = [
    {
      Header: 'First Name',
      PropName: 'firstName'
    },
    {
      Header: 'Last Name',
      PropName: 'lastName'
    },
    {
      Header: 'Birthday',
      PropName: 'birthday'
    }
  ];

  constructor(private service: PeopleService, private cd: ChangeDetectorRef) {

  }

  ngOnInit() {
    this.search();
  }

  Add() {
    let input: any[] = [];
    let value = { "Id": 2, "FirstName": "Jim", "LastName": "Parsons", "Birthday": "1977-02-20T00:00:00" }
    input.push(value);
    this.service.add(input).subscribe(value => {
      this.isLoading = false;
      this.search();
      this.cd.detectChanges();
    });
  }

  search(filter = '') {
    this.service.fetch(filter).subscribe(value => {
      this.rows = value;
      this.isLoading = false;
      this.cd.detectChanges();
    });
  }

}
