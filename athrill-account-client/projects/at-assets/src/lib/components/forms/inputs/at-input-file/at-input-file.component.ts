import { Component, EventEmitter, forwardRef, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgModel, NG_VALUE_ACCESSOR } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'at-assets-input-file',
  templateUrl: './at-input-file.component.html',
  styleUrls: ['./../at-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ATInputFileComponent),
      multi: true,
    }
  ],
})

export class ATInputFileComponent{

  @Input() name : string;
  @Input() label : string;
  @Input() placeholder : string;
  @Input() accept : string;
  @Input() required: boolean = false;
  @Input() allowedExtensions : string[] = [];

  @Input() set existingFile(value : string){
    this.fileName = value;
  }

  @Input() atFileModel : File;
  @Output() atFileModelChange : EventEmitter<File> = new EventEmitter();
  
  @Input() isFileChanged : boolean;
  @Output() isFileChangedChange : EventEmitter<boolean> = new EventEmitter();

  @ViewChild("input") model : NgModel;

  valueChanges : BehaviorSubject<File> = new  BehaviorSubject<File>(null);

  fileName : string;

  get hasFile(): boolean {
    return this.fileName != null && this.fileName.trim() != '';
  } 

  get valid(): boolean {
    let retVal : boolean = true;
    if(this.required){
      retVal = retVal && this.hasFile;
    }
    return retVal;
  } 

  get invalid(): boolean {
    return !this.valid;
  } 

  constructor() { }

  ngOnInit(): void {
  } 
  
  onFileChange(event: any){
    this.atFileModel = event?.target?.files[0];
    this.fileName = this.atFileModel?.name;
    this.valueChanges.next(this.atFileModel);
    this.atFileModelChange.emit(this.atFileModel);
    this.isFileChangedChange.emit(true);
  }

  onRemoveFile(){
    this.onFileChange(null);
  }

}