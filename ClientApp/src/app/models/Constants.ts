import {ThemeSwitchComponent} from '../components/theme-switch/theme-switch.component';

export class Constants {
  static readonly guidNull = '00000000-0000-0000-0000-000000000000';

  static color(flip: boolean = false): string {
    const isDarkTheme = ThemeSwitchComponent.theme === ThemeSwitchComponent.DARK_THEME_DARK;
    return flip ? (isDarkTheme ? 'white' : 'black') : (isDarkTheme ? 'black' : 'white');
  }

  static darkMode(): boolean {
    return ThemeSwitchComponent.theme === ThemeSwitchComponent.DARK_THEME_DARK;
  }


  static getEnumValues(enumObject: any): string[] {
    return Object.keys(enumObject)
      .filter(key => isNaN(Number(enumObject[key])))
      .map(key => enumObject[key]);
  }
}


export class Result<T> {
  isSuccess: boolean;
  status: number;
  value: T;
  error: string;
}
