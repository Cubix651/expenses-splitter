import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Component({
  selector: 'app-settlement-list',
  templateUrl: './settlement-list.component.html',
  styleUrls: ['./settlement-list.component.scss']
})
export class SettlementListComponent implements OnInit {
  li:any;
  user = localStorage.getItem("id"); 
  lis=[]; 
  constructor(private http : HttpClient){ 
  }

  ngOnInit(): void {
    if(localStorage.getItem("id") != null){
      //this.http.get('http://localhost:5000/api/GetAllSettlementsByUser?id=' + localStorage.getItem("id"))
      this.http.get(`${environment.apiUrl}/GetAllSettlementsByUserParticipant?id=` + localStorage.getItem("id"))  
      .subscribe(Response => { 
        console.log(Response);
        // If response comes hideloader() function is called 
        // to hide that loader  
        this.li=Response; 
        this.lis=this.li.list; 
      }); 
      let list = document
      .getElementById("settlement-list")
      .querySelectorAll('li');
  
      list.forEach((item, index) => {
      console.log({ index, item })
    });
    }
    else{
    //this.http.get('http://localhost:5000/api/GetAllSettlements')
    this.http.get(`${environment.apiUrl}/GetAllSettlements`)  
    .subscribe(Response => { 
  
      // If response comes hideloader() function is called 
      // to hide that loader  
      this.li=Response; 
      this.lis=this.li.list; 
    }); 
    let list = document
    .getElementById("settlement-list")
    .querySelectorAll('li');

    list.forEach((item, index) => {
    console.log({ index, item })
  });
  }
  }}


