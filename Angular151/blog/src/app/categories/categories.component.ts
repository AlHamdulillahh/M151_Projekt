import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from '../models/category';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  public categories: Category[] = [];
  public encoded: any;
  private _http: HttpClient;
  constructor(http: HttpClient) {
this._http = http;
    var token = localStorage.getItem("appToken");

    console.log(jwt_decode(token == null ? "" : token.toString()));

    var encoded = jwt_decode(token == null ? "" : token.toString());
    this.encoded = encoded;
    
    http.get<Category[]>("https://localhost:8000/api/Category").subscribe(res => {
      this.categories = res;
    }, error => console.log(error));
   }

  ngOnInit(): void {
  }

  public async loadCategories() {
    this._http.get<Category[]>("https://localhost:8000/api/Category").subscribe(res => {
      this.categories = res;
    }, error => console.log(error));
  }

  public async deleteCategory(id: number) {
    var token = localStorage.getItem("appToken");
    var headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    await this._http.delete("https://localhost:8000/api/Category/" + id, {headers: headers}).subscribe(res => {
      console.log(res);
    }, error => console.log(error));

    location.reload();
  }

}
