﻿import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class SimpleValidators{
  static EqualTo(controlNameToCompare: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.parent) {
        return null;
      }

      const controlToCompare = control.parent.get(controlNameToCompare);
      if (!controlToCompare) {
        return null;
      }


      const isEqual = control.value === controlToCompare.value;
      if(isEqual) {
        return null
      }

      return  { notEqual: true };
    };
  }

  static IsFutureDate(control: AbstractControl): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if(!control.value){
        return null
      }

      if(control.value < Date.now()){
        return {notFutureDate: true}
      }

      return null
    }
  }
}
