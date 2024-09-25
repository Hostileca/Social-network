﻿import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class SimpleValidators{
  static equalTo(controlNameToCompare: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.parent) {
        return null;
      }

      const controlToCompare = control.parent.get(controlNameToCompare);
      if (!controlToCompare) {
        return null;
      }

      const isEqual = control.value === controlToCompare.value;
      return isEqual ? null : { notEqual: true };
    };
  }
}
