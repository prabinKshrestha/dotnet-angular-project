import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})

export class ATScrollerService {

    constructor() { }

    // We Might need to following code later
    // document.querySelector("#nav>ul>li");

    public scrollToClassName(className : string){
        setTimeout(() => {
            const classElement = document.getElementsByClassName(className);
            if(classElement.length > 0){
                classElement[0].scrollIntoView();
            }
        });
    }

}