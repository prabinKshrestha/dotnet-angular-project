import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ContentsLayoutComponent } from './contents-layout.component';
import { ContentGeneralComponent } from './views/content-general/content-general.component';
import { ContentTreeComponent } from './views/content-tree/content-tree.component';


const routes: Routes = [
    {
        path: '',
        component: ContentsLayoutComponent,
        children : [
          {
            path: 'general',
            component: ContentGeneralComponent,
            pathMatch: 'full'
          },
          {
            path: 'tree',
            component: ContentTreeComponent,
            pathMatch: 'full'
          },
          {
            path: '',
            redirectTo: 'general'
          },
          {
            path: '**',
            component: ContentGeneralComponent
          }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContentsRoutingModule { }
