import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="app-container">
      <header>
        <h1>Companies</h1>
      </header>
      <main>
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
  standalone: false,
  styleUrls: ['./app.css']
})
export class App {
  protected readonly title = signal('company-frontend');
}
