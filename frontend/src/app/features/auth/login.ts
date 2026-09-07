import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [RouterLink],
  template: `
    <div class="row justify-content-center">
      <div class="col-sm-8 col-md-5 col-lg-4">
        <h1 class="h3 mb-4">Logga in</h1>
        <p class="text-body-secondary">Inloggningsformuläret byggs i steg 6.</p>
        <p class="mt-3">Ny här? <a routerLink="/register">Skapa konto</a></p>
      </div>
    </div>
  `,
})
export class Login {}
