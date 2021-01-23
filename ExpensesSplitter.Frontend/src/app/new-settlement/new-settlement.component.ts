import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-new-settlement',
  templateUrl: './new-settlement.component.html',
  styleUrls: ['./new-settlement.component.scss']
})
export class NewSettlementComponent implements OnInit {
  private readonly apiUrl = environment.apiUrl;

  name: string;
  description: string;
  id:string
  constructor(private http : HttpClient, private router: Router) { }

  ngOnInit(): void {
    console.log(localStorage.getItem("id"));
  }
  createSettlement(){
    this.http.get(`${this.apiUrl}/GetId`)
    .subscribe(Response => {
      Response +='';
      const id=Response;

    const headers = new Headers;
    headers.append('Access-Control-Allow-Origin', '*');
    const name = this.name
    const description = this.description
    if(localStorage.getItem('id') != null){
      const owner = localStorage.getItem("id").toString
      this.http.post(`${this.apiUrl}/AddSettlement`, { id: id, name: name, description: description, IdOwner: localStorage.getItem("id")}).subscribe(data => {
      this.router.navigate(["/"]);

      })
    }
    else{
    this.http.post(`${this.apiUrl}/AddSettlement`, { id: id, name: name, description: description }).subscribe(data => {
      this.router.navigate(["/"]);
    })
  }
});
  }

}
