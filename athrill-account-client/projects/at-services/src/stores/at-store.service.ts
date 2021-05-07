import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ATStoreService 
{
    protected STORAGE_KEY : string;

    constructor(){}

    protected setStorageKey(storageKey : string) : void
    {
      this.STORAGE_KEY = storageKey;
    }

    protected hasKeyInStore() : boolean
    {
        return window.localStorage.getItem(this.STORAGE_KEY) != null;
    }
    protected setToStore(data : any)
    {
        return window.localStorage.setItem(this.STORAGE_KEY,JSON.stringify(data));
    }
    protected removeFromStore() : void
    {
        return window.localStorage.removeItem(this.STORAGE_KEY);
    }
    protected getFromStore<T>() : T
    {
        return JSON.parse(window.localStorage.getItem(this.STORAGE_KEY)) as T;
    }


    protected hasKeyInStoreWithArg<T>(storageKey : string) : T
    {
        return JSON.parse(window.localStorage.getItem(storageKey)) as T;
    }
    
    protected getFromStoreWithArg<T>(storageKey : string) : T
    {
        return JSON.parse(window.localStorage.getItem(storageKey)) as T;
    }

    protected setToStoreWithArg(data : any, storageKey:string)
    {
        return window.localStorage.setItem(storageKey,JSON.stringify(data));
    }

    protected removeFromStoreWithArg(storageKey:string) : void
    {
        return window.localStorage.removeItem(storageKey);
    }
}
