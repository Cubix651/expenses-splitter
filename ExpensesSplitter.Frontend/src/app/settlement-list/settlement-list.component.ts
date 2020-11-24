import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
@Component({
  selector: 'app-settlement-list',
  templateUrl: './settlement-list.component.html',
  styleUrls: ['./settlement-list.component.scss']
})
export class SettlementListComponent implements OnInit {
  li:any; 
  lis=[]; 
  constructor(private http : HttpClient){ 
  }

  ngOnInit(): void {
    if(localStorage.getItem("id") != null){
      this.http.get('http://localhost:5000/api/GetAllSettlementsByUser?id=' + localStorage.getItem("id")) 
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
    else{
    this.http.get('http://localhost:5000/api/GetAllSettlements') 
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


