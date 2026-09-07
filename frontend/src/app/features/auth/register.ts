import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [RouterLink],
  template: `
    <div class="row justify-content-center">
      <div class="col-sm-8 col-md-5 col-lg-4">
        <h1 class="h3 mb-4">Skapa konto</h1>
        <p class="text-body-secondary">Registreringsformuläret byggs i steg 6.</p>
        <p class="mt-3">Har du redan ett konto? <a routerLink="/login">Logga in</a></p>
      </div>
    </div>
  `,
})
export class Register {}
