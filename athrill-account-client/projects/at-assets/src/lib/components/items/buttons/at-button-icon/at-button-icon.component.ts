import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'at-assets-button-icon',
  templateUrl: './at-button-icon.component.html',
  styleUrls: ['./at-button-icon.component.scss']
})
export class ATButtonIconComponent implements OnInit {

  @Input() title : string = "";
  @Input() icon : string = "";
  @Input() fontAwesomeIcon : string = "";
  @Input() disabled : boolean = false;
  @Input() classes : string = null;
  @Input() type : 'icon' | 'fontawesome' | 'fab' | 'mini-fab' = 'icon';
  @Input() color : 'primary' | 'accent' | 'warn' | '' = '';
  @Input() cursor : string = "pointer";
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
