import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users: any;
  constructor(http: HttpClient) {
    var token = localStorage.getItem("appToken");
    var headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    })
    http.get("https://localhost:8000/api/Account/Users", {headers: headers}).subscribe(res => {
      this.users = res;
      console.log(res);
    }, error => console.log(error));

    console.log(localStorage.getItem("appToken"));
   }

  ngOnInit(): void {
  }

}
