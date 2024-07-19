import { Injectable } from "@angular/core";
import { environment } from "../../../environment/environment";
import { HttpClient } from "@angular/common/http";
import { UserViewModel } from "../models/user.model";

const LOGIN = '/user/Login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private url: string = environment.webApiUserUrl;

  constructor(private http: HttpClient) { }

  login(request: UserViewModel) {
    return this.http.post<UserViewModel>(
      this.url + LOGIN, request
    )
  }

}