import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Category } from 'src/app/models/category';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  private _http: HttpClient;
  public category: Category = {id: 0, name: ''};
  constructor(http: HttpClient) {
    this._http = http;
   }

   public async createCategory() {
     var token = localStorage.getItem("appToken");
     var headers = new HttpHeaders({
       'Content-Type': 'application/json',
       'Authorization': `Bearer ${token}`
     });
     console.log("Y");
 await this._http.post("https://localhost:8000/api/Category",{ name: this.category.name}, {headers: headers}).subscribe(res => {
  console.log(res);
}, error => console.log(error));
   }

  ngOnInit(): void {
  }

}
