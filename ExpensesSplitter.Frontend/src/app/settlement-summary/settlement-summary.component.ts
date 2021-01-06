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
  friends:any;
  friendsToAddList: any;
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
    this.friendsToAddList = this.friendsToAddList || [];
    this.friends = this.friends || [];
    var friendsTemp: any;
    this.http.get(`${environment.apiUrl}/GetSettlement?id=` + this.id)
    .subscribe(Response => { 
      this.li=Response; 
      this.lis=this.li.list;
      this.http.get(`${environment.apiUrl}/GetSettlementUsers?id=` + this.id) 
      .subscribe(Response => { 
      this.users=Response;
      console.log(this.users);
      this.http.get(`${environment.apiUrl}/friends/get?id=` + localStorage.getItem("id"))
      .subscribe(Response => {
      if(Response != null){
      console.log(Response)
      this.friends = Response;
      this.friends.forEach(friend => {
        this.users.forEach(user => {
          console.log(user);
          console.log(friend);
          if(friend.id == user.id)
          {
            this.friends = this.friends.filter(e => e !== friend)
            
          }
        });
      });
      }  
      });
      this.http.get(`${environment.apiUrl}/GetSettlementUsersWithoutAccount?id=` + this.id) 
      .subscribe(Response => { 
        if(Response != null){
      this.users= this.users.concat(Response);
      console.log(this.users);
        }
    }, error => {
      console.log(error);
    });  
    });  
    this.http.get(`${environment.apiUrl}/GetRole?user=` + localStorage.getItem("id") + "&settlement=" + this.id) 
    .subscribe(Response => { 
    this.checkRole=Response['roleId'];
  });  
  }); 
  this.http.get(`${environment.apiUrl}/group/get?id=` + this.id) 
  .subscribe(Response => { 
  if(Response != null){
 this.groups=Response;
 this.groups.push({id: "00000000-0000-0000-0000-000000000000", name: "Indywidualna"})
}
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
  addToList(person: any)
  {
    var settlementUser: any = {
      displayName: person.login,
      settlementId: this.id,
      userId: person.id,
    }
    //this.friendsToAddList.push(person);
    if(!this.friendsToAddList.some(e => e.id == person.id)){
    //this.friendsToAddList.push({displayName: person.login, settlementId: this.id, userId: person.id, roleId: 1});
    this.friendsToAddList.push(person);
    //this.friendsToAddList.includes({displayName: person.login, settlementId: this.id, userId: person.id, roleId: 1, groupId: "00000000-0000-0000-0000-000000000000"});
    }
    //console.log(this.friendsToAddList.some(e => e.userId == person.id));
  }
  removeFromList(person: any)
  {
    var settlementUser: any = {
      displayName: person.login,
      settlementId: this.id,
      userId: person.id,
    }
    //this.removeAllElements(this.friendsToAddList, settlementUser)
    //this.friendsToAddList = this.friendsToAddList.filter(e => e !== {displayName: person.login, settlementId: this.id, userId: person.id, roleId: 1})
    this.friendsToAddList = this.friendsToAddList.filter(e => e !== person)
    console.log(this.friendsToAddList);
  }
  addFriendsToSettlment(){
    var settlementUsers = settlementUsers || [];
    this.friendsToAddList.forEach(element => {
      settlementUsers.push({
        displayName: element.login,
        settlementId: this.id,
        userId: element.id
      })
    });
    this.http.post(`${environment.apiUrl}/AddFriendsToSettlement` , settlementUsers) 
    .subscribe(Response =>{
      this.hiddenUser = !this.hiddenUser;
      this.friendsToAddList = []
      this.ngOnInit();
    })
    //console.log(settlementUsers);
  }
}
