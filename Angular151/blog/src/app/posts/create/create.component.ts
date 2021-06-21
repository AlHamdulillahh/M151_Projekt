import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from 'src/app/models/category';
import { Post } from 'src/app/models/post';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  // Bearer token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiO…zdCJ9.OOHBZulFTEhjxuqTBIud6uzieRvTy832BsUNn-CdSaY
  private _http: HttpClient;
  public categories: Category[] = [];
  public post: Post = {id: 0, title: '', body: '', categoryId: 1, userId: ""};
  constructor(http: HttpClient) { 
    this._http = http;
    http.get<Category[]>("https://localhost:8000/api/Category").subscribe(res => {
      this.categories = res;
    }, error => console.log(error));
  }

  ngOnInit(): void {
  }

  public async createPost() {
    var authToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiO…zdCJ9.OOHBZulFTEhjxuqTBIud6uzieRvTy832BsUNn-CdSaY';
    var token = localStorage.getItem("appToken");
    var encodedToken: any;
    encodedToken = jwt_decode(token == null ? "" : token.toString());
    console.log(encodedToken.UserId);
    var headers = new HttpHeaders({
      'Content-Type' : 'application/json',
      'Authorization': `Bearer ${token}`
    })
    await this._http.post("https://localhost:8000/api/Post", {id: 0, title: this.post.title, body: this.post.body, categoryId: this.post.categoryId, userId: encodedToken.UserId}, {headers: headers}).subscribe(res => {
      console.log(res);
    }, error => console.log(error));
  }

}
