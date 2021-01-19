import { Component, NgModule, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.scss']
})
export class Friends implements OnInit {
  friends:any;
  name: any;
  friendId: any;
  hidden = true;
  constructor(private http : HttpClient, private router: Router){ 
  }
  ngOnInit(): void {
    this.http.get(`${environment.apiUrl}/friends/get?id=` + localStorage.getItem("id"))
    .subscribe(Response => {
      if(Response != null){
      this.friends = Response;
      }
    })

  }
  AddFriend()
  {
    this.http.post(`${environment.apiUrl}/friends/add` , {userId: localStorage.getItem("id"), name: this.name})
    //this.http.get(`${environment.apiUrl}/friends/add?id=` + localStorage.getItem("id") + "&name=" + this.name)  
    .subscribe(Response =>{
      this.hidden = !this.hidden;
      this.ngOnInit();
    })
  }
  RemoveFriend(friend: any)
  {
    this.http.delete(`${environment.apiUrl}/friends/remove?id=` + localStorage.getItem("id") + "&friend=" + friend) 
    .subscribe(Response =>{
      this.ngOnInit();
    })
  }
  hiddenChange():void{
    this.hidden = !this.hidden;
  }
  }

