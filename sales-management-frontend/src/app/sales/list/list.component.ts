import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SalesService } from '../../services/sales.service';

@Component({
  selector: 'app-sales-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent implements OnInit {
  sales: any[] = [];

  constructor(private salesService: SalesService, private router: Router) {}

  ngOnInit(): void {
    this.salesService.getSales().subscribe((data) => {
      this.sales = data;
    });
  }

  deleteSale(saleId: string): void {
    this.salesService.deleteSale(saleId).subscribe(() => {
      this.sales = this.sales.filter((sale) => sale.id !== saleId);
    });
  }

  editSale(saleId: string): void {
    this.router.navigate(['/sales/edit', saleId]);
  }
}
