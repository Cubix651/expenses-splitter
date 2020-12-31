import { Component, ElementRef, Input, OnInit, Output } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import {SettlementUsersService} from '../services/settlement-users.service'
import { NewSettlementUser } from '../models/settlement-user.model';

enum role {
  Admin =  0,
  Watcher = 1,
  Editor = 2
}

@Component({
  selector: 'app-settlement-summary',
  templateUrl: './settlement-summary.component.html',
  styleUrls: ['./settlement-summary.component.scss'],
  providers: [SettlementUsersService]
})
export class SettlementSummaryComponent implements OnInit {
  @Output() id: string;
  settlementUser: NewSettlementUser = {
    displayName: '',
    settlementId: '',
    userId: '',
    roleId: role.Watcher,
    groupId: "00000000-0000-0000-0000-000000000000"
  }
  hidden = true;
  hiddenUser = true;
  hiddenGroup= true;
  groupName: any;
  displayName: any;
  name:any;
  li:any; 
  lis=[]; 
  userObject:any;
  users:any;
  checkRole:any;
  role: role;
  groups: any;
  userRole:{
    0:"Admin",
    1:"Watcher",
    2:"Editor"
  }
  constructor(private http : HttpClient, private route: ActivatedRoute, private elem: ElementRef, private readonly service: SettlementUsersService){ 
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('settlementId');
    this.http.get(`${environment.apiUrl}/GetSettlement?id=` + this.id)
    .subscribe(Response => { 
      this.li=Response; 
      this.lis=this.li.list;
      this.http.get(`${environment.apiUrl}/GetSettlementUsers?id=` + this.id) 
      .subscribe(Response => { 
      this.users=Response;
      this.http.get(`${environment.apiUrl}/GetSettlementUsersWithoutAccount?id=` + this.id) 
      .subscribe(Response => { 
        if(Response != null){
      this.users= this.users.concat(Response);
        }
    }, error => {
      console.log(error);
    });  
    });  
    this.http.get(`${environment.apiUrl}/GetRole?user=` + localStorage.getItem("id") + "&settlement=" + this.id) 
    .subscribe(Response => { 
    this.checkRole=Response['roleId'];
  });  
  this.http.get(`${environment.apiUrl}/group/get?id=` + this.id) 
  .subscribe(Response => { 
  if(Response != null){
     this.groups=Response;
     this.groups.push({id: "00000000-0000-0000-0000-000000000000", name: "Indywidualna"})
  }
  })
    }); 

  }
  AddUserToSettlement()
  {
    this.http.post(`${environment.apiUrl}/AddUserToSettlement` , {UserId: this.name, SettlementId: this.id}) 
    .subscribe(Response =>{
      this.hidden = !this.hidden;
      this.ngOnInit();
    })
  }
  AddUserWithoutAccountToSettlement()
  {
    this.http.post(`${environment.apiUrl}/AddUserWithoutAccountToSettlement` , {DisplayName: this.displayName, SettlementId: this.id}) 
    .subscribe(Response =>{
      this.hiddenUser = !this.hiddenUser;
      this.ngOnInit();
    })
  }
  addUser() {
    this.settlementUser.roleId = "Watcher";
    this.service.addUser(this.id, this.settlementUser)
      .subscribe(Response => {
        this.hiddenUser = !this.hiddenUser;
        this.ngOnInit();
      });
  }

  AddGroup()
  {
    this.http.post(`${environment.apiUrl}/group/add` , {Name: this.groupName, SettlementId: this.id}) 
    .subscribe(Response =>{
      this.hiddenGroup = !this.hiddenGroup;
      this.ngOnInit();
    })
  }
  RemoveUserFromSettlement(user: any, display: any)
  {
    this.http.delete(`${environment.apiUrl}/RemoveUserFromSettlement?settlementId=` + this.id + "&userId=" +  user + "&displayname=" + display) 
    .subscribe(Response =>{
      this.ngOnInit();
    })
  }
  hiddenChange():void{
    this.hidden = !this.hidden;
  }
  hiddenUserChange():void{
    this.hiddenUser = !this.hiddenUser;
  }
  hiddenGroupChange():void{
    this.hiddenGroup = !this.hiddenGroup;
  }
  onChangeGroup(id: any, newValue: any) {
    this.http.put(`${environment.apiUrl}/ChangeGroup`, {Id: id, GroupId: newValue}) 
    .subscribe(Response =>{
      this.ngOnInit();
    })
  }

}
