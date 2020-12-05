import { Component, ElementRef, Input, OnInit, Output } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-settlement-summary',
  templateUrl: './settlement-summary.component.html',
  styleUrls: ['./settlement-summary.component.scss']
})
export class SettlementSummaryComponent implements OnInit {
  @Output() id: string;
  hidden = true;
  name:any;
  li:any; 
  lis=[]; 
  userObject:any;
  users:any;
  constructor(private http : HttpClient, private route: ActivatedRoute, private elem: ElementRef){ 
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('settlementId');
    //this.http.get('http://localhost:5000/api/GetSettlement?id=' + this.id) 
    this.http.get(`${environment.apiUrl}/GetSettlement?id=` + this.id)
    .subscribe(Response => { 
      this.li=Response; 
      this.lis=this.li.list;
      //this.http.get('http://localhost:5000/api/GetSettlementUsers?id=' + this.id)
      this.http.get(`${environment.apiUrl}/GetSettlementUsers?id=` + this.id) 
      .subscribe(Response => { 
      this.users=Response;
    });  
    }); 

  }
  AddUserToSettlement()
  {
    //this.http.post('http://localhost:5000/api/AddUserToSettlement', {UserId: this.name, SettlementId: this.id})
    this.http.post(`${environment.apiUrl}/AddUserToSettlement` , {UserId: this.name, SettlementId: this.id}) 
    .subscribe(Response =>{
      this.ngOnInit();
    })
  }
  hiddenChange():void{
    this.hidden = !this.hidden;
  }

}
