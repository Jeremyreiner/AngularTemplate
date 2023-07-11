import {Component, Inject} from '@angular/core';
import {DOCUMENT} from '@angular/common';
import {Constants} from '../../models/Constants';

@Component({
  selector: 'app-theme-switch',
  templateUrl: './theme-switch.component.html',
  styleUrls: ['./theme-switch.component.scss']
})
export class ThemeSwitchComponent {
  private static readonly DARK_THEME_CLASS = 'dark-theme';
  private static readonly DARK_THEME_LIGHT = 'light';
  public static readonly DARK_THEME_DARK = 'dark';

  public static theme: string;


  constructor(@Inject(DOCUMENT) private document: Document) {
    ThemeSwitchComponent.theme = this.document.documentElement.classList.contains(ThemeSwitchComponent.DARK_THEME_CLASS) ? ThemeSwitchComponent.DARK_THEME_DARK : ThemeSwitchComponent.DARK_THEME_LIGHT;
  }

  public selectDarkTheme(): void {
    this.document.documentElement.classList.add(ThemeSwitchComponent.DARK_THEME_CLASS);
    ThemeSwitchComponent.theme = ThemeSwitchComponent.DARK_THEME_DARK;
  }

  public selectLightTheme(): void {
    this.document.documentElement.classList.remove(ThemeSwitchComponent.DARK_THEME_CLASS);
    ThemeSwitchComponent.theme = ThemeSwitchComponent.DARK_THEME_LIGHT;
  }


  switchTheme() {
    if (ThemeSwitchComponent.theme === ThemeSwitchComponent.DARK_THEME_LIGHT) {
      this.selectDarkTheme();
    } else {
      this.selectLightTheme();
    }
  }


  getTheme() {
    return ThemeSwitchComponent.theme;
  }

  protected readonly Constants = Constants;
}
