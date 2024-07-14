import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../auth/services/auth.service';
import { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService=inject(CookieService);
  const authService=inject(AuthService);
  const router=inject(Router); 
  //check for jwt token
  let token=cookieService.get('Authorization');
  let user =authService.getuser(); 
  if(token && user)
  { 
    token=token.replace('Bearer','');
    const decodedToken : any =jwtDecode(token);
    const expiryDate=decodedToken.exp * 1000;
    const currentTime=new Date().getTime();
    
    if(expiryDate < currentTime)
    { 
      authService.logout();
      return router.createUrlTree(['/login'],{queryParams:{ returnUrl:state.url}});
    }else
    {
         
         if(user.roles.includes('Writer'))
         {
          return true;
         }else
         {
          
           alert("Unauthorized");
           return false;
         }
    }

  }else
  { 
    authService.logout();
    return router.createUrlTree(['/login'],{queryParams:{ returnUrl:state.url}});
  } 
};
