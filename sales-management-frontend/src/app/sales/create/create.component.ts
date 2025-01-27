import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SalesService } from '../../services/sales.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-sale',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent {
  saleForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private salesService: SalesService,
    private router: Router
  ) {
    this.saleForm = this.fb.group({
      customer: ['', Validators.required],
      totalAmount: [0, [Validators.required, Validators.min(0.01)]],
      date: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.saleForm.valid) {
      this.salesService.createSale(this.saleForm.value).subscribe(() => {
        this.router.navigate(['/sales']);
      });
    }
  }
}