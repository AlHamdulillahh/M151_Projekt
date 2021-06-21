import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'blog';
  public encodedToken : any;
  public token: string | null = '';
  public showLoginLink: any;
  private _router: Router;
  public loggedOut: any;

  constructor(router: Router) {
    // localStorage.setItem("appToken", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJlYmNiNWNhNC04ZmNlLTRlOGItYTMxMC04YmIwYWM0Yzg2M2EiLCJVc2VyTmFtZSI6IkFkbWluMTIzNDUiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2MjQyNjUxNTIsImV4cCI6MTYyNDg2OTk1MiwiaWF0IjoxNjI0MjY1MTUyLCJpc3MiOiJCbG9nIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdCJ9.10eaISRK88I-saMuabZ0p5_D3KsF-Yb_T0Q3Hbsl9VI")
    this._router = router;
    // localStorage.setItem("appToken", "");
    var token = localStorage.getItem("appToken");
    this.token = token;
    console.log(token);
    console.log(this.showLoginLink);
    if(token == "" || token == null) {
      console.log("TOKEN");
      this.showLoginLink = true;
    }

    if(token != '') {
      console.log("YSEAH");
      this.showLoginLink = false;
      this.encodedToken = jwt_decode(token == null ? '' : token.toString());
    }
  }

  public async logout() {
    localStorage.setItem("appToken", "");
    this._router.navigate(['login']);
  }
}
