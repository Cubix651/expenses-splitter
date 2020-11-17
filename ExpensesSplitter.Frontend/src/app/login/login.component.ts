import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class Login implements OnInit {
  li:any; 
  lis=[]; 
  constructor(private http : HttpClient){ 
  }

  ngOnInit(): void {
    this.http.get('http://localhost:5000/api/GetAllSettlements') 
    .subscribe(Response => { 
  
      // If response comes hideloader() function is called 
      // to hide that loader  
      console.log(Response) 
      this.li=Response; 
      console.log(this.li) 
      this.lis=this.li.list; 
      console.log(this.lis) 
    }); 
    let list = document
    .getElementById("settlement-list")
    .querySelectorAll('li');

    list.forEach((item, index) => {
    console.log({ index, item })
  });
  }}

