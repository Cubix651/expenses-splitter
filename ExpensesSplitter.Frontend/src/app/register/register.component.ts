import { Component, NgModule, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class Register implements OnInit {
  invalidLogin: boolean;
  username: string;
  password: string;
  email: string;
  name: string;
  constructor(private http : HttpClient, private router: Router){ 
  }

  register(form: NgForm){
    
    this.http.get('http://localhost:5000/api/GetUserId') 
    .subscribe(Response => { 
  
      // If response comes hideloader() function is called 
      // to hide that loader  
      console.log(Response)
      Response +=''; 
      const id=Response; 
      console.log(Response);
    this.http.post("http://localhost:5000/api/register", {Id: id, Login: this.username, Password: this.password , Email: this.email, Name: this.name})
    .subscribe(response=> {
      this.router.navigate(["/login"]);
    }, err => {
      this.invalidLogin = true;
    }
    )
    });
  }
  ngOnInit(): void {
    console.log("Register");
  }
  }

