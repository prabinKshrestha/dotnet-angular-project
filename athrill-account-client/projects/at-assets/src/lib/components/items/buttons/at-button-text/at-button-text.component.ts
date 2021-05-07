import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'at-assets-button-text',
  templateUrl: './at-button-text.component.html',
  styleUrls: ['./at-button-text.component.scss']
})
export class ATButtonTextComponent implements OnInit {

  @Input() text : string = "";
  @Input() title : string = "";
  @Input() disabled : boolean = false;
  @Input() classes : string = null;
  @Input() cursor : string = "pointer";
  @Input() color : 'primary' | 'accent' | 'warn' | '' = '';
  @Input() type : 'basic' | 'raised' | 'stroked' | 'flat' = 'basic';
  @Input() set isSubmission(value : boolean){
    if(value){
      this.buttonType  = 'submit';
    }
  }

  buttonType : 'button' | 'submit' | 'reset' = 'button';

  @Output() onClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onButtonClick(){
    this.onClick.emit();
  }

}
