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
      this.http.get(`${environment.apiUrl}/GetAllSettlementsByUserParticipant?id=` + localStorage.getItem("id"))  
      .subscribe(Response => { 
        this.li=Response; 
        this.lis=this.li.list; 
      }); 
    }
    else{
    this.http.get(`${environment.apiUrl}/GetAllSettlements`)  
    .subscribe(Response => { 
      this.li=Response; 
      this.lis=this.li.list; 
    }); 
  }
  }}


