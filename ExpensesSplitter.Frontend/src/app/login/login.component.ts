import { Component, NgModule, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class Login implements OnInit {
  li:any; 
  lis=[]; 
  invalidLogin: boolean;
  username: string;
  password: string;
  constructor(private http : HttpClient, private router: Router){ 
  }

  login(form: NgForm){
    const credentials ={
      'Login': form.value.username,
      'Password': form.value.password
    }
    this.http.post("http://localhost:5000/api/login", {Login: this.username, Password: this.password })
    .subscribe(response=> {
      const token =(<any>response).token;
      localStorage.setItem("jwt",token);
      this.invalidLogin = false;
      this.router.navigate(["/"]);
    }, err => {
      this.invalidLogin = true;
    }
    )
    console.log(localStorage)
  }
  ngOnInit(): void {
    console.log("Login");
  }
  }

