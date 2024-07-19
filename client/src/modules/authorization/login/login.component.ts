import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule, NgStyle } from '@angular/common';
import { IconDirective } from '@coreui/icons-angular';
import {
  ContainerComponent, RowComponent, ColComponent, CardGroupComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective,
  InputGroupComponent, InputGroupTextDirective, FormControlDirective, ButtonDirective
} from '@coreui/angular';
import { FormsModule, FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { LoginService } from '../../../app/shared/services/login.service';
import { UserViewModel } from '../../../app/shared/models/user.model';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    ContainerComponent,
    RowComponent,
    ColComponent,
    CardGroupComponent,
    TextColorDirective,
    CardComponent,
    CardBodyComponent,
    FormDirective,
    InputGroupComponent,
    InputGroupTextDirective,
    IconDirective,
    FormControlDirective,
    ButtonDirective,
    NgStyle,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    NgxUiLoaderModule
  ]
})
export class LoginComponent implements OnInit, OnDestroy {
  // subs
  subscriptions: Subscription[] = [];
  
  loginForm!: FormGroup;
  loginFailed: boolean = false;
  showPassword: boolean = false;

  constructor(private formBuilder: FormBuilder, private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.loginFailed = false; //Reset the state of the failed login message when initializing the component
  }

  onLoginSubmit(): void {
    if (this.loginForm.valid) {
      const username = this.loginForm.value.username;
      const password = this.loginForm.value.password;
      const loginPost = <UserViewModel>{
        username: username,
        password: password
      }
      this.subscriptions.push(this.loginService.login(loginPost)
        .subscribe(
          response => {
            if (response) {
              this.loginFailed = false;
              this.loginForm.reset(); // Reset the form after successful login
              sessionStorage.setItem('userId', response.id.toString());
              sessionStorage.setItem('firstname', response.firstname.toString());
              sessionStorage.setItem('lastname', response.lastname.toString());
              this.router.navigate(['/layout']); // Redirect to LayoutComponent
            } else {
              this.loginFailed = true;
            }
          },
          error => {
            console.error('Error during login:', error);
            this.loginFailed = true;
          }
        ));
    }
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => {
      if (sub && !sub.closed) {
        sub.unsubscribe();
      }
    });
  }
}