import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Expenses} from './expenses.model';

@Component({
  selector: 'expenses',
  templateUrl: './expenses.component.html'
})
export class ExpensesComponent implements OnInit {
  public expenses: Expenses[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<Expenses[]>(apiUrl + 'expenses').subscribe(result => {
      this.expenses = result;
    }, error => console.error(error));
  }
  ngOnInit() {

  }
}




