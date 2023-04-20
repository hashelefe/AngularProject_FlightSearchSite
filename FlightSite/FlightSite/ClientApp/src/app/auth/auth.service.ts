import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }
  currentUser?: User

  loginUser(user: User) {
    this.currentUser = user
    console.log("Log in the user with email "+user.email)
  }
}

interface User {
  email: string
}
