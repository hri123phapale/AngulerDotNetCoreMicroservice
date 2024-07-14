import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/features/auth/models/user.model';
import { AuthService } from 'src/app/features/auth/services/auth.service';

@Component({
  selector: 'app-navabar',
  templateUrl: './navabar.component.html',
  styleUrls: ['./navabar.component.css']
})
export class NavabarComponent implements OnInit {


  user?:User;

  constructor(private authService: AuthService,
    private router:Router
  ){


  }
  ngOnInit(): void {

    this.authService.user().subscribe({ 
      next:(response)=>{
        this.user=response; 
      }
    })

    this.user=this.authService.getuser();
     
  }

  OnLogout() {
    this.authService.logout();
    this.router.navigateByUrl('/');
    }

}
