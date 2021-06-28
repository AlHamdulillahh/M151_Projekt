import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { Comment } from 'src/app/models/comment';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  public post: any = {};
  private _id: number = 0;
  private _http: HttpClient;
  public noComments: any;
  public encodedToken: any;
  public date: Date = new Date();
  public token: string | null = '';
  public comment: Comment = {id: 0, body: '', postId: 0, userId: ''};
  constructor(http: HttpClient, route: ActivatedRoute) {
    this._http = http;
    route.params.subscribe(params => {
      this._id = params['id'];
    });



    var token = localStorage.getItem("appToken");
    this.token = token;

    http.get("https://localhost:8000/api/Post/" + this._id).subscribe(result => {
      this.post = result;
      this.post.createdAt = new Date(this.post.createdAt);
    }, error => console.log(error));

   }

   public async writeComment(id: number) {
     var token = localStorage.getItem("appToken");
     var headers = new HttpHeaders({
       'Content-Type': 'application/json',
       'Authorization': `Bearer ${token}`
     });

     await this._http.post("https://localhost:8000/api/Comment/" + this._id, {id: 0, body: this.comment.body, postId: this._id}, {headers: headers}).subscribe(result => {
       console.log(result);
     }, error => console.log(error));
     location.reload();
   }

  ngOnInit(): void {
  }

}
