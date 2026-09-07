import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-page-header',
  template: `
    <header class="page-header">
      <div>
        <h1 class="h3 page-header__title">
          @if (icon) {
            <i class="fa-solid {{ icon }} me-2" [style.color]="'var(--bq-accent)'"></i>
          }
          {{ title }}
        </h1>
        @if (subtitle) {
          <p class="page-header__subtitle">{{ subtitle }}</p>
        }
      </div>
      <div class="d-flex align-items-center gap-2">
        <ng-content></ng-content>
      </div>
    </header>
  `,
})
export class PageHeader {
  @Input({ required: true }) title = '';
  @Input() subtitle?: string;
  @Input() icon?: string;
}
