import { Component, NgModule, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { environment } from 'src/environments/environment';

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
    this.http.post(`${environment.apiUrl}/login`,  {Login: this.username, Password: this.password })  
    .subscribe(response=> {
      localStorage.setItem("login", (<any>response).login);
      localStorage.setItem("id", (<any>response).id);
      this.invalidLogin = false;
      this.router.navigate(["/"]);
      window.location.reload();
      
    }, err => {
      this.invalidLogin = true;
    }
    )
  }
  ngOnInit(): void {
    if(localStorage.getItem('login') != null)
    {
      this.router.navigate(["/"]);
    }
  }
  }

