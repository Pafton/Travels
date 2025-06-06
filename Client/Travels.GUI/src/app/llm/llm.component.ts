import { Component, OnInit, inject } from '@angular/core';
import { LlmService } from '../Services/llm.service';
import { MyModel } from '../Model/myModel.model';
import { NavbarComponent } from "../navbar/navbar.component";
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-llm',
  templateUrl: './llm.component.html',
  styleUrls: ['./llm.component.css'],
  imports: [NavbarComponent, NgFor,NgIf]
})
export class LlmComponent implements OnInit {

  private readonly modelLlmService = inject(LlmService);

  list: MyModel[] = [];
  errorMessage: string = '';

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.modelLlmService.getList().subscribe({
      next: data => {
        this.list = data;
      },
      error: err => {
        this.errorMessage = 'Błąd podczas pobierania danych: ' + (err.message || err.statusText || 'Nieznany błąd');
      }
    });
  }
  
}
