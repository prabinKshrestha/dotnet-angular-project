import { Injectable } from '@angular/core';
import { ATStoreService } from '../at-store.service';
import { UserModel, ATStorageKeyConstants } from 'at-models';
import { BehaviorSubject } from 'rxjs';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})

export class UserContextService extends ATStoreService{

  private scopedStorageKey : string = ATStorageKeyConstants.USER_CONTEXT;

  public userContext : BehaviorSubject<UserModel> = new BehaviorSubject<UserModel>(null);

  constructor() 
  { 
    super();
    this.STORAGE_KEY = this.scopedStorageKey;
  }

  public exist() : boolean
  {
    return this.hasKeyInStore();
  }

  public store(userContext : UserModel) : void
  {
    this.setToStore(userContext);
    this.userContext.next(userContext);
  }

  public get() : UserModel
  {
    return this.getFromStore<UserModel>();
  }

  public clear() : void
  {
    this.removeFromStore();
  }

}
