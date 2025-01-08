import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MsalModule } from '@azure/msal-angular';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    MsalModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'azure-kv-client';
}
