import { Component, forwardRef, Input, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { AngularEditorComponent } from '@kolkov/angular-editor';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'at-assets-input-wysiwyg',
  templateUrl: './at-input-wysiwyg.component.html',
  styleUrls: ['./at-input-wysiwyg.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputWYSIWYGComponent),
      multi: true,
    }
  ],
})

export class ATInputWYSIWYGComponent implements ControlValueAccessor {

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() required: boolean = false;
  
  // <!-- TODO: Implement this -->
  // @Input() maxLength : number;
  // @Input() showLengthCountHint: boolean = true;
  
  @ViewChild("angularEditor", {static: true}) angularEditor : AngularEditorComponent;
  
  valueChanges : BehaviorSubject<any> = new  BehaviorSubject<any>(null);
  
  onChangeEvent: any = () => {};
  onTouch: any = () => {};
  
  constructor() { }
  
  get isValid(): boolean{
    let retVal = true;
    if(this.required){
        retVal = retVal && this.value && this.value.trim() != '';
    }
    return retVal;
  }

  get invalid(): boolean {
    return !this.isValid;
  } 
  
  val = null;
  
  set value(val: string) {
    this.val = val;
    this.onChangeEvent(val);
    this.onTouch(val);
  }

  get value() {
    return this.val;
  }
  
  writeValue(value: any) {
    this.value = value;
  }

  registerOnChange(fn: any) {
    this.onChangeEvent = fn;
  }

  registerOnTouched(fn: any) {
    this.onTouch = fn;
  }

  ngOnInit(): void {
  }

}