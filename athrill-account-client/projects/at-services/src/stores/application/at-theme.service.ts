import { Injectable } from '@angular/core';
import { ATStoreService } from '../at-store.service';
import { ATStorageKeyConstants } from 'at-models';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})

// TODO:  use model here
export class ATThemeService extends ATStoreService {

  private scopedStorageKey: string = ATStorageKeyConstants.DYNAMIC_THEME_CONTEXT;

  constructor() {
    super();
    this.STORAGE_KEY = this.scopedStorageKey;
  }

  public exist(): boolean {
    return this.hasKeyInStore();
  }

  public store(data: any): void {
    this.setToStore(data);
  }

  public get(): any {
    return this.getFromStore<any>();
  }

  public clear(): void {
    this.removeFromStore();
  }

}
