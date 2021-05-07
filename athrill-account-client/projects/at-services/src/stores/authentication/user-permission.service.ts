import { Injectable } from '@angular/core';
import { ATStoreService } from '../at-store.service';
import { ATStorageKeyConstants } from 'at-models';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})

export class UserPermissionService extends ATStoreService {

  private scopedStorageKey: string = ATStorageKeyConstants.USER_PERMISSION_CONTEXT;

  constructor() {
    super();
    this.STORAGE_KEY = this.scopedStorageKey;
  }

  public exist(): boolean {
    return this.hasKeyInStore();
  }

  public store(permissions: string[]): void {
    this.setToStore(permissions);
  }

  public get(): string[] {
    return this.getFromStore<string[]>();
  }

  public clear(): void {
    this.removeFromStore();
  }

  public hasPermission(permission: string): boolean {
    return this.get().indexOf(permission) > -1;
  }

  //TODO: Need to test this method properly before use
  public hasPermissions(permissions: string[]): boolean {
    let retVal: boolean = false;
    let userPermissions: string[] = this.get();
    for (let i = 0; i < permissions.length; i++) {
      if (userPermissions.indexOf(permissions[i]) > -1) {
        retVal = true;
      } else {
        retVal = false;
        break;
      }
    }
    return retVal;
  }

}
