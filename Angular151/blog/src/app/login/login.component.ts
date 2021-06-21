import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public login: LoginData = {username: '', password: ''};
  private _http: HttpClient;
  public result: any;
  constructor(http: HttpClient) {
    this._http = http;
   }

  ngOnInit(): void {
  }

  public async loginAction() {
    var headers = new HttpHeaders({'Content-Type': 'application/json'});

    await this._http.post("https://localhost:8000/api/Account/Login", {username: 'Admin12345', password: 'sqlPHP3306$'}, {headers: headers})
    .subscribe(res => {
      console.log(res);
      this.result = res;
      if(this.result.token) {
        localStorage.setItem("appToken", this.result.token);
      }
    }, error => console.log(error));
    console.log(this.login.username);
    console.log(this.login.password);
  }

}

interface LoginData {
  username: string,
  password: string
}
