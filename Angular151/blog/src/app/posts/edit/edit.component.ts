import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { Category } from 'src/app/models/category';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  private _http: HttpClient;
  private _route: ActivatedRoute;
  private _id: number = 0;
  public categories: Category[] = [];
  public post: Post = {id: 0, title: '', body: '', categoryId: 0, userId: ""};
  constructor(http: HttpClient, route: ActivatedRoute) {
    this._http = http;
    this._route = route;

    route.params.subscribe(params => {
      this._id = params['id'];
    });

    http.get<Category[]>("https://localhost:8000/api/Category").subscribe(result => {
      this.categories = result;
    }, error => console.log(error));

    http.get<Post>("https://localhost:8000/api/Post/" + this._id).subscribe(result => {
      this.post = result;
    });

   }

   public async edit(id: number) {
     var token = localStorage.getItem("appToken");
     var headers = new HttpHeaders({
       'Content-Type': 'application/json',
       'Authorization': `Bearer ${token}`
     });

     var post = {
       id: this.post.id,
       title: this.post.title,
       body: this.post.body,
       categoryId: this.post.categoryId,
       userId: this.post.userId
     };
     await this._http.put("https://localhost:8000/api/Post/" + id, post, {headers: headers}).subscribe(result => {
       console.log(result);
     }, error => console.log(error));
   }

  ngOnInit(): void {
  }

}
