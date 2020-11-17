import { Component, Input, OnInit, Output } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-settlement-summary',
  templateUrl: './settlement-summary.component.html',
  styleUrls: ['./settlement-summary.component.scss']
})
export class SettlementSummaryComponent implements OnInit {
  @Output() id: string;
  li:any; 
  lis=[]; 
  constructor(private http : HttpClient, private route: ActivatedRoute){ 
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    console.log(this.id)
    this.http.get('http://localhost:5000/api/GetSettlement?id=' + this.id) 
    .subscribe(Response => { 
  
      // If response comes hideloader() function is called 
      // to hide that loader  
      console.log(Response) 
      this.li=Response; 
      console.log(this.li) 
      this.lis=this.li.list; 
    }); 

  }

}
