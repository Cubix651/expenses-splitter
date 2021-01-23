import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'expenses-splitter';
  isMenuCollapsed = true;
  login = localStorage.getItem('login');
  constructor(private router: Router){
  }
  isUserAuthenticated(){
    const token: string = localStorage.getItem("jwt");
    if(token){
      return true;
    }
    else{
      return false
    }
  }
  logOut(){
    localStorage.removeItem("jwt");
    localStorage.removeItem("login");
    localStorage.removeItem("id");
    window.location.reload();
    this.router.navigate(["/"]);
  }
}
