import { Component } from '@angular/core';
import { faLinkedin, faTwitter, faFacebook, faGooglePlus, faInstagram, faGithub } from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent {
  title = 'app';
  faLinkedin = faLinkedin;
  faTwitter = faTwitter;
  faFacebook = faFacebook;
  faGoooglePlus = faGooglePlus;
  faInstagram = faInstagram;
  faGithub = faGithub;
  
}






