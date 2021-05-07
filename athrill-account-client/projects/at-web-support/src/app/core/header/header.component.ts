import { Component, EventEmitter, OnInit, Output, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService, SessionTokenService, ATThemeService } from 'at-services';
import { ATThemeModel, ATThemes } from 'at-assets';

@Component({
  selector: 'at-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() toggleSidebar = new EventEmitter();

  availableThemes: ATThemeModel[] = ATThemes;

  constructor(
    private _router: Router,
    private _sessionTokenService: SessionTokenService,
    private _authService: AuthenticationService,
    private _renderer2: Renderer2,
    private _themeService: ATThemeService
  ) { }

  ngOnInit(): void {
    this._processTheme();
  }

  toggleSidebarNavigation() {
    this.toggleSidebar.emit();
  }

  public signOut(): void {
    this._authService.signOut().subscribe();
    this._sessionTokenService.invalidateSession();
    this._router.navigate(['/auth']);
  }

  public changeTheme(theme: ATThemeModel) {
    this.availableThemes.forEach(x => {
      if (x.themeName.trim() != "") {
        this._renderer2.removeClass(document.body, x.themeName);
      }
    });
    if (theme.themeName.trim() != "") {
      this._renderer2.addClass(document.body, theme.themeName);
    }
    this._themeService.store(theme);
  }

  private _processTheme() {
    let data: ATThemeModel = this._themeService.get();
    if (!data) {
      data = this.availableThemes.find(x => x.isDefault);
    }
    this.changeTheme(data);
  }
}
