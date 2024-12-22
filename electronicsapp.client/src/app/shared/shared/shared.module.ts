import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedHeaderComponent } from './shared-header/shared-header.component';
import { RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.services';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [SharedHeaderComponent],
  imports: [CommonModule, RouterModule],
  exports: [SharedHeaderComponent],
  providers: [AuthService],
})
export class SharedModule {}
