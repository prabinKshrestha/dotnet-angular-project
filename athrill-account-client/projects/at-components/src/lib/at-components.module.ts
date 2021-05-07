import { NgModule } from '@angular/core';
import { ATLocalDateTimePipe } from './pipes/dates/at-local.pipe';
import { ATNetworkImagePipe } from './pipes/images/at-network-image.pipe';

const Pipes = [
  ATNetworkImagePipe,  
  ATLocalDateTimePipe, 
]

@NgModule({
  declarations: [
    ...Pipes
  ],
  imports: [
  ],
  exports: [
    ...Pipes
  ]
})
export class AtComponentsModule { }
