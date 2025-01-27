import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SalesService } from '../../services/sales.service';

@Component({
  selector: 'app-edit-sale',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent implements OnInit {
  saleForm: FormGroup;
  saleId: string = '';

  constructor(
    private fb: FormBuilder,
    private salesService: SalesService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.saleForm = this.fb.group({
      customer: ['', Validators.required],
      totalAmount: [0, [Validators.required, Validators.min(0.01)]],
      date: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.saleId = this.route.snapshot.paramMap.get('id') || '';
    if (this.saleId) {
      this.salesService.getSaleById(this.saleId).subscribe((sale) => {
        this.saleForm.patchValue({
          customer: sale.customer,
          totalAmount: sale.totalAmount,
          date: sale.date,
        });
      });
    }
  }

  onSubmit(): void {
    if (this.saleForm.valid) {
      this.salesService.updateSale(this.saleId, this.saleForm.value).subscribe(() => {
        this.router.navigate(['/sales']);
      });
    }
  }
}
