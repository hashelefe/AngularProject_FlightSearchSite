import { Component } from '@angular/core';
import { PassengerService } from './../api/services/passenger.service';
import { FormBuilder } from '@angular/forms'
import { AuthService } from './../auth/auth.service'
import { Router, ActivatedRoute } from '@angular/router';
import { Validators } from '@angular/forms'

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent {
  constructor(private passengerService: PassengerService, private fb: FormBuilder, private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) { }

  requestedUrl?: string = undefined
  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.min(3), Validators.max(254)])],
    firstName: ['', Validators.compose([Validators.required, Validators.min(2), Validators.max(254)])],
    lastName: ['', Validators.compose([Validators.required, Validators.min(3), Validators.max(254)])],
    isFemale: [true, Validators.required]

  })

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl'])
  }



  register() {
    if (this.form.invalid)
      return;

    console.log("Form Values: ", this.form.value);
    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(this.login, console.error)
  }
  checkPassenger(): void {
    const params = {
      email: this.form.get('email')?.value
    }
    this.passengerService
      .findPassenger(params)
      .subscribe(this.login, e => {
        if (e.status != 404)
          console.error(e)
      })
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value })
    this.router.navigate([this.requestedUrl ?? '/search-flights'])
  }
}
