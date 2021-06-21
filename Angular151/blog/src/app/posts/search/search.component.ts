import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  
  public input: search = {term: ''};
  private _http: HttpClient;
  public posts: any;
  constructor(http: HttpClient) { 
    this._http = http;

    http.get("https://localhost:8000/api/Post").subscribe(result => {
      this.posts = result;
    }, error => console.log(error));
  }

  ngOnInit(): void {
  }

  public async search() {
    await this._http.get("https://localhost:8000/api/Post/?query=" + this.input.term).subscribe(result => {
      this.posts = result;
    }, error => console.log(error));
  }

}

interface search {
  term: string;
}
