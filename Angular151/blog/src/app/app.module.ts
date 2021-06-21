import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PostsComponent } from './posts/posts.component';
import { CreateComponent as CreatePost } from './posts/create/create.component';
import { LoginComponent } from './login/login.component';
import { CategoriesComponent } from './categories/categories.component';
import { CreateComponent as CreateCategory } from './categories/create/create.component';
import { UsersComponent } from './users/users.component';
import { DetailsComponent as PostDetails } from './posts/details/details.component';
import { EditComponent } from './posts/edit/edit.component';
import { SearchComponent } from './posts/search/search.component';

@NgModule({
  declarations: [
    AppComponent,
    PostsComponent,
    CreatePost,
    LoginComponent,
    CategoriesComponent,
    CreateCategory,
    UsersComponent,
    PostDetails,
    EditComponent,
    SearchComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'posts', component: PostsComponent },
      { path: 'login', component: LoginComponent},
      { path: 'new-post', component: CreatePost},
      { path: 'categories', component: CategoriesComponent},
      { path: 'new-category', component: CreateCategory},
      { path: 'users', component: UsersComponent},
      { path: 'post/:id', component: PostDetails},
      { path: 'post/edit/:id', component: EditComponent},
      { path: 'search-posts', component: SearchComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
