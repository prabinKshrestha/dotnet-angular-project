import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})

//TODO : We might not need this
export class PageTitleService {

  public isTitleSet = false;

  constructor(private _titleService : Title) { }

  setTitle(title : string)
  { 
    this.isTitleSet = true;
    this._titleService.setTitle(title);
  }
}
