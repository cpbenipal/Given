import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
@Component({
  selector: 'app-confirmation-dialog',
  template: `
    <div mat-dialog-content>
     {{message}}
    </div>
    <div mat-dialog-actions>
      <button style="color:#fff;border-radius:3px;padding:5px 15px;margin-top:5px;margin-left:10px;float:right;background:#f44336;box-shadow:0 3px 1px -2px rgba(0,0,0,.2), 0 2px 2px 0 rgba(0,0,0,.14), 0 1px 5px 0 rgba(0,0,0,.12)" mat-raised-button color="warn" (click)="onNoClick(true)" cdkFocusInitial>Yes</button>
      <button style="border-radius:3px;padding:5px 15px;margin-top:5px;margin-left:10px;float:right;box-shadow:0 3px 1px -2px rgba(0,0,0,.2), 0 2px 2px 0 rgba(0,0,0,.14), 0 1px 5px 0 rgba(0,0,0,.12)" mat-raised-button (click)="onNoClick(false)">No</button>
      
    </div>`
})
export class ConfirmationDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public message: string) { }
  onNoClick(data): void {
    this.dialogRef.close(data);
  }
}
