import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-new-settlement',
  templateUrl: './new-settlement.component.html',
  styleUrls: ['./new-settlement.component.scss']
})
export class NewSettlementComponent implements OnInit {
  name: string;
  description: string;
  id:string

  constructor(private http : HttpClient) { }

  ngOnInit(): void {

  }
  createSettlement(){
    this.http.get('http://localhost:5000/api/GetId') 
    .subscribe(Response => { 
  
      // If response comes hideloader() function is called 
      // to hide that loader  
      console.log(Response)
      Response +=''; 
      const id=Response; 

    const headers = new Headers;
    headers.append('Access-Control-Allow-Origin', '*');
    const name = this.name
    const description = this.description
    this.http.post('http://localhost:5000/api/AddSettlement', { id: id, name: name, description: description },  {
    
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Access-Control-Allow-Origin': 'http://localhost:4200',
        "Access-Control-Allow-Credentials": "true",
        "Access-Control-Allow-Methods": "GET,HEAD,OPTIONS,POST,PUT",

        Authorization: 'my-auth-token',
        mode: 'no-cors'
      })  }).subscribe(data => {    
  })
}); 
  }

}
