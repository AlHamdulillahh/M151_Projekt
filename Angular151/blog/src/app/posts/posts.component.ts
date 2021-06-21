import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
public posts: any;
public encodedToken: any;
public token1: string | null = '';
private _http: HttpClient;
  constructor(http: HttpClient) {
    this._http = http;
    var token = localStorage.getItem("appToken");
    this.token1 = token;
    this.encodedToken = jwt_decode(token == null ? "" : token.toString());
    var headers = new HttpHeaders({
      'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIyN2E3OTgwYS1iZTdhLTQ2MjAtYTE1MS0wNDhmYmViMDNjM2MiLCJVc2VyTmFtZSI6IkFkbWluMTIzNDUiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2MjQwMDUyMzQsImV4cCI6MTYyNDYxMDAzNCwiaWF0IjoxNjI0MDA1MjM0LCJpc3MiOiJCbG9nIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdCJ9.tADB43nhQOe5OP0t-yDctCA5jRRaRMRCKSW9BG6t-eA'
    })
http.get("https://localhost:8000/api/Post", {headers: headers}).subscribe(result => {
  this.posts = result;
}, error => console.log(error));
   }

   public async delete(id: number) {
     var token = localStorage.getItem("appToken");
     var headers = new HttpHeaders({
       'Authorization': `Bearer ${token}`
     });

     await this._http.delete("https://localhost:8000/api/Post/" + id, {headers: headers}).subscribe(res => {
       console.log(res);
     }, error => console.log(error));
     console.log(id);
   }

  ngOnInit(): void {
  }

}
