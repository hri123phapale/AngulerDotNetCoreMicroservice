import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
 
  form: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('') 
  });
  submitted = false;
 

 constructor(private authService:AuthService,
  private cookieService:CookieService,
  private router:Router,
  private formBuilder :FormBuilder
 ){ 
 }
 get f(): { [key: string]: AbstractControl  } {
  return this.form.controls;
}
  ngOnInit(): void {
     this.form=this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(40)
          ]
        ],
      }
     )
  }

 OnLoginSubmit() {
  if(!this.form.invalid)
  {
    return;
  }
  let requestModel={
    email:this.form.value.email,
    password:this.form.value.password
  }
  this.authService.loginResuest(requestModel).subscribe(
    {
      // set auth cookier
      next:(response)=>{
        this.cookieService.set('Authorization',`Bearer ${response.token}`,
          undefined,'/',undefined,true,'Strict'); 
            
          //set user
          this.authService.setuser({
            email:response.email,
            roles:response.roles
          });

          //redirect to home
          this.router.navigateByUrl('/');
      }

    }
  )

  }
}
