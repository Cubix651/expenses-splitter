import { Component, NgModule, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class Register implements OnInit {
  private readonly apiUrl = environment.apiUrl;

  invalidLogin: boolean;
  username: string;
  password: string;
  email: string;
  name: string;
  constructor(private http : HttpClient, private router: Router){
  }

  register(form: NgForm){

    this.http.get(`${this.apiUrl}/GetUserId`)
    .subscribe(Response => {
      console.log(Response)
      Response +='';
      const id=Response;
      console.log(Response);
    this.http.post(`${this.apiUrl}/register`,  {Id: id, Login: this.username, Password: this.password , Email: this.email, Name: this.name})
    .subscribe(response=> {
      this.router.navigate(["/login"]);
    }, err => {
      this.invalidLogin = true;
    }
    )
    });
  }
  ngOnInit(): void {
  }
  }

